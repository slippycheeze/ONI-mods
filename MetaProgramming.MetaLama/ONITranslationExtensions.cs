using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using Metalama.Framework.Eligibility;

namespace SlippyCheeze.MetaProgramming.MetaLama;

[CompileTime]
public class ONITranslationExtensions: IAspect<INamedType> {
    public void BuildEligibility(IEligibilityBuilder<INamedType> builder) {
        // yay. metalama make their internal rules available to build from.  nice.
        builder.AddRule(EligibilityRuleFactory.GetAdviceEligibilityRule(AdviceKind.IntroduceField));

        builder.MustBeStatic();  // not static, not welcome. :)
        builder.MustSatisfy(t => t.TypeKind is TypeKind.Class, t => $"'{t}' must be a class");

        // be opinionated, it fine. :)
        builder.MustHaveAccessibility(Accessibility.Public);
        builder.MustBePartial();

        // ...extremely opinionated!
        builder.MustSatisfy(t => t.Name == "MODSTRINGS", t => $"'t' must be named 'MODSTRINGS'");
    }

    public void BuildAspect(IAspectBuilder<INamedType> builder) {
        BuildKeysFor(builder, builder.Target);
    }

    internal void BuildKeysFor(IAspectBuilder<INamedType> builder, INamedType type) {
        builder.IntroduceField(
            fieldName:  "prefix",
            fieldType:  typeof(string),
            scope:      IntroductionScope.Static,
            buildField: (f) => {
                f.Writeability = Writeability.ConstructorOnly;
                f.Accessibility = Accessibility.Internal;

                // I wish there was a cleaner way to strip the namespace, but whatever.
                string value = FullNameWithoutNamespace(type);
                if (value.StartsWith("MODSTRINGS"))
                    value = value.Substring(3);

                f.InitializerExpression = ExpressionFactory.Literal(value);
            }
        );

        // Klei just shove stuff right in the root, no namespace. :)
        IType LocString = TypeFactory.GetType("LocString");
        // NOTE: **MUST** use `.Equals`, not `==`, to do this comparison: you get reference equality
        // with `==` and that fails to work as expected.
        foreach (var field in type.AllFields.Where(field => field.Type.Equals(LocString))) {
            // internal static readonly string WHATEVER_key = "STRINGS.blah.blah.blah";
            builder.IntroduceField(
                fieldName:  $"{field.Name}_key",
                fieldType:  typeof(string),
                scope:      IntroductionScope.Static,
                buildField: (f) => {
                    f.Writeability = Writeability.ConstructorOnly;
                    f.Accessibility = Accessibility.Internal;

                    string value = $"{FullNameWithoutNamespace(type)}.{field.Name}";

                    if (value.StartsWith("MODSTRINGS"))
                        value = value.Substring(3);

                    f.InitializerExpression = ExpressionFactory.Literal(value);
                }
            );
        }

        foreach (var subType in type.Types.Where(t => t.TypeKind == TypeKind.Class)) {
            BuildKeysFor(builder.With(subType), subType);
        }
    }

    public static string FullNameWithoutNamespace(INamedType type) {
        if (type.ContainingNamespace is INamespace ns) {
            return type.FullName.Substring(ns.FullName.Length + 1);
        }
        return type.FullName;
    }
}
