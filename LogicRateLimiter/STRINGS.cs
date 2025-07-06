using static SlippyCheeze.SupportCode.UIStrings;

namespace SlippyCheezePersonal.LogicRateLimiter;

public static partial class MODSTRINGS {
    public static partial class BUILDINGS {
        public static partial class PREFABS {
            public static partial class LOGICRATELIMITER {
                public static readonly LocString NAME = "Signal Rate Limiter".AsLink(LogicRateLimiterConfig.ID);
                public static readonly LocString DESC = $"This gate emits pulses rate-limited {GreenSignal("Green Signals")} while receiving a {GreenSignal()}.  It will not emit another pulse while receiving a {RedSignal()}, and the timeout is not reset when the incoming signal is {RedSignal("Red")}.";
                public static readonly LocString EFFECT = $"Sends a {GreenSignal()} when it receives a a {GreenSignal()}.  It will then send a {RedSignal()} for the configured time, after which it will pulse {GreenSignal("Green")} again if it is still receiving a {GreenSignal()}.\n\nThe rate-limit is not reset if the incoming signal becomes {RedSignal("Red")}; the gate will {"NEVER".AsKeyWord()} emit {GreenSignal("Green Signals")} faster than the configured rate.";

                public static readonly LocString OUTPUT_NAME     = $"RATE-LIMITED OUTPUT";
                public static readonly LocString OUTPUT_ACTIVE   = $"Pulses a {GreenSignal()} at the configured interval while receiving a {GreenSignal()} from the input.";
                public static readonly LocString OUTPUT_INACTIVE = $"Otherwise, sends a {RedSignal()}.  The timeout is <b>NOT</b> reset when a {RedSignal()} is received.";
            }
        }
    }

    public static partial class UI {
        public static partial class UISIDESCREENS {
            public static partial class LOGICRATELIMITER {
                public static readonly LocString TITLE = BUILDINGS.PREFABS.LOGICRATELIMITER.NAME.StripLinkFormatting();
                public static readonly LocString TOOLTIP
                    = $"Will pulse a {GreenSignal()} every {"{0} seconds".AsPositiveModifier()} while receiving a {GreenSignal()}.  The delay is <b>NOT</b> reset if a {RedSignal()} is received before it expires.";
            }
        }
    }
}
