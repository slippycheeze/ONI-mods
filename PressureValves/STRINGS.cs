using static SlippyCheeze.SupportCode.UIStrings;

namespace SlippyCheeze.PressureValves;

public static partial class MODSTRINGS {
    public static partial class BUILDINGS {
        public static partial class PREFABS {
            public static partial class LIQUIDPRESSUREVALVE {
                public static readonly LocString NAME = LiquidPressureValveConfig.ID.Humanize().AsLink(LiquidPressureValveConfig.ID);
                public static readonly LocString DESC = $"Pressure Valves turn small packets of a {links.Liquids("liquid")} into large packets.  If more than one {links.Liquids("liquid")} is in the pipe, it may not always fully merge packets.";
                public static readonly LocString EFFECT = $"Manages the {links.Liquids("Liquid")} volume in {links.LiquidPipes("Pipes")} by merging multiple small packets into a single, large packet.";
            }

            public static partial class GASPRESSUREVALVE {
                public static readonly LocString NAME = GasPressureValveConfig.ID.Humanize().AsLink(GasPressureValveConfig.ID);
                public static readonly LocString DESC = $"Pressure Valves turn small packets of a {links.Gases("gas")} into large packets.  If more than one {links.Gases("gas")} is in the pipe, it may not always fully merge packets.";
                public static readonly LocString EFFECT = $"Manages the {links.Gases("Gas")} volume in {links.GasPipes("Pipes")} by merging multiple small packets into a single, large packet.";
            }
        }
    }
}
