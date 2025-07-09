namespace SlippyCheeze.AllHatchesMustDie;


[HarmonyPatch(typeof(Db), nameof(Db.PostProcess))]
internal static class AllHatchesMustDie {
    internal static void Postfix() {
        string[] hatches = ["Hatch", "HatchBaby", "Glom"];

        foreach (string ID in hatches) {
            L.log($"Watching for spawns of {ID}");
            Assets.GetPrefab(ID).GetComponent<KPrefabID>().prefabSpawnFn += OnHatchSpawned;
        }
    }

    public const string ID = "AllHatchesMustDie";

    private static StatusItem MakeStatusItem(string name) {
        var item = new StatusItem(
            id:                 $"{AllHatchesMustDie.ID}.{name.ToUpperInvariant()}",
            name:               String.Format(MODSTRINGS.CREATURES.STATUSITEMS.ALLHATCHESMUSTDIE.NAME, name),
            tooltip:            String.Format(MODSTRINGS.CREATURES.STATUSITEMS.ALLHATCHESMUSTDIE.TOOLTIP, name),
            icon:               "",
            icon_type:          StatusItem.IconType.Exclamation,
            notification_type:  NotificationType.Bad,  // BadMinor?
            allow_multiples:    false,
            render_overlay:     OverlayModes.None.ID,
            showWorldIcon:      true
        );
        item.AddNotification(
            sound_path:            null,
            notification_text:     String.Format(MODSTRINGS.CREATURES.STATUSITEMS.ALLHATCHESMUSTDIE.NOTIFICATION_NAME, name),
            notification_tooltip:  String.Format(MODSTRINGS.CREATURES.STATUSITEMS.ALLHATCHESMUSTDIE.NOTIFICATION_TOOLTIP, name)
        );
        return item;
    }

    private static StatusItem HatchStatusItem => field ??= MakeStatusItem("Hatches");
    private static StatusItem GlomStatusItem  => field ??= MakeStatusItem("Morbs");

    private static StatusItem GetStatusItemFor(GameObject go) => go.PrefabID().ToString() switch {
        "Hatch" or "HatchBaby" => HatchStatusItem,
        "Glom"                 => GlomStatusItem,
        _                      => MakeStatusItem($"<<<REPORT THIS ERROR: go.PrefabID()>>>")
    };

    internal static void OnHatchSpawned(GameObject go) {
        if (!go.TryGetComponent<KSelectable>(out var selectable)) {
            L.error($"passed {go}, but has no KSelectable Component?");
            return;
        }

        // I deliberately throw away the return value here, which would otherwise be useful to
        // remove the StatusItem from the creature ... because I never will.  They all must die!
        //
        // Setting `allow_multiples: false` above ensures that we never add the StatusItem a second
        // time to the same "group" of status effects on a thing™ according to the game logic.
        selectable.AddStatusItem(GetStatusItemFor(go));

        // ...and try and flag it to be attacked, just like the AttackTool would.
        if (go.TryGetComponent<FactionAlignment>(out var alignment)) {
            // safety check for the moment.
            var disposition = FactionManager.Instance.GetDisposition(FactionManager.FactionID.Duplicant, alignment.Alignment);
            if (disposition == FactionManager.Disposition.Assist)
                L.warn($"shocking disposition {disposition} from factionAlignment {alignment.Alignment}; go={go}");

            alignment.SetPlayerTargeted(true);
            if (go.TryGetComponent<Prioritizable>(out var prioritizable))
                prioritizable.SetMasterPriority(new(PriorityScreen.PriorityClass.basic, 8));
        }
    }
}

public static partial class MODSTRINGS {
    public static partial class CREATURES {
        public static partial class STATUSITEMS {
            public static partial class ALLHATCHESMUSTDIE {
                public static readonly LocString NAME = "All {0} Must Die!";
                public static readonly LocString TOOLTIP = $"A {"{0}".AsKeyWord()} has been detected.  These pernicious little terrors MUST ALL DIE!!!!11!1!";
                public static readonly LocString NOTIFICATION_NAME = NAME;
                public static readonly LocString NOTIFICATION_TOOLTIP = TOOLTIP;
            }
        }
    }
}
