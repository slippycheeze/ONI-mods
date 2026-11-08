namespace SlippyCheeze.DupesBreathInSuitsWhenAsphyxiating;

[HarmonyPatch(typeof(GasBreatherFromWorldProvider), nameof(GasBreatherFromWorldProvider.IsBlocked))]
public partial class DupesBreathInSuitsWhenAsphyxiating {
    public static void Postfix(OxygenBreather ___oxygenBreather, ref bool __result) {
        if (!__result)
            return;
        if (___oxygenBreather == null)
            return;
        if (!___oxygenBreather.HasTag(GameTags.RecoveringBreath))
            return;
        __result = false;
    }
}
