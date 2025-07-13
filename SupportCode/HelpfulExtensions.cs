namespace SlippyCheeze.SupportCode;


public static class HelpfulExtensions {
    [return: NotNull]
    public static T ThrowIfNull<T>(this T? value, string? context = null, object? arg0 = null, [CallerFilePath] string? path = null, [CallerLineNumber] int line = -1) {
        if (value == null) {
            StringBuilder msg = new("\n\nUnexpected null at ");
            msg.Append(L.Caller(path, line));
            if (context != null)
                msg.Append(": ").AppendFormat(context, arg0);
            throw new NullReferenceException(msg.ToString());
        }
        return value!;
    }

    public static T DebugDump<T>(
        this T value,
        [CallerFilePath] string? path = null,
        [CallerLineNumber] int line = -1,
        [CallerArgumentExpression(nameof(value))] string? expr = null

    ) {
        L.debug(value, expr, line, path);
        return value;
    }

    // 2025-07-06 REVISIT: should I do this more dynamically or something?  hard to say, TBH.
    private const string topLevelNamespace = "SlippyCheeze.";
    public static string ShortDescription(this MethodBase method, bool stripNamespace = false) {
        string desc = $"{method.DeclaringType}.{method.Name}";
        if (stripNamespace) {
            if (desc.StartsWith(topLevelNamespace))
                desc = desc[topLevelNamespace.Length..];
        }
        return desc;
    }


    public static bool IsCompilerGenerated(this Type type)
        => type.GetCustomAttribute<CompilerGeneratedAttribute>() != null;


    // I missed clamp, so I implement it, YOLO!
    public static int Clamp(this int value, int min, int max) => value < min ? min : value > max ? max : value;
    public static float Clamp(this float value, float min, float max) => value < min ? min : value > max ? max : value;


    // backport this method from newer versions of dotnet
    public static T[] Fill<T>(this T[] array, T value) {
        for (int n = 0; n < array.Length; ++n) {
            array[n] = value;
        }
        return array;
    }


    // WTF isn't this the default?
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? str) => String.IsNullOrEmpty(str);
}
