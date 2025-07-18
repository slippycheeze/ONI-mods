using STRINGS;

namespace SlippyCheeze.TweaksAndFixes;

// The 'Crops' diagnostic group has two problematic items:
//
// - 'Check colony has farms' does not seem to make much sense, that's not a state that is likely to
//   change without the player noticing, and it is also rather pointless (there are other ways to
//   make food).
//
// - 'Check farms are planted' triggers even if there are no farm plots.
//
// These two together mean that e.g. unhabitated planetoids get a permanent yellow 'Crops'
// diagnostic that is useless. Make it possible to disable the first one and fix the latter one.

[HarmonyPatch]
internal static class FixFarmDiagnostics {
    internal static void Prepare(MethodInfo target) {
        if (target == null)
            L.log($"Prevented planets with no farm plots from warning about nothing being planted.");
    }

    // [HarmonyPrefix]
    // [HarmonyPatch(nameof(CheckHasFarms))]
    // public static bool CheckHasFarms(ref ColonyDiagnostic.DiagnosticResult __result) {
    //     if (!Options.Instance.BlockHasFarmsDiagnostic)
    //         return true;
    //     __result = new ColonyDiagnostic.DiagnosticResult(
    //         ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS);
    //     return false;
    // }

    [HarmonyPatch(typeof(FarmDiagnostic), nameof(FarmDiagnostic.CheckPlanted))]
    [HarmonyPrefix]
    public static bool CheckPlanted(ref ColonyDiagnostic.DiagnosticResult __result, List<PlantablePlot> ___plots) {
        if (___plots.Count == 0) {
            __result = new ColonyDiagnostic.DiagnosticResult(
                ColonyDiagnostic.DiagnosticResult.Opinion.Normal,
                UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS
            );
            return HarmonySkipMethod;
        }
        return HarmonyRunMethod;
    }
}
