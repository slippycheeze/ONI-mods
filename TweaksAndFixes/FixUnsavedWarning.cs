namespace SlippyCheeze.TweaksAndFixes;

[HarmonyPatch(typeof(PauseScreen), nameof(PauseScreen.ConfirmDecision))]
internal static class AutoConfirmQuitIfRecentlySaved {
    internal static void Prepare(MethodInfo target) {
        if (target == null)
            L.log($"PauseScreen.ConfirmDecision will be patched to auto-confirm quit if recently saved.");
    }

    internal static bool Prefix(
        // return value of the method is void, so I don't need to mess __result here.
        bool ___recentlySaved,
        string? questionText,
        string? alternateButtonText,
        System.Action? alternateButtonAction
    ) {
        bool isQuitConfirm = (questionText == STRINGS.UI.FRONTEND.MAINMENU.QUITCONFIRM ||
                              questionText == STRINGS.UI.FRONTEND.MAINMENU.DESKTOPQUITCONFIRM);
        bool isQuitWithoutSaveButton = alternateButtonText == STRINGS.UI.FRONTEND.MAINMENU.QUIT;

        if (___recentlySaved && isQuitConfirm && isQuitWithoutSaveButton && alternateButtonAction != null) {
            // all the conditions to skip the confirmation are present, so pretend the
            // "QUIT WITHOUT SAVING" button was clicked, and don't bother invoking the confirmation
            // dialog at all.
            alternateButtonAction();
            return HarmonySkipMethod;
        }

        // run the original, we didn't hit our "safe to quit without confirm" conditions.
        return HarmonyRunMethod;
    }
}
