// Some chores like going to the toilet and catching breath have a higher priority than eating, and
// at least on the highest hunger difficulty setting it happens quite often that at the beginning of
// the downtime dupes run to the toilet and become starving, which triggers a warning, even though
// those dupes have eating queued right after the toilet and still have more than enough calories to
// make it.
//
// Since going to eat already disables the starving notification, disable it also if the going to
// eat task is only preceded by these high-priority tasks and the dupe still has enough calories.
namespace SlippyCheeze.TweaksAndFixes;

// hooking into IsEating is the easiest way to get the right state transitions to happen.
[HarmonyPatch(typeof(CalorieMonitor.Instance), nameof(CalorieMonitor.Instance.IsEating))]
public static class CalorieMonitor_Instance_Patch {
    internal static void Prepare(MethodInfo target) {
        if (target == null)
            L.log($"Smarter warnings about starvation while Dupes go through their morning routine.");
    }

    private static Urge[] allowedUrges => field ??= [
        Db.Get().Urges.Pee,
        Db.Get().Urges.RecoverBreath,
        Db.Get().Urges.Emote,
        Db.Get().Urges.EmoteHighPriority,
    ];

    private static bool isAllowedChore(Chore chore) => allowedUrges.Contains(chore.choreType.urge);


    public static void Postfix(CalorieMonitor.Instance __instance, ref bool __result) {
        if (__result || !__instance.IsStarving())
            return;

        if ((__instance.calories.value / __instance.calories.GetMax()) < 0.20f)
            return; // Less than 800 kcal, do not adjust anything.

        ChoreDriver choreDriver = __instance.master.GetComponent<ChoreDriver>();
        if (choreDriver.HasChore() && !isAllowedChore(choreDriver.GetCurrentChore()))
            return;

        ChoreConsumer choreConsumer = __instance.GetComponent<ChoreConsumer>();
        ChoreConsumer.PreconditionSnapshot lastPreconditionSnapshot = choreConsumer.GetLastPreconditionSnapshot();

        if (lastPreconditionSnapshot.doFailedContextsNeedSorting) {
            lastPreconditionSnapshot.failedContexts.Sort();
            lastPreconditionSnapshot.doFailedContextsNeedSorting = false;
        }

        var contexts = lastPreconditionSnapshot.failedContexts
            .Concat(lastPreconditionSnapshot.succeededContexts)
            .Reverse();

        foreach (var context in contexts) {
            Chore chore = context.chore;
            if (chore.driver != choreConsumer.choreDriver)
                continue;

            // If all chores above eating are allowed ones (or do not apply), make the function
            // claim the dupe is eating to stay out of the alert state.
            if (chore.choreType.urge == Db.Get().Urges.Eat) {
                __result = true;
                return;
            }

            // if it is allowed, continue, otherwise...
            if (isAllowedChore(chore))
                continue;

            // Check IsPotentialSuccess() only after checking the eat chore, as it may return false
            // for that one because of being "low priority" while going to the toilet or something.
            if (!context.IsPotentialSuccess())
                continue;

            // Found a chore that's not eating and not the allowed one, do not change anything
            // and just return.
            return;
        }
    }
}
