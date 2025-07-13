using System.Reflection.Emit;

namespace SlippyCheeze.DeveloperHelpers;

// [HarmonyPatch]
// internal class FindLogSpamCulprits {
//     // disabled for now, I found all the culprits!
//     public static bool Prepare() => false;

//     private static IEnumerable<MethodBase> targetMethods = typeof(Console)
//         .GetDeclaredMethods()
//         .Where(method => method.Name == "WriteLine");

//     internal static IEnumerable<MethodBase> TargetMethods() => targetMethods;
//     internal static bool Prepare(MethodBase target) {
//         if (target == null)
//             L.log($"Hooking {targetMethods.Count()} Console.WriteLine methods");
//         return targetMethods.Any();
//     }

//     internal static void Postfix(MethodBase __originalMethod, object[] __args) {
//         if (__args.Length == 0) return;
//         object arg0 = __args[0];

//         if (arg0 is string string0) {
//             // avoid recursing forever if I log the same thing I'm hunting for!
//             if (string0.Contains("SlippyCheezePersonal")) return;
//         }
//     }

//     private static void DumpStack(string msg) {
//         StackTrace stack = new(skipFrames: 2);  // our *known* level of nesting.
//         L.debug($"{msg}\n\n{stack}\n\n");
//     }
// }


[HarmonyPatch(typeof(TargetScreen), nameof(TargetScreen.SetTarget))]
internal static class SilenceTargetScreenLogSpam {
    internal static IEnumerable<CodeInstruction> Transpiler(
        IEnumerable<CodeInstruction> code,
        ILGenerator generator,
        MethodBase target
    ) => Transpile(
        "remove log-spamming with 'False'",
        code, generator, target,
        (matcher) => matcher
        .ThrowIfNotMatch(
            "The 'False' log-spam code has changed?",
            // IL_0000: ldarg.1      // target
            new(OpCodes.Ldarg_1),
            // IL_0001: call         bool UnityEngine.Object::op_Implicit(class Object)
            new(i => i.ToString().Contains(@"call static System.Boolean UnityEngine.Object::op_Implicit")),
            // IL_0006: call         void [mscorlib]System.Console::WriteLine(bool)
            new(i => i.Calls(typeof(Console).DeclaredMethod(nameof(Console.WriteLine), [typeof(bool)])))
        )
        .RemoveInstructions(3)
    );
}


[ModPatch("asquared31415.TrafficVisualizer", steamID: 2208398090)]
internal static partial class SilenceTrafficVisualizerLogSpam {
    [Memoize]
    private static MethodBase? targetMethod => AccessTools
        .TypeByName("TrafficVisualizer.TransitionDriver_EndTransition_Patch")
        ?.DeclaredMethod("Prefix");

    public static bool Prepare(MethodBase _) => targetMethod != null;
    public static MethodBase? TargetMethod() => targetMethod;

    internal static IEnumerable<CodeInstruction> Transpiler(
        IEnumerable<CodeInstruction> code,
        ILGenerator generator,
        MethodBase target
    ) => Transpile(
        code,
        generator,
        target,
        (matcher) => matcher.MatchStartForwardAndRemove(
            "Traffic Visualizer log spam code point",
            // IL_001b: ldarg.2      // ___transition
            new(OpCodes.Ldarg_2),
            // IL_001c: call         void ['Assembly-CSharp-firstpass']Debug::Log(object)
            new(CodeMatch.Call(typeof(Debug), nameof(Debug.Log), [typeof(object)]))
        )
    );
}
