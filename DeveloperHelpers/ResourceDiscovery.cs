namespace SlippyCheeze.DeveloperHelpers;

[HarmonyPatch(typeof(DiscoveredResources), nameof(DiscoveredResources.Discover), [typeof(Tag), typeof(Tag)])]
public static partial class ResourceDiscovery {
    // some debugging help.  this time with sensible behaviour. :)
    public static void Prefix(Tag tag, Tag categoryTag) {
        if (!DiscoveredResources.Instance.IsDiscovered(tag))
            L.log($"""Newly discovered: {tag.Name}.ID, GameTags.{categoryTag.Name}""");
    }
}
