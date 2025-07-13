using System.Reflection.Emit;

namespace SlippyCheeze.DeveloperHelpers;

[HarmonyPatch]
internal static class ExtraDebugInfoForKleiErrors {
    // add some extra debugging information to various points in the code that Klei log an error,
    // but don't tell me anything about what the root cause of the problem is, or even enough to
    // start guessing at it.
    [HarmonyPatch(typeof(Assets), nameof(Assets.GetPrefab))]
    [HarmonyPostfix]
    [HarmonyPriority(Priority.Last)]
    public static void AssetsMissingPrefab(Tag tag, GameObject __result) {
        if (__result is not null) return;
        if (tag == GameTags.Filter) return;  // not... really much I can do with this. :(
        L.debug($"\n{Environment.StackTrace}");
    }


    // Method:GeneratedBuildings.MakeBuildingAlwaysOperational(UnityEngine.GameObject go)
    // report the PrefabID of the thing it complains about.  ;___;
    [HarmonyPatch(typeof(GeneratedBuildings), nameof(GeneratedBuildings.MakeBuildingAlwaysOperational))]
    [HarmonyTranspiler]
    internal static IEnumerable<CodeInstruction> MakeBuildingAlwaysOperationalReportsPrefabID(
        IEnumerable<CodeInstruction> code,
        ILGenerator generator,
        MethodBase target
    ) => Transpile(
        "Report *which* building MakeBuildingAlwaysOperational was inappropriately called on",
        code, generator, target,
        (matcher) => matcher
        .MatchStartForward(
            // IL_001c: ldstr        "Do not call MakeBuildingAlwaysOperational directly if LogicInputPorts or LogicOutputPorts are defined. Instead set BuildingDef.AlwaysOperational = true"
            new(i => i.opcode == OpCodes.Ldstr && i.operand is string s && s.Contains("Do not call MakeBuildingAlwaysOperational")),
            // IL_0021: call         void ['Assembly-CSharp-firstpass']Debug::LogWarning(object)
            new(i => i.Calls(typeof(Debug).DeclaredMethod(nameof(Debug.LogWarning), [typeof(object)])))
        )
        .Advance(1)             // step past the string
        .InsertAndAdvance(
            new(OpCodes.Ldarg_0),  // GameObject go
            CodeInstruction.CallClosure(static (string msg, GameObject go) => $"{go.PrefabID()}: {msg}")
        )
    );
}
