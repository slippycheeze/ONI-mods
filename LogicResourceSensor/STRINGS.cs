using static SlippyCheeze.SupportCode.UIStrings;

namespace SlippyCheeze.LogicResourceSensor;

public static partial class MODSTRINGS {
    public static partial class UI {
        public static partial class UISIDESCREENS {
            public static partial class RESOURCE_SENSOR_SIDESCREEN {
                public static readonly LocString TITLE = "Resource Sensor";

                public static partial class THRESHOLD {
                    public static readonly LocString UNITS = "Available Mass";
                    public static readonly LocString ABOVE = $"Will send a {GreenSignal()} if {UNITS.AsKeyWord()} is above {"{0}".AsPositiveModifier()}";
                    public static readonly LocString BELOW = $"Will send a {GreenSignal()} if the {UNITS.AsKeyWord()} is below {"{0}".AsNegativeModifier()}>";
                }

                public static partial class DISTANCE {
                    public static readonly LocString TITLE = "Sensor Range";
                    public static readonly LocString UNITS = "Cells";
                    public static readonly LocString TOOLTIP = $"Will count materials within {"{0}".AsPositiveModifier()} {"{1}".AsKeyWord()} of the sensor";
                }

                public static partial class MODE {
                    public static readonly LocString TITLE = "Resource Sensor Mode";
                    public static readonly LocString DESC  = "The Sensor Mode determines where it looks for matching things.";

                    public static readonly LocString INSTRUCTIONS = "Scan for resources based on:";

                    public static partial class DISTANCE {
                        public static readonly LocString TITLE = "Distance";
                        public static readonly LocString TOOLTIP = "Count resources within a specified number of tiles.";
                    }

                    public static partial class ROOM {
                        public static readonly LocString TITLE = "Room";
                        public static readonly LocString TOOLTIP = "Count resources in the same room as the sensor.";
                    }

                    public static partial class GLOBAL {
                        public static readonly LocString TITLE = "Global";
                        public static readonly LocString TOOLTIP = "Count resources on the same planet.";
                    }

                    public static partial class DISTANCEMODEOPTIONS {
                        public static readonly LocString CURRENT_VALUE = "Search Distance: {0}";
                    }
                }

                public static partial class OPTIONS {
                    public static partial class INCLUDESTORAGE {
                        public static readonly LocString TITLE   = "Include Storage Buildings";
                        public static readonly LocString TOOLTIP = "Should resources in storage buildings be counted?";
                    }

                    public static partial class INCLUDERESERVED {
                        public static readonly LocString TITLE   = "Include Reserved Materials";
                        public static readonly LocString TOOLTIP = "Should resources reserved by dupes be counted?";
                    }
                }
            }
        }
    }

    public static partial class BUILDINGS {
        public static partial class PREFABS {
            public static partial class LOGICRESOURCESENSOR {
                public static readonly LocString NAME = "Resource Sensor".AsLink(LogicResourceSensorConfig.ID);
                public static readonly LocString DESC = "Detecting resources can enable complex storage automations.";
                public static readonly LocString EFFECT = $"Sends a {GreenSignal()} or a {RedSignal()} based on count mode and material amount.";

                public static readonly LocString LOGIC_PORT = "Material Count";
                public static readonly LocString LOGIC_PORT_ACTIVE = $"Sends a {GreenSignal()} if the total mass of counted resources is greater than the selected threshold.";
                public static readonly LocString LOGIC_PORT_INACTIVE = $"Otherwise, sends a {RedSignal()}";
            }
        }
    }
}
