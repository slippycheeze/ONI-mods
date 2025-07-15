using System.Diagnostics;
using System.Threading;

using SlippyCheeze.SupportCode.LogErrorNotifier;

namespace SlippyCheeze.SupportCode;


public enum LogLevel {
    DEBUG   = 0,
    INFO    = 1,
    WARNING = 2,
    ERROR   = 3,
    FATAL   = 4,
}

public class L {
    // internal static string thisAssembly = Assembly.GetExecutingAssembly().GetName().Name;

    // caller lookup via the stack, with memoization.
    internal sealed record class Callsite(string? path, int line);
    internal static Dictionary<Callsite, string> callerID = [];

    // was going to hookName in "boring" method name tracking, but ... prolly there are none.
    internal static string MakeNiceCallerName(Type type, MethodBase method) {
        string name = method.Name switch {
            ".ctor"   => $"{type.Name}()",         // regular constructor
            ".cctor"  => $"{type.Name}",           // static construcor
            "Prepare" => $"{type.Name}",           // Harmony Prepare method, so boring!
            "Prefix"  => $"{type.Name}:Pre",
            "Postfix" => $"{type.Name}:Pst",
            _         => $"{type.Name}.{method.Name}",
        };
        return name;
    }

    private static Regex pathToLoggingFilenameRegex = new(
        // "C:/…/ONI-mods/MakeThingsUserNamable/MakeThingsUserNamable.cs"
        // "ONI-mods/SlippyCheezePersonal/obj/Debug/net48/metalama/StoragePod/FreezerPodConfig.cs"
        @"/ONI-mods/(?:.+/obj/.+/metalama/)?(.+)$",
        RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled
    );

    private static string pathToLoggingFilename(string? path) {
        if (path == null)
            return "<path=null>";

        var match = pathToLoggingFilenameRegex.Match(path.Replace(@"\", "/"));
        if (match.Success && match.Groups[1].Success)
            return match.Groups[1].Value;

        // just give up.
        return path;
    }

    private static Regex isInMyNamespaceRegex = new(
        @"^SlippyCheeze\b([.]SupportCode\b)?",
        RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled
    );

    private static bool IsInIgnoredNamespace(Type type, bool ignoreSupportCode) {
        if (type?.Namespace == null) {
            return true;
        }
        var match = isInMyNamespaceRegex.Match(type.Namespace);
        if (!match.Success) {
            return true;
        }
        if (ignoreSupportCode && match.Groups[1].Success) {
            return true;
        }

        return false;
    }


    internal static string Caller(string? path, int line) {
        Callsite key = new(path, line);

        // fast path: already in our cache.
        if (callerID.TryGetValue(key, out string caller))
            return caller;


        // slow path: get the stack track, walk out of the current class, record the caller for
        // the future, and then return it.
        bool ignoreSupportCode = path?.Contains(@"\SupportCode\") != true;

        for (int ancestor = 1; ancestor < 20; ancestor++) {
            // This has some additional checks, from Harmony, to find the method based on the
            // address if the StackFrame itself can't, and is otherwise identical to just asking the
            // StackFrame directly.
            var method = Harmony.GetMethodFromStackframe(new StackFrame(ancestor));
            if (method == null)
                continue;

            var namedType = method.DeclaringType;
            if (namedType == null)
                continue;

            // a very special case™, are we trying to trace ourselves?
            if (!ignoreSupportCode && namedType == typeof(L))
                continue;

            // ignore things outside our own namespace, like the MoreLinq helper library.
            if (IsInIgnoredNamespace(namedType, ignoreSupportCode))
                continue;

            // ignore compiler generated classes from, eg, async methods and Linq processing
            if (namedType.IsCompilerGenerated())
                continue;

            // finally got there!
            callerID[key] = MakeNiceCallerName(namedType, method);
            return callerID[key];
        }

        // failed, just report the relative path, filename, and line number, of our caller.
        return callerID[key] = $"{pathToLoggingFilename(path)}:{line}";
    }


    public static string WriteLogLine(LogLevel level, string msg, int line, string? path)
        => WriteLogLine(level, msg, Caller(path, line));

    public static string WriteLogLine(LogLevel level, string msg, string caller) {
        // I was going to drop THREAD entirely, but then it turns out the beta for the next
        // expansion runs various code on background threads rather than the Unity main thread!
        // So suddenly they *do* become important.
        string thread = $"{(Game.IsOnMainThread() ? "M" : "×")}{Thread.CurrentThread.ManagedThreadId,-2}";

        string time = System.DateTime.Now.ToString("HH:mm:ss.ffff");

        if (level >= WARNING)
            RemoteLogListener.OnLogMessage(time, level, thread, caller, msg);

        string text = $"{thread} SC:{LogLevelForDisplay(level)} {caller}] {msg}";
        Console.WriteLine($"{time} {text}");
        return text;
    }

    // internal const string DEBUG = "D";
    // internal const string INFO  = "I";
    // internal const string WARN  = "W";
    // internal const string ERROR = "E";
    // internal const string FATAL = "F";

    public const LogLevel DEBUG   = LogLevel.DEBUG;
    public const LogLevel INFO    = LogLevel.INFO;
    public const LogLevel WARNING = LogLevel.WARNING;
    public const LogLevel ERROR   = LogLevel.ERROR;
    public const LogLevel FATAL   = LogLevel.FATAL;

    public static string LogLevelForDisplay(LogLevel l) => l switch {
        LogLevel.DEBUG   => "D",
        LogLevel.INFO    => "I",
        LogLevel.WARNING => "W",
        LogLevel.ERROR   => "E",
        LogLevel.FATAL   => "!"
    };

    public static void debug(
        string message,
        [CallerLineNumber] int line = -1,
        [CallerFilePath] string? path = null
    ) {
        WriteLogLine(DEBUG, message, line, path);
    }

    public static void debug(
        object? value,
        [CallerArgumentExpression(nameof(value))] string? expr = null,
        [CallerLineNumber] int line = -1,
        [CallerFilePath] string? path = null
    ) {
        WriteLogLine(DEBUG, $"{expr}={value}", line, path);
    }



    public static void log(string msg, [CallerLineNumber] int line = -1, [CallerFilePath] string? path = null)
        => WriteLogLine(INFO, msg, line, path);
    public static void info(string msg, [CallerLineNumber] int line = -1, [CallerFilePath] string? path = null)
        => WriteLogLine(INFO, msg, line, path);

    public static void warn(string msg, [CallerLineNumber] int line = -1, [CallerFilePath] string? path = null)
        => WriteLogLine(WARNING, msg, line, path);

    public static void error(string msg, [CallerLineNumber] int line = -1, [CallerFilePath] string? path = null)
        => WriteLogLine(ERROR, msg, line, path);

    [DoesNotReturn]
    public static void fatal(string msg, [CallerLineNumber] int line = -1, [CallerFilePath] string? path = null)
        => throw new InvalidOperationException(WriteLogLine(FATAL, msg, line, path));
}

