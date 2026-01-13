namespace SlippyCheeze.TweaksAndFixes;

// https://forums.kleientertainment.com/klei-bug-tracker/oni/slicksters-do-not-float-and-drown-r44058/
//
// When a liquid crossed the substantial-liquid threshold, something related to pathfinding changes,
// but pathfinding itself is not updated. Check for drowning and make pathfinding dirty if needed.
[HarmonyPatch(typeof(DrowningMonitor), nameof(DrowningMonitor.CheckDrowning))]
internal static class FixSlicksterDrowningBug {
    internal static void Prepare(MethodInfo target) {
        if (target == null)
            L.log($"Fixed drowning Slicksters being unable to find their way out of the water.");
    }

    public static void Postfix(DrowningMonitor __instance, bool ___drowning) {
        if (!___drowning)
            return;
        if (!__instance.canDrownToDeath)
            return; // The code is used also for lettuce, which cannot move or drown.
        int cell = Grid.PosToCell(__instance.gameObject);
        if (IsPossiblyDrowningChamber(cell))
            return;
        Pathfinding.Instance.AddDirtyNavGridCell(cell);
    }

    private static bool IsPossiblyDrowningChamber(int cell) {
        // I'm not sure how big the performance impact of making the pathfinding dirty is, but it
        // can't hurt to avoid the common case of a drowning chamber, in which case the critter
        // cannot escape the drowning anyway.
        CavityInfo cavity = Game.Instance.roomProber.GetCavityForCell(cell);
        if (cavity == null)
            return false;
        if (cavity.NumCells > 64)  // 8×8 chamber
            return false;
        for (int x = cavity.minX; x <= cavity.maxX; ++x)
            for (int y = cavity.minY; y <= cavity.maxY; ++y) {
                if (!Grid.IsSubstantialLiquid(Grid.XYToCell(x, y)))
                    return false;
            }
        return true;
    }
}
