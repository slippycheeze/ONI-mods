using System.Reflection.Emit;

namespace SlippyCheeze.TweaksAndFixes;

// https://forums.kleientertainment.com/klei-bug-tracker/oni/false-breathability-diagnostic-warnings-r41463/
[HarmonyPatch]
internal static class FixLowBreathabilityDiagnostics {
    internal static void Prepare(MethodInfo target) {
        if (target == null)
            L.log($"Do not show a 'Low Breathability' diagnostic waring without sufficient data.");
    }

    [HarmonyPatch(typeof(BreathabilityDiagnostic), nameof(BreathabilityDiagnostic.CheckLowBreathability))]
    [HarmonyPrefix]
    [HarmonyPriority(Priority.Last)]  // give others as much chance to be called as possible.
    internal static bool CheckSampleCountForColonyDiagnostic(Tracker ___tracker, ref ColonyDiagnostic.DiagnosticResult __result) {
        if (___tracker.GetDataTimeLength() < 10f) {
            __result = new ColonyDiagnostic.DiagnosticResult(
                ColonyDiagnostic.DiagnosticResult.Opinion.Normal,
                STRINGS.UI.COLONY_DIAGNOSTICS.NO_DATA
            );
            return HarmonySkipMethod;
        }
        return HarmonyRunMethod;
    }

    [HarmonyPatch(typeof(BreathabilityTracker), nameof(BreathabilityTracker.UpdateData))]
    [HarmonyTranspiler]
    internal static IEnumerable<CodeInstruction> NoSamplesOnEmptyPlanets(
        IEnumerable<CodeInstruction> code,
        ILGenerator generator,
        MethodBase target
    ) {
        L.log($"Patching {target.ShortDescription()} to skip recording zero samples on empty planets.");
        try {
            CodeMatch[] sequence = [
                // IL_001e: ldarg.0      // this
                new(OpCodes.Ldarg_0),
                // IL_001f: ldc.r4       0.0
                new(OpCodes.Ldc_R4, 0.0f),
                // IL_0024: call         instance void Tracker::AddPoint(float32)
                new(CodeMatch.Call(typeof(Tracker), nameof(Tracker.AddPoint), [typeof(float)])),
            ];

            CodeMatcher matcher = new(code, generator);

            return matcher
                .MatchStartForward(sequence)
                .ThrowIfInvalid("could not find the invocation of AddPoint with a constant zero value.")
                .RemoveInstructions(sequence.Length)
                .Instructions();
        } catch (Exception e) {
            L.error($"Exception in transpiler, returning original code\n{e}");
            return code;
        }
    }
}
