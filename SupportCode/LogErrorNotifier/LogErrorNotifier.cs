namespace SlippyCheeze.SupportCode.LogErrorNotifier;

// MonoBehaviour is the first thing in Unity that gets the Update() call, which is what I really
// want to be going on with here.

public class LogErrorNotifier: MonoBehaviour {
    // It is **CRITICAL** that the version is bumped if there could be version skew between mods.
    // While these are just private and I rebuild everything on change, fine, but if I ever release
    // these on steam or whatever I **MUST** respect that.
    //
    // if this ever goes above 2^31 we have trouble, but really, increment once on
    // cross-mod-incompatible changes only, it'll never happen.
    public const int Version = 1;

    private static string MyFullTypeName = typeof(LogErrorNotifier).FullName;

    // This is a HarmonyPrefix method, attached to Game.OnPrefabInit, but only in the "winner" of
    // the election for the RemoteLogListener component by PLib.
    internal static void BeforeGamePrefabInit(Game __instance) {
        // NOTE: because, technically, each of my mods has a distinct instance of this class, if two
        // mods were to attach their components we wouldn't actually find the "second party"
        // component here, we'd just miss it when they Type compares non-equal.
        //
        // so, for my own sake, I'm going to add a safety check to see if there is a component based
        // on matching the *name* of the thing, and report if there is.  Just to be sure.
        foreach (var comp in __instance.GetComponents<Component>()) {
            if (comp.GetType().FullName == MyFullTypeName) {
                // is it an instance of me?
                if (comp is LogErrorNotifier) {
                    L.warn($"Found an instance of my assemblies LogErrorNotifier on Game already?");
                } else {
                    L.fatal($"Found an instance of LogErrorNotifier from another assembly on Game! {comp}");
                    // if fatal doesn't terminate, we can strip that out, but things'll still be
                    // pretty broken I suspect.
                    UnityEngine.Object.Destroy(comp);
                }
            }
        }

        // now, since we *should* have been the winners, slap our component into place.
        __instance.gameObject.AddOrGet<LogErrorNotifier>();

        // 2025-07-16 REVISIT: I'm not actually sure if OnPrefabInit will get called on my instance
        // or not, so maybe I should have a flag and call it once anyways?  ...or just rely on
        // OnSpawn(), which I'm pretty sure is called, I guess.
    }

    // info about an indivdual log message.
    internal readonly record struct LogLine(
        string time, LogLevel level, string thread, string caller, string message
    ) {
        public readonly string FormattedLine = $"{time} {thread} SC:{level.Humanize()} {caller}] {message}";
        public override string ToString() => FormattedLine;

        private readonly string Level = level.ToString().Transform(To.TitleCase);

        public readonly string Title   => $"SC: {Level} logged";

        public readonly NotificationType NotificationType = level switch {
            L.DEBUG   => NotificationType.Neutral,
            L.INFO    => NotificationType.Neutral,
            L.WARNING => NotificationType.BadMinor,
            L.ERROR   => NotificationType.Bad,
            L.FATAL   => NotificationType.Bad,
            _         => NotificationType.Bad
        };

        public Notification ToNotification() => new(
            title: Title,
            type: NotificationType,
            tooltip: GetToolTip,
            tooltip_data: this,
            expires: false,
            clear_on_click: true,
            show_dismiss_button: true
        );

        public static string GetToolTip(List<Notification> notifications, object _) {
            StringBuilder sb = GlobalStringBuilderPool.Alloc();
            try {
                sb.Append("log messages".ToQuantity(notifications.Count)).AppendLine(":");

                foreach (var line in notifications)
                    // tooltipData is the FormattedLine instance associated.
                    sb.AppendLine(line.tooltipData.ToString());

                return GlobalStringBuilderPool.ReturnAndFree(sb);
            } catch {
                GlobalStringBuilderPool.Free(sb);  // probably pointless, but whatever.
                throw;
            }
        }

        public static implicit operator Notification(LogLine message) => message.ToNotification();
    }

    private static Queue<LogLine> pendingMessages = [];

    public static void Add(string time, LogLevel level, string thread, string caller, string message)
        => pendingMessages.Enqueue(new(time, level, thread, caller, message));


    public bool haveShownOperational = false;
    public void Update() {
        if (!haveShownOperational) {
            L.debug($"pendingMessages.Count={pendingMessages.Count} {NotificationScreen.Instance.IsOrIsNotNull()}");
            haveShownOperational = true;
        }

        if (pendingMessages.Count > 0 && NotificationScreen.Instance is NotificationScreen notifier) {
            while (pendingMessages.Count > 0)
                notifier.AddNotification(pendingMessages.Dequeue());
            haveShownOperational = false;
        }
    }
}
