using Metalama.Framework.Aspects;
using Metalama.Framework.Diagnostics;

namespace SlippyCheeze.MetaProgramming.Metalama;

// central stash of compilation errors, for when things go wrong. :)
[CompileTime]
public static class CompileError {
    public static DiagnosticDefinition<string> MSBuildPropertyMissing = new(
        "SC001",                // stupid ID number thing.  why not a meaningful name?
        Severity.Error,         // build infrastructure is broken
        "The MSBuild Property '{0}' was not exposed to the compiler.  Fix build infrastructure.",
        "MSBuild Property not exposed to compiler: fix build infrastructure.",
        "SlippyCheeze.MetaProgramming.Metalama"
    );
}
