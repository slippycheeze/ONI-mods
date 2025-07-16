namespace SlippyCheeze.RescueEntombedCritters;


[HarmonyPatch]
public class RescueEntombedCritterMonitor: StateMachineComponent<RescueEntombedCritterMonitor.SMI> {
    // The critter will end up this many cells up.  Inclusive; if the value is 5, and you at Y=10,
    // you might end up anywhere from Y=11 to Y=15 when auto-rescuing.
    public const int MaxCellsToMoveVerticallyIfEntombed = 5;


    public override void OnSpawn() {
        smi.StartSM();
    }

    public class SMI(RescueEntombedCritterMonitor master):
        GameStateMachine<States, SMI, RescueEntombedCritterMonitor, object>.GameInstance(master) {

        public bool IsEntombed() {
            // 2025-07-16 REVISIT: I'm not sure if the TagTransition is racing, so adding a check here?
            if (HasTag(GameTags.Creatures.Bagged))
                return false;
            
            int cell = Grid.PosToCell(this);
            if (!Grid.IsSolidCell(cell)) {
                // L.debug($"{master.Humanize()}: not in a solid cell");
                return false;
            }

            if (HasTag(GameTags.Creatures.Burrowed)) {
                // Hatches can unburrow even from built tiles, but there must be room above.
                if (gameObject.GetSMI<BurrowMonitor.Instance>().EmergeIsClear())
                    return false;
            }

            if (this.gameObject.GetSMI<DiggerMonitor.Instance>() is DiggerMonitor.Instance monitor) {
                if (monitor.IsValidDigCell(cell)) {
                    // Shove voles can get out of almost everything.
                    return false;
                }
            }

            // L.debug($"{master.Humanize()}: found an entombed critter");
            return true;
        }

        public void AutomaticallyRescue() {
            L.debug($"{master.Humanize()}: trying to auto-rescue trapped critter");

            if (!IsEntombed()) {
                GoTo(sm.normal);
                return;         // just in case we were freed, but didn't notice yet.
            }

            int cell = Grid.PosToCell(this);
            for (int offset = 1; offset <= MaxCellsToMoveVerticallyIfEntombed; offset++) {
                int candidate = Grid.OffsetCell(cell, x: 0, y: offset);
                if (!Grid.AreCellsInSameWorld(cell, candidate) || Grid.IsCellBiomeSpaceBiome(candidate))
                    break;      // just give up if we hit space...

                if (Grid.IsSolidCell(candidate))
                    continue;   // nope, try again.  try harder.

                // found a satisfactory cell, where we are not stuck in a solid.  might be stuck in
                // liquid or something, but whatever, that isn't /our/ problem.
                Vector3 pos = Grid.CellToPos(candidate, CellAlignment.RandomInternal, Grid.SceneLayer.Creatures);
                L.debug($"{master.Humanize()}: auto-rescue moving from {Grid.CellToXY(cell)} to {Grid.CellToXY(candidate)}");
                transform.SetPosition(pos);

                // ...aaand we are falling, aaah!
                if (GameComps.Fallers.Has(this.gameObject))
                    GameComps.Fallers.Remove(this.gameObject);
                GameComps.Fallers.Add(this.gameObject, Vector2.zero);

                // yay.  rescue.  back to normal state to await our next fun experience of being trapped.
                this.GoTo(sm.normal);
                return;
            }

            L.debug($"{master.Humanize()}: auto-rescue attempt failed.");
        }
    }

    public class States: GameStateMachine<States, SMI, RescueEntombedCritterMonitor> {
        // not entombed
        public State normal                     = null!;

        // while we are bagged we ignore being entombed; this, apparently, avoids triggering alerts
        // when being carried, and inside an Airlock Door, per the mod.
        public State bagged                     = null!;

        // we are entombed, rescue ourselves if possible, or bring up the notification to get rescued.
        public State entombed                   = null!;


        public override void InitializeStates(out BaseState default_state) {
            default_state = this.normal;

            this.normal
                // .Enter("Normal", smi => L.debug($"IsNormal: {smi.master.Humanize()}"))
                .Transition(entombed, smi => smi.IsEntombed(), UpdateRate.SIM_4000ms)
                .TagTransition(GameTags.Creatures.Bagged, this.bagged);

            this.bagged
                // .Enter("Bagged", smi => L.debug($"IsBagged: {smi.master.Humanize()}"))
                .TagTransition(GameTags.Creatures.Bagged, this.normal, on_remove: true);


            this.entombed
                .Transition(normal, smi => !smi.IsEntombed(), UpdateRate.SIM_4000ms)
                .TagTransition(GameTags.Creatures.Bagged, this.bagged)
                .Enter("AutomaticallyRescue", smi => smi.AutomaticallyRescue())
                .ToggleNotification(
                    smi => new Notification(
                        MODSTRINGS.MISC.NOTIFICATIONS.ENTOMBEDCRITTER.NAME,
                        NotificationType.BadMinor,
                        (notifications, data) => string.Concat(
                            MODSTRINGS.MISC.NOTIFICATIONS.ENTOMBEDCRITTER.TOOLTIP,
                            notifications.ReduceMessages(countNames: false)
                        ),
                        expires: false
                    )
                );
        }
    }


    public static Tag[] AllRobotModels => field ??= typeof(GameTags.Robots.Models)
        .GetDeclaredFields()
        .Where(fi => fi.IsPublic && fi.IsStatic)
        .Select(fi => fi.GetValue(null))
        .OfType<Tag>()
        .ToArray();

    // Wiring ourselves into the system as generically as reasonable.
    [HarmonyPatch(typeof(EntityTemplates), nameof(EntityTemplates.AddCreatureBrain))]
    [HarmonyPostfix]
    public static void OnAddCreatureBrain(GameObject prefab, Tag species) {
        // Hives are technically creatures, but the warning is annoying there and probably(?) does not make much sense.
        if (prefab.GetComponent<HiveWorkableEmpty>() is not null)
            return;

        // This should check !HasTag(GameTags.Robot), but that isn't set for rovers early enough;
        // nothing requires it to be.  So, this hopefully not too terrible alternative:
        if (AllRobotModels.Contains(species))
            return;

        // L.info($"Adding RescueEntombedCritterMonitor to {prefab.Humanize()}");

        // hopefully...
        prefab.AddOrGet<RescueEntombedCritterMonitor>();

        // if (prefab.GetComponent<Capturable>() is null)
        //     L.error($"prefab {prefab.PrefabID()} does not have a Capturable component");
        // MonitoredPrefabTags.Add(prefab.PrefabID());
    }
}


public static partial class MODSTRINGS {
    public static partial class MISC {
        public static partial class NOTIFICATIONS {
            public static partial class ENTOMBEDCRITTER {
                public static readonly LocString NAME     = "Critter entombed";
                public static readonly LocString TOOLTIP  = "These critters are entombed:";
            }
        }
    }
}
