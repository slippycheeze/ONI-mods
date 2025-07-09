using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Eligibility;

namespace SlippyCheeze.MetaProgramming.MetaLama;

[CompileTime]
public static partial class EligibilityExtensions {
    public static void MustBePartial(this IEligibilityBuilder<IMemberOrNamedType> builder)
        => builder.MustSatisfy(d => d.IsPartial, d => $"{d} must be partial");

    public static void MustNotBeReadOnly(this IEligibilityBuilder<IMethod> builder)
        => builder.MustSatisfy(m => !m.IsReadOnly, m => $"{m} must not be readonly");
}
