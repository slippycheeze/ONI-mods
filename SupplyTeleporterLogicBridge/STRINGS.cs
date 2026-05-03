using static SlippyCheeze.SupportCode.UIStrings;

namespace SlippyCheeze.SupplyTeleporterLogicBridge;

public static partial class MODSTRINGS {
    public static partial class BUILDINGS {
        public static partial class PREFABS {
            public static partial class SUPPLYTELEPORTERLOGICRECEIVER {

                public static readonly LocString LOGIC_PORT = $"Output Signals received from the {STRINGS.BUILDINGS.PREFABS.WARPCONDUITSENDER.NAME}";
                public static readonly LocString OUTPUT_PORT_ACTIVE =
                    $"Output {GreenSignal("Green Signals")} received from the connected {STRINGS.BUILDINGS.PREFABS.WARPCONDUITSENDER.NAME}.";
                public static readonly LocString OUTPUT_PORT_INACTIVE =
                    $"Output {RedSignal("Red Signals")} received from the connected {STRINGS.BUILDINGS.PREFABS.WARPCONDUITSENDER.NAME}.";
            }

            public static partial class SUPPLYTELEPORTERLOGICSENDER {

                public static readonly LocString LOGIC_PORT = $"Relay Signals to the {STRINGS.BUILDINGS.PREFABS.WARPCONDUITRECEIVER.NAME}";
                public static readonly LocString INPUT_PORT_ACTIVE =
                    $"Relay {GreenSignal("Green Signals")} to the connected {STRINGS.BUILDINGS.PREFABS.WARPCONDUITRECEIVER.NAME}.";
                public static readonly LocString INPUT_PORT_INACTIVE =
                    $"Relay {RedSignal("Red Signals")} to the connected {STRINGS.BUILDINGS.PREFABS.WARPCONDUITRECEIVER.NAME}.";
            }
        }
    }
}
