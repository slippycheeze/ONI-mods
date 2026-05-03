using KSerialization;

namespace SlippyCheeze.SupplyTeleporterLogicBridge;

/*
 * This component lives on the sender side of the warp conduit buildings, and when we connect to the
 * partner receiver, forwards any logic signals received to the ribbon output on that building.
 *
 * All the work is done here, including hooking into the receiver and adding the output port,
 * because there is absolutely zero logic on that side: we just add the port definition, and then
 * interact with it from here.
 *
 * NOTE: OptIn serialization, because we actually save nothing.  all connectivity happens each time,
 * or rather, is handled by other components and we just ride their coat-tails.
 */
[SerializationConfig(MemberSerialization.OptIn)]
public partial class SupplyTeleporterLogicSender: KMonoBehaviour {
    public static readonly HashedString SENDER_INPUT_PORT_ID  = "SupplyTeleporterSenderLogicInput";
    public static readonly HashedString RECEIVER_OUTPUT_PORT_ID = "SupplyTeleporterReceiverLogicOutput";

    [MyCmpGet]
    private Operational operational = null!;

    [MyCmpGet]
    private LogicPorts logicports = null!;

    [MyCmpGet]
    private WarpConduitSender sender = null!;


    public override void OnPrefabInit() {
        base.OnPrefabInit();
        Subscribe<SupplyTeleporterLogicSender>((int)GameHashes.LogicEvent, OnLogicEventDelegate);
    }

    public override void OnSpawn() {
        base.OnSpawn();
        // we can't know if SupplyTeleporterSender.OnSpawn ran yet, so schedule this for the *next*
        // frame, which ensures that all the OnSpawn has happened.
        Game.Instance.StartDelayed(0, () => this.SendLogicValue(logicports.GetInputValue(SENDER_INPUT_PORT_ID)));
    }

    public override void OnCleanUp() {
        Unsubscribe(ref onLogicEventHandle);
        base.OnCleanUp();
    }

    // received when our input logic value changes, via the LogicPorts component.
    private static readonly EventSystem.IntraObjectHandler<SupplyTeleporterLogicSender> OnLogicEventDelegate
        = new((component, data) => component.OnLogicEvent(data));

    // the handle from our event subscription.  non-persistent, since we reconnect next time.
    private int onLogicEventHandle = -1;

    internal void OnLogicEvent(object data) {
        if (data is not LogicValueChanged change) {
            L.error($"OnLogicEvent(data={data.GetType()}<{data}>) when LogicValueChanged was expected");
            return;
        }

        // ignore the event if it isn't for our port, or if the old and new values are identical.
        if (change.portID != SENDER_INPUT_PORT_ID || change.newValue == change.prevValue)
            return;

        // delegate to the actual sending method to do the work though. :)
        SendLogicValue(change.newValue);
    }


    // if everything is connected together, output the logic value on the peer SupplyTeleporterReceiver.
    public void SendLogicValue(int value) {
        if (sender.receiver == null)
            return;             // guess they ain't connected together yet.

        // ignore until we are fully active.  this trusts that we are never operational without the
        // other side also being operational.
        if (!operational.IsOperational)
            return;

        // all seems in order, relay this to the other side as the logic output.  again, trusting
        // that being operational here, and having a peer, means we are also operational there.
        sender.receiver.GetComponent<LogicPorts>().SendSignal(RECEIVER_OUTPUT_PORT_ID, value);
    }
}
