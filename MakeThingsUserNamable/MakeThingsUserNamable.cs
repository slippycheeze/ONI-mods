namespace SlippyCheeze.MakeThingsUserNamable;

[HarmonyPatch]
internal static class MakeThingsUserNamable {
    private static Type[] targetTypes = [
        typeof(ObjectDispenserConfig),
        typeof(SolidConduitInboxConfig),
        typeof(StorageTileConfig),
        AccessTools.TypeByName("MoveThisHere.HaulingPointConfig"),
    ];

    private static IEnumerable<string> diagnose(Type type) {
        if (!type.Name.EndsWith("Config")) {
            yield return "name doesn't end with 'Config'";

            string configClass = type.Name + "Config";
            if (AccessTools.TypeByName(configClass) != null)
                yield return $"{configClass} exists";
        }
        if (type.GetInterface(nameof(IBuildingConfig)) == null)
            yield return "not IBuildingConfig";
        if (type.DeclaredMethod("CreateBuildingDef") == null)
            yield return "no CreateBuildingDef method";
    }

    private static List<MethodInfo> targetMethods {
        get {
            if (field == null) {
                var (bad, good) = targetTypes
                    .Where(type => type != null)
                    .Select(type => new { type, method = type.DeclaredMethod("DoPostConfigureComplete") })
                    .Partition(type => type.method == null);

                if (bad.Any()) {
                    string diags = bad.Select(data => diagnose(data.type)).Humanize();
                    string count = "types".ToQuantity(bad.Count());
                    throw new InvalidOperationException($"{count} types exist, without DoPostConfigureComplete: {diags}");
                }

                field = [.. good.Select(data => data.method)];
            }
            return field;
        }
    }

    public static IEnumerable<MethodBase> TargetMethods() => targetMethods;
    public static bool Prepare(MethodBase target) {
        if (target == null)
            L.log($"I intend to make {"types".ToQuantity(targetMethods.Count)} UserNamable");
        return targetMethods.Count > 0;
    }

    // indexed argument to avoid crashing if one random mod uses a different name.
    public static void Postfix([HarmonyArgument(0)] GameObject? go) {
        if (go == null) return;
        if (go.GetComponent<UserNameable>() != null) return;

        L.log($"making {go.PrefabID()} UserNameable");
        go.AddComponent<UserNameable>();
    }
}
