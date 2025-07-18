using static STRINGS.UI;

namespace SlippyCheeze.SupportCode;

public static class UIStrings {
    public static string AsLink(this string name, string target = "")
        => FormatAsLink(name, String.IsNullOrEmpty(target) ? name.ToUpperInvariant() : target);

    public static string AsTemperature(this float value) => GameUtil.GetFormattedTemperature(value);

    public static string AsKeyWord(this string text) => FormatAsKeyWord(text);
    public static string AsKeyWord(this LocString text) => FormatAsKeyWord(text);

    public static string AsPositiveModifier(this string text) => FormatAsPositiveModifier(text);
    public static string AsNegativeModifier(this string text) => FormatAsNegativeModifier(text);

    public static string ExtractLinkID(this string target) => STRINGS.UI.ExtractLinkID(target);

    // sadly, need to be explicit here, or the extension function won't match. :(
    public static string StripLinkFormatting(this StringEntry target) => STRINGS.UI.StripLinkFormatting(target);
    public static string StripLinkFormatting(this LocString target)   => STRINGS.UI.StripLinkFormatting(target);
    public static string StripLinkFormatting(this string target)      => STRINGS.UI.StripLinkFormatting(target);


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
