namespace SlippyCheeze.SweepyConveyorOutput;

[HarmonyPatch(typeof(GeneratedBuildings), nameof(GeneratedBuildings.LoadGeneratedBuildings))]
public static partial class AddConveyorOutputsToSweepy {
    public static void Postfix() {
        // this is a bit horrid, but there is no easier way to implement the same logic Klei does
        // given the concrete IBuildingConfig classes don't actually override all the configure
        // methods we need to make this happen with *two* conduit outputs.

        AddConveyorOutputTo(Assets.GetBuildingDef(SweepBotStationConfig.ID));
        if (Assets.GetBuildingDef("ReapBotStation") is BuildingDef ReapBotStation) {
            AddConveyorOutputTo(ReapBotStation);

            // In order to restore the TintColor that Reapy uses when we exit from a full screen
            // overlay like the solid or liquid conduit display, we need to hook into the
            // KBatchedAnimController and catch changes to the tint color externally.
            ReapBotStation.BuildingComplete.AddOrGet<ReapyTintManager>();
        }
    }

    private static void AddConveyorOutputTo(BuildingDef def) {
        L.log($"Adding Solid and Liquid Conduit outputs to {def.PrefabID}");

        // register with both overlays, because we are adding both conduit types
        GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SolidConveyorIDs, def.PrefabID);
        GeneratedBuildings.RegisterWithOverlay(OverlayScreen.LiquidVentIDs,    def.PrefabID);

        def.BuildingPreview.AddTag(GameTags.OverlayBehindConduits);
        def.BuildingUnderConstruction.AddTag(GameTags.OverlayBehindConduits);
        def.BuildingComplete.AddTag(GameTags.OverlayBehindConduits);

        // gonna YOLO the Storage, since I can't get at the Reapy private fields without being
        // reflection-heavy, and that just doesn't seem worth it right now.  guess I'll change
        // my mind when I rewrite it, or I end up broken. :)
        Storage storage = def.BuildingComplete.GetComponents<Storage>()[1];

        // I'm going to treat all the outputs as "secondary" in Klei terms, which is to say that I'm
        // not going to set the primary output conduit stuff in the def, just attach secondary
        // conduit outputs to the buildings, and conduitdispensers to the completed building.
        //
        // side note: I *really* wish that C# would get better about this sort of used-once iterable
        // definition, please.  please.
        ConduitPortInfo solidPort = new(ConduitType.Solid, new CellOffset(1, 1));

        def.BuildingPreview.AddComponent<ConduitSecondaryOutput>().portInfo            = solidPort;
        def.BuildingUnderConstruction.AddComponent<ConduitSecondaryOutput>().portInfo  = solidPort;
        def.BuildingComplete.AddComponent<ConduitSecondaryOutput>().portInfo           = solidPort;

        SolidConduitDispenser solids = def.BuildingComplete.AddComponent<OptionalSolidConduitDispenser>();
        solids.useSecondaryOutput = true;
        solids.storage            = storage;
        solids.alwaysDispense     = true;
        solids.elementFilter      = null;

        // occupies the same cell, which I'm OK with.
        ConduitPortInfo liquidPort = new(ConduitType.Liquid, new CellOffset(1, 1));

        def.BuildingPreview.AddComponent<ConduitSecondaryOutput>().portInfo            = liquidPort;
        def.BuildingUnderConstruction.AddComponent<ConduitSecondaryOutput>().portInfo  = liquidPort;
        def.BuildingComplete.AddComponent<ConduitSecondaryOutput>().portInfo           = liquidPort;

        ConduitDispenser liquids = def.BuildingComplete.AddComponent<OptionalConduitDispenser>();
        liquids.useSecondaryOutput = true;
        liquids.conduitType        = ConduitType.Liquid;
        liquids.storage            = storage;
        liquids.alwaysDispense     = true;
        liquids.elementFilter      = null;
    }
}
