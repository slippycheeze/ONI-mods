namespace SlippyCheeze.SupportCode;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
public class CommentAttribute(string comment): Attribute {
    public string Comment => comment;
}


public static class CommentAttributeExtensions {
    public static string? GetComment(this Type type) {
        var attrs = type.GetCustomAttributes<CommentAttribute>();
        if (attrs.Any())
            return String.Join("\n", attrs.Select(attr => attr.Comment));
        return null;
    }
}
