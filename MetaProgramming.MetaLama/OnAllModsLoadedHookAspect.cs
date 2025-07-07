using System.Diagnostics;

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;

namespace SlippyCheeze.MetaProgramming.Metalama;

[CompileTime]
public class OnAllModsLoadedHookAspect: IAspect<IMethod> {
    public void BuildEligibility(IEligibilityBuilder<IMethod> builder) {
        Debugger.Break();

        builder.AddRule(EligibilityRuleFactory.GetAdviceEligibilityRule(AdviceKind.OverrideMethod));

        builder.MustBeStatic();
        builder.MustHaveAccessibility(Accessibility.Public);

        builder.MustSatisfy(
            static method => method.MethodKind == MethodKind.Default,
            static method => $"'{method}' must be a normal method, not {method.Object.MethodKind}"
        );

        builder.MustSatisfy(
            static method => method is not IGeneric { IsGeneric: true },
            static method => $"'{method}' must not be a generic method"
        );

        builder.ReturnType().MustSatisfy(
            type => type.SpecialType == SpecialType.Void,
            type => $"OnModLoadedHook must return void, not '{type.Object}'"
        );

        builder.MustSatisfy(
            static method => method.Parameters.Count == 1,
            static method => $"{method} must not have one parameter"
        );
    }

    // we are just here for the eligibility builder. :)
    public void BuildAspect(IAspectBuilder<IMethod> builder) {}
}
