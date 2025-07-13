using static STRINGS.UI;

namespace SlippyCheeze.SupportCode;

public static partial class KleiStringExtensions {
    public static string AsLink(this string name, string target = "")
        => FormatAsLink(name, String.IsNullOrEmpty(target) ? name.ToUpperInvariant() : target);

    public static string AsKeyWord(this string text) => FormatAsKeyWord(text);

    public static string AsPositiveModifier(this string text) => FormatAsPositiveModifier(text);

    // sadly, need to be explicit here, or the extension function won't match. :(
    public static string StripLinkFormatting(this StringEntry target) => STRINGS.UI.StripLinkFormatting(target);
    public static string StripLinkFormatting(this LocString target)   => STRINGS.UI.StripLinkFormatting(target);
    public static string StripLinkFormatting(this string target)      => STRINGS.UI.StripLinkFormatting(target);
}
