namespace SlippyCheeze.TweaksAndFixes;

[HarmonyPatch(typeof(Notifier), nameof(Notifier.Add))]
internal static class SuppressUselessNotifications {
    internal static void Prepare(MethodInfo target) {
        if (target == null)
            L.log($"Will suppress all notifications for attribute increases, daily reports, and schedule alarms.");
    }

    // Like Ilunak, I thought about using a transpiler to remove the call posting the notification
    // from the methods, and then I looked at how much code it was, and the relative complexity, and
    // decided that instead I'd much rather do it at the choke-point of adding the notification.
    [HarmonyPriority(Priority.Last)]  // give others as much chance to be called as possible.
    internal static bool Prefix() {
        if (AttributeLevel_LevelUp.suppressNotifications)               return HarmonySkipMethod;
        if (ReportManager_OnNightTime.suppressNotifications)            return HarmonySkipMethod;
        if (ScheduleManager_PlayScheduleAlarm.suppressNotifications)    return HarmonySkipMethod;

        return HarmonyRunMethod;
    }

    // AttributeLevel.LevelUp - "Attribute increase"
    [HarmonyPatch(typeof(Klei.AI.AttributeLevel), nameof(Klei.AI.AttributeLevel.LevelUp))]
    internal static class AttributeLevel_LevelUp {
        internal static bool suppressNotifications = false;
        internal static void Prefix()  => suppressNotifications = true;
        internal static void Postfix() => suppressNotifications = false;
    }

    // ReportManager.OnNightTime - "Cycle {0} report ready"
    [HarmonyPatch(typeof(ReportManager), nameof(ReportManager.OnNightTime))]
    internal static class ReportManager_OnNightTime {
        internal static bool suppressNotifications = false;
        internal static void Prefix()  => suppressNotifications = true;
        internal static void Postfix() => suppressNotifications = false;
    }
        // ScheduleManager.PlayScheduleAlarm
    [HarmonyPatch(typeof(ScheduleManager), nameof(ScheduleManager.PlayScheduleAlarm))]
    internal static class ScheduleManager_PlayScheduleAlarm {
        internal static bool suppressNotifications = false;
        internal static void Prefix()  => suppressNotifications = true;
        internal static void Postfix() => suppressNotifications = false;
    }
}
