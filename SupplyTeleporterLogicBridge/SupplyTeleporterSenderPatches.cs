namespace SlippyCheeze.SupplyTeleporterLogicBridge;

[HarmonyPatch(typeof(WarpConduitSenderConfig))]
internal static partial class SupplyTeleporterSenderPatches {
    // add the logic port to the BuildingDef
    [HarmonyPatch(nameof(WarpConduitSenderConfig.CreateBuildingDef))]
    [HarmonyPostfix]
    internal static void AddPortToSupplyTeleporterSender(BuildingDef __result) {
        __result.LogicInputPorts ??= [];
        __result.LogicInputPorts.Add(
            LogicPorts.Port.RibbonInputPort(
                SupplyTeleporterLogicSender.SENDER_INPUT_PORT_ID,
                // placed at 0,0 below the liquid port, because it fits the least worst into the building anim.
                new CellOffset(1, 0),
                MODSTRINGS.BUILDINGS.PREFABS.SUPPLYTELEPORTERLOGICSENDER.LOGIC_PORT,
                MODSTRINGS.BUILDINGS.PREFABS.SUPPLYTELEPORTERLOGICSENDER.INPUT_PORT_ACTIVE,
                MODSTRINGS.BUILDINGS.PREFABS.SUPPLYTELEPORTERLOGICSENDER.INPUT_PORT_INACTIVE,
                show_wire_missing_icon: false,
                display_custom_name:    true
            )
        );

        // make it a happy little camper. :)
        GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, WarpConduitSenderConfig.ID);
        __result.AddSearchTerms(STRINGS.SEARCH_TERMS.AUTOMATION);
    }

    // SupplyTeleporterSenderConfig: add this component to the building when it is constructed.
    [HarmonyPatch(nameof(WarpConduitSenderConfig.DoPostConfigureComplete))]
    [HarmonyPostfix]
    internal static void AddComponentToSupplyTeleporterSender([HarmonyArgument(0)] GameObject go) {
        go.AddOrGet<SupplyTeleporterLogicSender>();
    }
}
