namespace SlippyCheeze.Personal;

[HarmonyPatch]
public static class SmartResourceDiscovery {
    // being able to reference the worn version of equipment the moment that I first create the
    // normal versions is especially helpful, thank you.
    [HarmonyPatch(typeof(DiscoveredResources), nameof(DiscoveredResources.Discover), [typeof(Tag), typeof(Tag)])]
    [HarmonyPostfix]
    public static void EquipmentDiscoversWornVersion(Tag tag, Tag categoryTag) {
        // Everything equippable is in this category.
        if (categoryTag != GameTags.Clothes)
            return;

        EquipmentDef? def = Assets.GetPrefab(tag).GetComponent<Equippable>()?.def;
        if (def == null)  // this happens for the "worn" versions, and everything else should have one.
            return;

        // this is technically recursive, but should be acyclic unless someone does something
        // suuuper-weird in their equipment design ... and I'll fix that when I finally get the
        // stack overflow!  plus, I can imagine someone modding in equipment that degrades
        // multiple times before finally failing completely...
        if (def.wornID != null && !DiscoveredResources.Instance.IsDiscovered(def.wornID)) {
            L.log($"auto-discovering worn version of '{tag.Name}', '{def.wornID}' ");
            DiscoveredResources.Instance.Discover(def.wornID, GameTags.Clothes);
        }
    }

    [HarmonyPatch(typeof(MegaBrainTank), nameof(MegaBrainTank.DoInitialUnlock))]
    [HarmonyPostfix]
    public static void DreamJournalSmartUnlock() {
        L.log($"Auto-discovering resources now you have found the Somnium Synthasizer");
        DiscoveredResources.Instance.Discover(SleepClinicPajamas.ID, GameTags.Clothes);
        DiscoveredResources.Instance.Discover(DreamJournalConfig.ID, GameTags.StoryTraitResource);
    }


    // ================================================================================
    // just discover some nice stuff on the start of each game, just in case.
    [HarmonyPatch(typeof(Game), nameof(Game.OnSpawn))]
    [HarmonyPostfix]
    public static void DiscoverBasicStuffOnGameLoad() {
        // I really, really want to know about the hatch before the first one spawns, so I can
        // track them on the resource monitor and avoid them inappropriately eating my metals.

        // REVISIT 2025-05-16: this didn't work, because the way that the critters in resource
        // list mod is coded is a little weird, at least compared to how the other resource
        // discovery part happens.
        //
        // DiscoveredResources.Instance.Discover(HatchConfig.ID, GameTags.BagableCreature);

        // Discover plain and polluted water, which do show up everywhere.
        DiscoveredResources.Instance.Discover(Tags.Water, GameTags.Liquid);
        DiscoveredResources.Instance.Discover(Tags.DirtyWater, GameTags.Liquid);

        // Discover sandstone, which always happens ... at least on the clusters I played on so far.
        DiscoveredResources.Instance.Discover(Tags.SandStone, GameTags.BuildableRaw);
        DiscoveredResources.Instance.Discover(Tags.IgneousRock, GameTags.BuildableRaw);
        DiscoveredResources.Instance.Discover(Tags.Cuprite, GameTags.Metal);  // copper ore
        DiscoveredResources.Instance.Discover(Tags.Dirt, GameTags.Farmable);

        // The nasty things I want to schedule putting away *before* they first drop.
        DiscoveredResources.Instance.Discover(Tags.SlimeMold, GameTags.Organics);  // Slime
        DiscoveredResources.Instance.Discover(Tags.ToxicSand, GameTags.Organics);  // Polluted Dirt
        DiscoveredResources.Instance.Discover(Tags.ToxicMud, GameTags.Organics);   // Polluted Mud
        // vaguely weird thing that food turns into before turning into Pollutied Dirt (*ahem*, toxic sand)
        DiscoveredResources.Instance.Discover(RotPileConfig.ID, GameTags.Organics);

        DiscoveredResources.Instance.Discover(Tags.BleachStone, GameTags.Sublimating);
        DiscoveredResources.Instance.Discover(Tags.OxyRock, GameTags.Sublimating);  // Oxylite

        // If the story trait was unlocked, then this mod enabled, I still want to unlock the
        // Somnium Sythasizer related items, so... check the lock on start. :)
        if (Game.Instance.unlocks.IsUnlocked(MegaBrainTankConfig.INITIAL_LORE_UNLOCK_ID))
            DreamJournalSmartUnlock();
    }
}
