using Metalama.Framework.Aspects;
using Metalama.Framework.Code;

namespace SlippyCheeze.MetaProgramming.MetaLama;

[CompileTime]
public static class MetaTypes {
    public static INamedType RequireType(string typeName) {
        if (TypeFactory.GetType(typeName) is INamedType type)
            return type;
        throw new NotSupportedException($"Type '{typeName}' was not found in the compilation");
    }
}
