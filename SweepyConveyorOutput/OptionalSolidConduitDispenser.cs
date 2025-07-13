namespace SlippyCheeze.SweepyConveyorOutput;

// dont't like doing this, but there isn't a better way to make one of these without copying a lot
// of code, so harmony-emulated virtual methods it is.
[HarmonyPatch(typeof(SolidConduitDispenser), nameof(SolidConduitDispenser.ConduitUpdate))]
public partial class OptionalSolidConduitDispenser: SolidConduitDispenser {
    public static void Postfix(SolidConduitDispenser __instance) {
        if (__instance is OptionalSolidConduitDispenser)
            __instance.operational.SetFlag(outputConduitFlag, true);
    }
}
