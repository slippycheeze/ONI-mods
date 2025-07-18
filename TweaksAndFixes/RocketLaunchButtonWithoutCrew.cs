using STRINGS;

namespace SlippyCheeze.TweaksAndFixes;

// https://forums.kleientertainment.com/klei-bug-tracker/oni/rocket-is-loading-crew-even-if-it-has-no-crew-assigned-r40371/
//
// It is possible to hit 'Begin Launch Sequence' and get a rocket to status 'Loading Crew...' even
// if the rocket has no crew assigned, thus waiting there forever. Disable the button in that case
// and provide 'No Crew Assigned' status.
[HarmonyPatch(typeof(LaunchButtonSideScreen), nameof(LaunchButtonSideScreen.Refresh))]
public static class DisableLaunchButtonWhenNoCrewAssigned {
    internal static void Prepare(MethodInfo target) {
        if (target == null)
            L.log($"Warn when a rocket launch is started with no crew assigned.");
    }

    public static void Postfix(LaunchButtonSideScreen __instance) {
        var screen = __instance;  // just a nicer name, yo.

        if (!screen.launchButton.isInteractable)
            return;

        bool waiting = (
            screen.statusText.text == UI.UISIDESCREENS.LAUNCHPADSIDESCREEN.STATUS.LOADING_CREW ||
            screen.statusText.text == UI.UISIDESCREENS.LAUNCHPADSIDESCREEN.STATUS.READY_FOR_LAUNCH
        );

        if (!waiting || screen.rocketModule == null || screen.selectedPad == null)
            return;

        var passengers = screen.rocketModule.GetComponent<PassengerRocketModule>();
        if (passengers == null)
            return;

        int expected = passengers.GetCrewBoardedFraction().second;
        if (expected > 0)
            return;

        screen.launchButton.isInteractable = false;
        screen.statusText.text = MODSTRINGS.UI.UISIDESCREENS.LAUNCHPADSIDESCREEN.STATUS.NO_CREW_ASSIGNED;

        screen.launchButton.GetComponentInChildren<LocText>()
            ?.text = UI.UISIDESCREENS.LAUNCHPADSIDESCREEN.LAUNCH_BUTTON;

        screen.launchButton.GetComponentInChildren<ToolTip>()
            ?.toolTip = UI.UISIDESCREENS.LAUNCHPADSIDESCREEN.LAUNCH_BUTTON_NOT_READY_TOOLTIP;
    }
}
