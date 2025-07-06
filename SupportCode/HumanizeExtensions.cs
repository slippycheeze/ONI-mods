namespace SlippyCheeze.SupportCode;


public static class HumanizerExtensions {
    public static string IsOrIsNot(this string term, bool value) => value switch {
        true  => $"is {term}",
        false => $"is not {term}"
    };

    public static string IsOrIsNot(this bool value, [CallerArgumentExpression(nameof(value))] string? expr = null)
        => expr.ThrowIfNull().IsOrIsNot(value);

    public static string IsOrIsNotNull<T>(this T? value, [CallerArgumentExpression(nameof(value))] string? expr = null)
        => $"{expr.ThrowIfNull()} {"null".IsOrIsNot(value == null)}";


    public static string Humanize(this GameObject? go)
        => go == null ? "<null>" : $"{go.name}[{go.GetInstanceID()}]";

    public static string Humanize<T>(this T? cmp) where T: Component
        => cmp == null ? "<null>" : $"{cmp.GetType().Name}({cmp.gameObject.Humanize()})";
}
