using ConduitContents = ConduitFlow.ConduitContents;
using DiseaseInfo     = Klei.SimUtil.DiseaseInfo;

namespace SlippyCheeze.PressureValves;

public partial class LiquidPressureValve: PressureValve {
    public override ConduitType ConduitType => ConduitType.Liquid;
}

public partial class GasPressureValve: PressureValve {
    public override ConduitType ConduitType => ConduitType.Gas;
}

public abstract partial class PressureValve: KMonoBehaviour, IBridgedNetworkItem {
    [MyCmpReq]
    private Storage storage = null!;

    [MyCmpReq]
    private Operational operational = null!;
    // name isn't actually used, so IDK why I bother, really.
    private static readonly Operational.Flag inputConnectionFlag   = new("input",  Operational.Flag.Type.Functional);
    private static readonly Operational.Flag outputConnectionFlag  = new("output", Operational.Flag.Type.Functional);
    // private static readonly Operational.Flag outputConduitFullFlag = new("outputFull", Operational.Flag.Type.Functional);

    public abstract ConduitType ConduitType { get; }

    private ConduitFlow FlowManager {
        get => field ??= ConduitType switch {
            ConduitType.Liquid => Conduit.GetFlowManager(ConduitType),
            ConduitType.Gas    => Conduit.GetFlowManager(ConduitType),
        };
        set;
    }

    private int inputCell;
    private int outputCell;


    private static bool warnOnce = true;

    // ======================================================================
    public override void OnSpawn() {
        base.OnSpawn();

        if (warnOnce) {
            L.warn($"This component should manage a MeterController for ... something.  probably fullness?  or throughput?");
            L.warn($"This component should control operational.IsActive when, y'know, active, or anims will not work.");
            // ALSO, consider setting `PoweredActiveTransitionController.Def.showWorkingStatus` true?
            warnOnce = false;
        }

        Subscribe((int)GameHashes.FunctionalChanged, OnFunctionalChangedDelegate);

        var building = GetComponent<Building>();
        inputCell    = building.GetUtilityInputCell();
        outputCell   = building.GetUtilityOutputCell();

        FlowManager.onConduitsRebuilt += OnConduitNetworksChanged;
        OnConduitNetworksChanged();

        FlowManager.AddConduitUpdater(this.ConduitUpdate);
    }

    public override void OnCleanUp() {
        FlowManager.RemoveConduitUpdater(this.ConduitUpdate);

        FlowManager.onConduitsRebuilt -= OnConduitNetworksChanged;
        OnConduitNetworksChanged();

        base.OnCleanUp();
    }

    private void OnConduitNetworksChanged() {
        operational.SetFlag(inputConnectionFlag,  FlowManager.HasConduit(inputCell));
        operational.SetFlag(outputConnectionFlag, FlowManager.HasConduit(outputCell));
    }

    private static readonly EventSystem.IntraObjectHandler<PressureValve> OnFunctionalChangedDelegate
        = new((component, data) => component.OnFunctionalChanged((bool)data));

    private void OnFunctionalChanged(bool functional) {
        // if we are non-functional, we are definitely non-active.  on the other hand, if we are
        // functional then active being both true and false is handled in the ConduitUpdate method
        // when we emit...
        if (!functional)
            operational.SetActive(false);
    }


    private void ConduitUpdate(float dt) {
        // 2025-07-17 REVISIT: I'm pretty sure I *don't* want to be non-functional here, but rather,
        // non-active.  which makes sense, but OTOH, means we would continue to consume from the
        // input until storage was filled...
        //
        // are we blocked on output?  if so, we stop being functional.  by the time of the next call
        // to our ConduitUpdate the flow system *should* have removed as much as possible from the
        // output conduit already, so we don't need to worry about `IsConduitFull` being true
        // because we are waiting for that to happen already.
        //
        // operational.SetFlag(outputConduitFullFlag, FlowManager.IsConduitFull(outputCell));

        if (!operational.IsFunctional)  // and here we check it, round-about style. :)
            return;             // nothing to do, friend, nothing to do.

        // amount we have sent to the output conduit.
        float massToConsume = FlowManager.MaxMass;
        float massEmitted   = 0;

        // this should never, ever store more thana a single full packet of any substance in our
        // storage.  we try and fill first, and then output, which'll leave a full packet of empty
        // space if we succeed.
        //
        // then next update we will consume up to a full packet, which'll get us ready to output it
        // immediately if we are looking at a full packet each update on the input conduit.
        var packet = FlowManager.GetContents(inputCell);
        if (packet.mass > 0) {
            massToConsume = Mathf.Min(packet.mass, FlowManager.MaxMass);
            // L.debug($"have input packet, will consume Min(packet.mass={packet.mass}, FlowManager.MaxMass={FlowManager.MaxMass}) = {massToConsume}");

            if (storage.FindPrimaryElement(packet.element) is PrimaryElement stored) {
                float storedMass = storage.GetMassAvailable(stored.ElementID);
                massToConsume = Mathf.Min(massToConsume, FlowManager.MaxMass - storedMass);
                // L.debug($"Storing {storedMass} of {packet.element}, massToConsume={massToConsume}");
            }

            // if we don't have enough storage space to fill the current packet with the content of
            // the input conduit, try and emit something to free up some space, even
            // a partial packet.
            if (storage.RemainingCapacity() < massToConsume) {
                // L.debug($"Trying to emit packet because storage.RemainingCapacity() is less than massToConsume");
                massEmitted += TryAndOutputPacket(emitPartialPackets: true);
            }

            // and now, consume enough that we don't over-fill storage.  hopefully the full content
            // of the input, but you never know. :)
            massToConsume = Mathf.Min(massToConsume, storage.RemainingCapacity());
            // L.debug($"finaly massToConsume={massToConsume}");
            if (massToConsume > 0) {
                storage.AddElement(
                    element:        packet.element,
                    mass:           massToConsume,
                    temperature:    packet.temperature,
                    disease_idx:    packet.diseaseIdx,
                    disease_count:  Mathf.FloorToInt((massToConsume / packet.mass) * packet.diseaseCount)
                );
                FlowManager.RemoveElement(inputCell, massToConsume);
            }
        }

        // now, try and output a packet, if we have any capacity left.  if not, this'll just return early.
        massEmitted += TryAndOutputPacket(emitPartialPackets: false);

        // update our activity state based on the amount we output, basically, idle if we can't
        // output, otherwise inactive.
        operational.SetActive(massEmitted > 0);
    }

    private float TryAndOutputPacket(bool emitPartialPackets) {
        // L.debug($"emitPartialPackets={emitPartialPackets}");

        // figure out what we can output.  starting with: can we merge something into the
        // output conduit?
        SimHashes element = SimHashes.Vacuum;
        float wantToEmit  = FlowManager.MaxMass;

        if (FlowManager.GetContents(outputCell) is ConduitContents packet
            && packet.element != SimHashes.Vacuum
            && packet.mass > 0
        ) {
            element = packet.element;
            wantToEmit -= packet.mass;  // fill the cell, but only that much. :)
        } else if (element == SimHashes.Vacuum && storage.PrimaryElementWithHighestMass() is PrimaryElement pe) {
            element = pe.ElementID;
        } else {
            return 0;             // nothing we can emit.  not an error, just ... nothing we can do.
        }

        if (wantToEmit <= 0)
            return 0;

        // OK, storage, we know what element, now, how much do you hold?  if we are not being forced
        // to emit partial packets, and we hold less than a full packet, we don't emit anything.
        float canEmit = Mathf.Min(wantToEmit, storage.GetMassAvailable(element));
        if (canEmit <= 0) {
            // most likely, we have a partially fill output, and none of element in storage, so we
            // can't fill the output pipe any further, but we also can't put a new element in there.
            return 0;
        }
        if (!emitPartialPackets && canEmit < wantToEmit) {
            // L.debug($"not emitting: !emitPartialPackets={!emitPartialPackets} and canEmit({canEmit}) < wantToEmit({wantToEmit})");
            return 0;
        }

        // now, consume the desired amount from storage, and emit it to the output, passing on the
        // proportional disease and all that jazz.
        storage.ConsumeAndGetDisease(
            element.ToTag(),
            canEmit,
            out float willEmit,
            out DiseaseInfo disease,
            out float temperature
        );

        // this should *never* return less than the amount we wanted to emit, because we
        // deliberately limited that to be <= the available space, but just in case...
        float didEmit = FlowManager.AddElement(
            outputCell,
            element,
            willEmit,
            temperature,
            disease.idx,
            disease.count
        );
        if (didEmit < willEmit) {
            L.error($"expected to emit {willEmit}, but only output {didEmit}, WTF, Klei and/or ME!");
            // aside from complaining, put back the excess into our storage for next time.
            storage.AddElement(
                element:        element,
                mass:           willEmit - didEmit,
                temperature:    temperature,
                disease_idx:    disease.idx,
                disease_count:  Mathf.FloorToInt((didEmit / willEmit) * disease.count)
            );
        }

        // L.debug(didEmit);
        return didEmit;
    }



    // ======================================================================
    // IBridgedNetworkItem
    //
    // 2025-07-17 REVISIT: I feel like this acts more like a clever bridge than a valve/gate of any
    // sort, so I *think* rendering both sides as "part of the same network" is correct.
    void IBridgedNetworkItem.AddNetworks(ICollection<UtilityNetwork> networks) {
        IUtilityNetworkMgr manager = Conduit.GetNetworkManager(ConduitType.Liquid);
        UtilityNetwork inputNetwork = manager.GetNetworkForCell(inputCell);
        if (inputNetwork != null)  // remember, Unity, so `==` rather than `is null`
            networks.Add(inputNetwork);
        UtilityNetwork outputNetwork = manager.GetNetworkForCell(outputCell);
        if (outputNetwork != null)
            networks.Add(outputNetwork);
    }

    bool IBridgedNetworkItem.IsConnectedToNetworks(ICollection<UtilityNetwork> networks) {
        IUtilityNetworkMgr manager = Conduit.GetNetworkManager(ConduitType.Liquid);
        // this was hand-derived from the quite confusing disassmbly, but the Klei implementation is
        // "either", not "both", which I guess makes sense.
        return networks.Contains(manager.GetNetworkForCell(inputCell))
            || networks.Contains(manager.GetNetworkForCell(outputCell));
    }

    // feels wrong, but this is what the ConduitBridge does, so it what we do. :(
    int IBridgedNetworkItem.GetNetworkCell() => this.inputCell;
}
