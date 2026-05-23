using System.Diagnostics;
using System.Reflection.Emit;

namespace SlippyCheeze.DeveloperHelpers;

[HarmonyPatch]
internal class FindLogSpamCulprits {
    internal static List<LogSpamMatch> matches = [
        // `"string"`, or `new Regex("...")`, here.
    ];



    private static IEnumerable<MethodBase> targetMethods = typeof(Console)
        .GetDeclaredMethods()
        .Where(method => method.Name == "WriteLine");

    internal static IEnumerable<MethodBase> TargetMethods() => targetMethods;
    internal static bool Prepare(MethodBase target) {
        if (matches.Count == 0) {
            L.log($"No log spam patterns found, will not hook the Console.WriteLine methods");
            return false;
        }

        if (target == null)
            L.log($"Hooking {targetMethods.Count()} Console.WriteLine methods");
        return targetMethods.Any();
    }

    // avoid infinite recursion when we print the matching message ourselves...
    private static bool recursiveCall = false;

    internal static bool Prefix(MethodBase __originalMethod, object[] __args) {
        if (recursiveCall)
            return HarmonyRunMethod;

        bool prune = false;

        try {
            recursiveCall = true;

            if (__args.Length == 0)
                return HarmonyRunMethod;

            object arg0 = __args[0];
            if (arg0 is not string string0)
                return HarmonyRunMethod;

            foreach (var match in matches) {
                if (match.IsFinished)
                    prune = true;
                else if (match.DumpStackIfMatches(string0))
                    return HarmonySkipMethod;
            }

            return HarmonyRunMethod;
        }
        finally {
            recursiveCall = false;

            if (prune)
                matches.RemoveAll(m => m.IsFinished);

        }
    }


    internal class LogSpamMatch(Regex rx) {
        private readonly Regex pattern = rx;
        private int seen = 0;

        // string constructor, with implicit escaping...
        internal LogSpamMatch(string text) : this(new Regex(Regex.Escape(text))) { }

        // conversions, how convenient. :)
        public static implicit operator LogSpamMatch(string s) => new(s);
        public static implicit operator LogSpamMatch(Regex r)  => new(r);

        internal bool IsFinished => seen > 5;

        // and a nice helper to match things.
        internal bool DumpStackIfMatches(string text) {
            if (IsFinished || !pattern.IsMatch(text))
                return false;

            seen++;

            // our *known* level of nesting is 3, ignore those lowest level method calls.
            //
            // NOTE: this doesn't have a valid ILOffset in the StackFrame, but I can see no way to
            // obtain that.  Apparently we only get it when an exception hits the top level or similar. :(
            StackTrace stack = new(skipFrames: 3, fNeedFileInfo: true);
            L.debug($"matched log spam target (seen={seen}):\n{text}\n{stack}\n");

            return true;
        }
    }
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
