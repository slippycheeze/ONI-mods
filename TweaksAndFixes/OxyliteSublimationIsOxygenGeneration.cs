namespace SlippyCheeze.TweaksAndFixes;

[HarmonyPatch(typeof(Sublimates), nameof(Sublimates.Emit))]
internal static class OxyliteSublimationIsOxygenGeneration {
    internal static void Prepare(MethodInfo target) {
        if (target == null)
            L.log($"Oxylite Sublimation will now count towards oxygen generation");
    }

    internal static void Postfix(Sublimates __instance, float mass) {
        if (__instance.info.sublimatedElement == SimHashes.Oxygen)
            ReportManager.Instance.ReportValue(
                ReportManager.ReportType.OxygenCreated,
                mass,
                __instance.gameObject.GetProperName()
            );
    }
}
