using static STRINGS.UI;

namespace SlippyCheeze.SupportCode;

public static class UIStrings {
    public static string GreenSignal(string? label = null) =>
        FormatAsAutomationState(label ?? STRINGS.UI.LOGIC_PORTS.GATE_SINGLE_OUTPUT_ONE_ACTIVE, AutomationState.Active);

    public static string RedSignal(string? label = null) =>
        FormatAsAutomationState(label ?? STRINGS.UI.LOGIC_PORTS.GATE_SINGLE_OUTPUT_ONE_INACTIVE, AutomationState.Standby);


    public static partial class links {
        // frequently used links.
        public static string Food(string label = "Food") => label.AsLink("FOOD");
        public static string GasPipes(string label = "Gas Pipes") => label.AsLink("GASPIPING");
        public static string Gases(string label = "Gases") => label.AsLink(CodexEntryGenerator_Elements.ELEMENTS_GASES_ID);
        public static string LiquidPipes(string label = "Liquid Pipes") => label.AsLink("LIQUIDPIPING");
        public static string Liquids(string label = "Liquids") => label.AsLink(CodexEntryGenerator_Elements.ELEMENTS_LIQUIDS_ID);
        public static string Solids(string label = "Solids") => label.AsLink(CodexEntryGenerator_Elements.ELEMENTS_SOLIDS_ID);
        public static string Temperature(string label = "Temperature") => label.AsLink("HEAT");
    }
}
