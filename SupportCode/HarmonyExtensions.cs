using System.Reflection.Emit;

namespace SlippyCheeze.SupportCode;

public static class GlobalHarmonyHelper {
    // I can never, ever remember which value is "run" or "skip",
    // so let us use a nice constant instead to show intent.
    public const bool HarmonySkipMethod = false;
    public const bool HarmonyRunMethod  = true;


    public delegate CodeMatcher wrappedTranspilerMethodDelegate(CodeMatcher matcher);

    public static IEnumerable<CodeInstruction> Transpile(
        string? comment,
        IEnumerable<CodeInstruction> code,
        ILGenerator generator,
        MethodBase target,
        wrappedTranspilerMethodDelegate body,
        [CallerLineNumber] int line = -1,
        [CallerFilePath] string? path = null
    ) {
        // I apparently incorrectly assumed that the CodeMatcher starts at, well, the start of the
        // function.  This is, in fact, not true!  It starts invalid, and needs to be given *some*
        // sort of explicit instruction about where to move before it becomes value.
        //
        // Which, for me, sucks, so ... start at the start, neh? :)
        CodeMatcher matcher = new CodeMatcher(code, generator).Start();
        try {
            if (comment != null) {
                L.log(target == null ? comment : $"patching {target.ShortDescription()} to {comment}", line, path);
            }
            matcher = body(matcher);
            return matcher.ThrowIfNull(path: path, line: line).Instructions();
        } catch (Exception e) {
            L.error($"Exception in transpiler for {target.ShortDescription()}, rethrowing\n{e}", line, path);
            throw;
        }
    }

    public static IEnumerable<CodeInstruction> Transpile(
        IEnumerable<CodeInstruction> code,
        ILGenerator generator,
        MethodBase target,
        wrappedTranspilerMethodDelegate body,
        [CallerLineNumber] int line = -1,
        [CallerFilePath] string? path = null
    ) => Transpile(null, code, generator, target, body, line, path);


    public static IEnumerable<CodeInstruction> Transpile(
        IEnumerable<CodeInstruction> code,
        ILGenerator generator,
        MethodBase target,
        MethodInfo body,
        [CallerLineNumber] int line = -1,
        [CallerFilePath] string? path = null
    ) => Transpile(
        null,
        code, generator, target,
        (wrappedTranspilerMethodDelegate)body.CreateDelegate(typeof(wrappedTranspilerMethodDelegate), null),
        line, path
    );

    public static IEnumerable<CodeInstruction> Transpile(
        string? comment,
        IEnumerable<CodeInstruction> code,
        ILGenerator generator,
        MethodBase target,
        MethodInfo body,
        [CallerLineNumber] int line = -1,
        [CallerFilePath] string? path = null
    ) => Transpile(
        comment,
        code, generator, target,
        (wrappedTranspilerMethodDelegate) body.CreateDelegate(typeof(wrappedTranspilerMethodDelegate), null),
        line, path
    );
}


// some helpful extras I wrote myself.
public static class HarmonyExtras {
    public static CodeMatcher DumpInstructionsToLog(
        this CodeMatcher matcher,
        string? message = null,
        int back    = 5,
        int forward = 5,
        [CallerFilePath] string? path = null,
        [CallerLineNumber] int line = -1
    ) {
        if (matcher.IsInvalid) {
            L.debug($"CodeMatcher is in an invalid position", line, path);
            return matcher;
        }

        StringBuilder report = new();
        report.AppendLine(message ?? "Dumping instructions for debugging");

        int start = (matcher.Pos - back).Clamp(0, matcher.Length - 1);
        int end   = (matcher.Pos + forward).Clamp(0, matcher.Length - 1);
        matcher.InstructionsInRange(start, end).ForEach(
            (instr, n) => {
                int here = n + start;
                report
                    .AppendFormat("{0} {1,3}  {2}", here == matcher.Pos ? "==>" : "   ", here, instr)
                    .AppendLine();
            }
        );

        L.debug(report.ToString(), line, path);
        return matcher;
    }


    public static CodeMatcher MatchStartForwardAndRemove(this CodeMatcher matcher, params CodeMatch[] matches)
        => matcher.MatchStartForwardAndRemove("failed to match code forward", matches);
    public static CodeMatcher MatchStartForwardAndRemove(this CodeMatcher matcher, string explanation, params CodeMatch[] matches)
        => matcher
        .MatchStartForward(matches)
        .ThrowIfInvalid(explanation)
        .RemoveInstructions(matches.Length);
}


// apparently our Harmony is too early for some useful bits, so backport or implement them myself.
public struct HarmonyBackports {
    public struct AccessTools {
        public static IEnumerable<Type> InnerTypes(Type type) => type.GetNestedTypes(HarmonyLib.AccessTools.all);
    }
}

public static class CodeInstructionBackports {
    /// <summary>Creates a CodeInstruction loading a local with the given index, using the shorter forms when possible</summary>
    /// <param name="index">The index where the local is stored</param>
    /// <param name="useAddress">Use address of local</param>
    /// <returns></returns>
    /// <seealso cref="CodeInstructionExtensions.LocalIndex(CodeInstruction)"/>
    public static CodeInstruction LoadLocal(int index, bool useAddress = false)
    {
        if (useAddress)
        {
            if (index < 256) return new CodeInstruction(OpCodes.Ldloca_S, Convert.ToByte(index));
            else return new CodeInstruction(OpCodes.Ldloca, index);
        }
        else
        {
            if (index == 0) return new CodeInstruction(OpCodes.Ldloc_0);
            else if (index == 1) return new CodeInstruction(OpCodes.Ldloc_1);
            else if (index == 2) return new CodeInstruction(OpCodes.Ldloc_2);
            else if (index == 3) return new CodeInstruction(OpCodes.Ldloc_3);
            else if (index < 256) return new CodeInstruction(OpCodes.Ldloc_S, Convert.ToByte(index));
            else return new CodeInstruction(OpCodes.Ldloc, index);
        }
    }

}

