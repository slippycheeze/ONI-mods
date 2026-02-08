using System.Runtime.CompilerServices;

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using Metalama.Framework.Eligibility;

namespace SlippyCheeze.MetaProgramming.MetaLama;

public class MemoizeAspect: IAspect<IMethod>, IAspect<IProperty> {
    public void BuildEligibility(IEligibilityBuilder<IMethod> builder) {
        builder.DeclaringType().MustSatisfy(t => !t.IsReadOnly, t => $"{t} with a [Memoize] annotation must not be readonly");
        builder.AddRule(EligibilityRuleFactory.GetAdviceEligibilityRule(AdviceKind.OverrideMethod));
        builder.MustSatisfy(m => !m.IsReadOnly, m => $"{m} must not be readonly");
        builder.MustSatisfy(m => m.MethodKind == MethodKind.Default, m => $"{m} must be a normal method");
        builder.ReturnType().MustSatisfy(t => t.SpecialType != SpecialType.Void, t => $"{t} must not be void");
        builder.MustSatisfy(m => m.Parameters.Count == 0, m => $"{m} must not have any parameters");
    }

    public void BuildEligibility(IEligibilityBuilder<IProperty> builder) {
        builder.DeclaringType().MustSatisfy(t => !t.IsReadOnly, t => $"{t} with a [Memoize] annotation must not be readonly");
        builder.AddRule(EligibilityRuleFactory.GetAdviceEligibilityRule(AdviceKind.OverrideFieldOrPropertyOrIndexer));
        builder.MustSatisfy(p => p.Writeability == Writeability.None, p => $"{p} must not have a setter");
    }

    public void BuildAspect(IAspectBuilder<IMethod> builder) {
        IField field = IntroduceBackingField(builder, builder.Target.ReturnType, out bool isBoxed);
        builder.Override(
            template: isBoxed ? nameof(BoxingTemplate) : nameof(NonNullableReferenceTypeTemplate),
            args: new { field, T = builder.Target.ReturnType }
        );
    }

    public void BuildAspect(IAspectBuilder<IProperty> builder) {
        IField field = IntroduceBackingField(builder, builder.Target.Type, out bool isBoxed);
        builder.OverrideAccessors(
            getTemplate: isBoxed ? nameof(BoxingTemplate) : nameof(NonNullableReferenceTypeTemplate),
            args: new { field, T = builder.Target.Type }
        );
    }

    private IField IntroduceBackingField<T>(T builder, IType type, out bool isBoxed) where T: IAspectBuilder<IMember> {
        // If the type is not a non-nullable reference type we need to box it in order to store it
        // effectively, and be able to use correct "the first value computed is *always* the value
        // returned" semantics.
        if (type is { IsReferenceType: true, IsNullable: false }) {
            isBoxed = false;
        } else {
            type = ((INamedType) TypeFactory.GetType(typeof(StrongBox<>))).WithTypeArguments(type);
            isBoxed = true;
        }

        return builder.With(builder.Target.DeclaringType)
            .IntroduceField(
                fieldName:  $"_{builder.Target.Name}",
                fieldType:  type,
                scope:      IntroductionScope.Target,
                whenExists: OverrideStrategy.Fail,
                buildField: m => {
                    m.Accessibility = Accessibility.Private;
                    m.Writeability  = Writeability.All;
                }
            )
            .Declaration;
    }

    [Template]
    private dynamic? NonNullableReferenceTypeTemplate(IField field) {
        if (field.Value == null) {
            var value = meta.Proceed();

            var statementBuilder = new StatementBuilder();
            statementBuilder.AppendTypeName(typeof(Interlocked));
            statementBuilder.AppendVerbatim(".");
            statementBuilder.AppendVerbatim(nameof(Interlocked.CompareExchange));
            statementBuilder.AppendVerbatim("( ref ");
            statementBuilder.AppendExpression(field);
            statementBuilder.AppendVerbatim(", ");
            statementBuilder.AppendExpression(value);
            statementBuilder.AppendVerbatim(", null );");

            meta.InsertStatement(statementBuilder.ToStatement());
        }

        return field.Value;
    }

    [Template]
    private T BoxingTemplate<[CompileTime] T>(IField field) {
        if (field.Value == null) {
            var value = new StrongBox<T>( meta.Proceed()! );

            var statementBuilder = new StatementBuilder();
            statementBuilder.AppendTypeName(typeof(Interlocked));
            statementBuilder.AppendVerbatim(".");
            statementBuilder.AppendVerbatim(nameof(Interlocked.CompareExchange));
            statementBuilder.AppendVerbatim("( ref ");
            statementBuilder.AppendExpression(field);
            statementBuilder.AppendVerbatim(", ");
            statementBuilder.AppendExpression(value);
            statementBuilder.AppendVerbatim(", null );");

            meta.InsertStatement(statementBuilder.ToStatement());
        }

        return field.Value!.Value;
    }
}
