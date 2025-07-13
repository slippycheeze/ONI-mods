namespace SlippyCheeze.SweepyConveyorOutput;

// dont't like doing this, but there isn't a better way to make one of these without copying a lot
// of code, so harmony-emulated virtual methods it is.
[HarmonyPatch(typeof(ConduitDispenser), nameof(ConduitDispenser.ConduitUpdate))]
public partial class OptionalConduitDispenser: ConduitDispenser {
    public static void Postfix(ConduitDispenser __instance) {
        if (__instance is OptionalConduitDispenser)
            __instance.operational?.SetFlag(outputConduitFlag, true);
    }
}
