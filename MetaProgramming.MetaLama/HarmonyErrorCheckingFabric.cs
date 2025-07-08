using System.Diagnostics;

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using Metalama.Framework.Fabrics;

using SlippyCheeze.MetaProgramming.Metalama;

namespace SlippyCheeze.MetaProgramming.MetaLama;

public class HarmonyErrorCheckingFabric: TransitiveProjectFabric {
    public override void AmendProject(IProjectAmender project) {
        // I occasionally mess up, and forget to mark a Harmony patch method as static, which really
        // rather hurts and all that.  So, easy enough to add some error checking with MetaLama,
        // turning that into a much more pleasant compiler error, not runtime throw.
        var HarmonyPatch = TypeFactory.GetType("HarmonyLib.HarmonyPatch");

        var methods = project
            .SelectTypes()
            .Where(type => type.Attributes.Any(HarmonyPatch))
            .SelectMany(type => type.Methods);

        var prepare = methods.HarmonyMethods("Prepare");

        prepare.Where(m => !m.IsStatic).ReportDiagnostic(IsNotStaticError("HarmonyPrepare"));
        //     method => CompileError.HarmonyMethodIsNotStatic.WithArguments(("HarmonyPrepare", method.ShortDescription()))
        // );

        prepare.Where(m => !m.ReturnType.IsVoidOrConvertibleTo(typeof(bool))).ReportDiagnostic(
            method => CompileError.HarmonyMethodMustReturn.WithArguments(
                ("HarmonyPrepare", method.ShortDescription(), "void or bool", method.ReturnType)
            )
        );

        string[] simpleChecks = [
            "Cleanup", "TargetMethod", "TargetMethods", "Prefix", "Postfix", "Finalizer", "Transpiler"
        ];

        foreach (string Name in simpleChecks)
            methods.HarmonyMethods(Name).Where(m => !m.IsStatic).ReportDiagnostic(IsNotStaticError($"Harmony{Name}"));
    }

    private Func<IMethod, IDiagnostic> IsNotStaticError(string name)
        => (method) => CompileError.HarmonyMethodIsNotStatic.WithArguments((name, method.ShortDescription()));
}


[CompileTime]
internal static class HarmonyQueryExtensions {
    public static IQuery<IMethod> HarmonyMethods(this IQuery<IMethod> query, string Name) {
        string HarmonyAttributeName = $"HarmonyLib.Harmony{Name}";
        IType? HarmonyAttribute     = TypeFactory.GetType(HarmonyAttributeName);
        if (HarmonyAttribute is null)
            throw new InvalidOperationException($"TypeFactory.GeTType(\"{HarmonyAttributeName}\") => null");

        return query.Where(method => method.Name == Name || method.Attributes.Any(HarmonyAttribute));
    }
}
