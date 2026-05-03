namespace SlippyCheeze.MeteorCrashBangCatcher;

[HarmonyPatch]
internal static partial class Game__InitializeFXSpawners__destroyer_safety {
    // the destroyer method is hidden inside several layers of lambdas, capturing from their
    // environemnt, making them horribly inaccessible to anything less ugly than this.  sad.
    //
    // our target is this:
    // Method:Game+<>c__DisplayClass187_0.<InitializeFXSpawners>b__0(SpawnFXHashes fxid,UnityEngine.GameObject go)
    public static MethodBase? TargetMethod()
        => typeof(Game).Inner("<>c__DisplayClass187_0")?.DeclaredMethod("<InitializeFXSpawners>b__0");

    public static bool Prepare(MethodBase target) {
        if (target != null)
            L.info($"Patching {target.ShortDescription(true)} to detect out-of-bounds FX crashes");
        return TargetMethod() != null;
    }

    public static void Prefix(SpawnFXHashes fxid, GameObject go) {
        // copies the internals, but I don't want to write a transpiler just to avoid a slightly
        // redundant check, and this is fragile enough already that I can't really make it worse by
        // copying logic around too.
        if (Game.IsQuitting())
            return;

        int cell = Grid.PosToCell(go);
        int activeFX_max = Game.Instance.activeFX.GetLength(0);

        if (!Grid.IsValidCell(cell) || cell < 0 || cell >= activeFX_max) {
            L.error($"Invalid World Cell cell={cell} vs activeFX[{activeFX_max}] when destroying FXSpawner fxid={fxid} for {go.Humanize()}");
        }

        if (!Game.Instance.fxPools.ContainsKey((int)fxid)) {
            L.error($"Game.fxPool missing destroying FXSpawner fxid={fxid} for {go.Humanize()}");
        }
    }
}
