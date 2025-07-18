using System.Reflection.Emit;

namespace SlippyCheeze.TweaksAndFixes;

// The game code uses LT (less-than) instead of GTE for checking minor exposure, which is fixed.
//
// The other issues previously addressed no longer seem relevant: the code bases the check on
// reaching half the exposure required for it to be a minor issue, rather than on an instantaneous
// exposure count.
//
// https://forums.kleientertainment.com/klei-bug-tracker/oni/check-exposed-radiation-diagnostic-has-inverted-condition-r40487/
[HarmonyPatch(typeof(RadiationDiagnostic), nameof(RadiationDiagnostic.CheckExposure))]
public static class RadiationDiagnosticExposureFixes {
    internal static IEnumerable<CodeInstruction> NoSamplesOnEmptyPlanets(
        IEnumerable<CodeInstruction> code,
        ILGenerator generator,
        MethodBase target
    ) {
        L.log($"Patching {target.ShortDescription()} to check overall exposure, and fix minor exposure warning bug.");
        try {
            CodeMatcher matcher = new(code, generator);

            // The use of `RadiationMonitor.COMPARE_LT_MINOR` should be `.COMPARE_GTE_MINOR`, which
            // involves changing this code:
            //
            // IL_00ba: ldsfld       class StateMachine`4/Parameter`1/Callback<class RadiationMonitor, class RadiationMonitor/Instance, class ['Assembly-CSharp-firstpass']IStateMachineTarget, object, float32> RadiationMonitor::COMPARE_LT_MINOR
            //
            // Everything else can remain the same, and since this is the one and only use of the
            // check in the code, I'm happy to YOLO it and pretend that I don't need to check
            // context to verify the use is the expected one. :)
            matcher
                .Start()
                .MatchStartForward(
                    new CodeMatch(CodeMatch.LoadField(typeof(RadiationMonitor), nameof(RadiationMonitor.COMPARE_LT_MINOR)))
                )
                .ThrowIfInvalid("Unabled to find the load of RadiationMonitor.COMPARE_LT_MINOR")
                .SetInstruction(CodeMatch.StoreField(typeof(RadiationMonitor), nameof(RadiationMonitor.COMPARE_GTE_MINOR)))
                .DumpInstructionsToLog();

            return matcher.Instructions();
        } catch (Exception e) {
            L.error($"Exception in transpiler, returning original code\n{e}");
            return code;
        }
    }
}
