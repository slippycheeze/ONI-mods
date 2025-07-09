using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace SlippyCheeze.MetaProgramming.MetaLama;

[CompileTime]
public static partial class CodeExtensions {
    [CompileTime]
    public static string ShortDescription(this IMethod method) => $"{method.DeclaringType}.{method.Name}";

    // [CompileTime]
    // public static IMethod? DeclaredMethod(this INamedType type, string name) {
    //     var methods = type.Methods.OfName(name);
    //     return methods.Any() ? methods.First() : null;
    // }

    public static bool IsVoidOrConvertibleTo(this IType @this, params Type[] types) {
        if (@this.IsConvertibleTo(SpecialType.Void))
            return true;

        foreach (var type in types)
            if (@this.IsConvertibleTo(type))
                return true;

        return false;
    }
}
