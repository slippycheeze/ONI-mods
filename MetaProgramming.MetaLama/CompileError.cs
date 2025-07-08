using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;

namespace SlippyCheeze.MetaProgramming.Metalama;

// central stash of compilation errors, for when things go wrong. :)
[CompileTime]
public static class CompileError {
    public static readonly DiagnosticDefinition<string> MSBuildPropertyMissing = new(
        "SC0001",                // stupid ID number thing.  why not a meaningful name?
        Severity.Error,         // build infrastructure is broken
        "The MSBuild Property '{0}' was not exposed to the compiler.  Fix build infrastructure.",
        null,                   // use the previous message also.
        "SlippyCheeze.MetaProgramming.Metalama"
    );

    public static readonly DiagnosticDefinition<(string, string)> HarmonyMethodIsNotStatic = new(
        "SC0002",
        Severity.Error,
        "The {0} method '{1}' is not static.  This will cause an exception when patching!",
        null,
        "SlippyCheeze.MetaProgramming.Metalama"
    );

    public static readonly DiagnosticDefinition<(string harmonyType, string method, string valid, IType invalid)>
        HarmonyMethodMustReturn = new(
            "SC0003",
            Severity.Error,
            "The {0} method '{1}' must return {2}, not {3}.",
            null,
            "SlippyCheeze.MetaProgramming.Metalama"
        );
}
