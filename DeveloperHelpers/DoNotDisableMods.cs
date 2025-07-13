namespace SlippyCheeze.DeveloperHelpers;

[HarmonyPatch]
internal static class DoNotDisableMods {
    private static bool inCrashDialog = false;

    [HarmonyPatch(typeof(ReportErrorDialog), nameof(ReportErrorDialog.BuildModsList))]
    [HarmonyPrefix]
    private static void StartOfReportErrorDialogBuildModList() => inCrashDialog = true;

    [HarmonyPatch(typeof(ReportErrorDialog), nameof(ReportErrorDialog.BuildModsList))]
    [HarmonyFinalizer]
    private static void EndOfReportErrorDialogBuildModList(string ___m_stackTrace) {
        inCrashDialog = false;
        // ...since it doesn't seem to reliably make it into the logs for some reason???
        L.warn($"StackTrace from Klei ReportErrorDialog follows:\n{___m_stackTrace}");
    }

    // ...and the meat of the whole thing, where we just ignore what the game asks.
    [HarmonyPatch(typeof(KMod.Manager), nameof(KMod.Manager.EnableMod))]
    [HarmonyPrefix]
    [HarmonyPriority(Priority.Last)]
    private static bool IgnoreRequest(KMod.Label id, bool enabled) {
        if (inCrashDialog && !enabled) {
            L.warn($"did not disable '{id}'");
            return HarmonySkipMethod;
        }
        return HarmonyRunMethod;
    }
}
