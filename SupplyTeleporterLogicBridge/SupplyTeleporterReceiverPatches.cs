namespace SlippyCheeze.SupplyTeleporterLogicBridge;

// SupplyTeleporterReceiverConfig: add the logic port to the BuildingDef.  This is everything required
// from that side; Klei will create the `LogicPorts` component that I need, later, to send data, and
// we never receive it, so ... job done.
[HarmonyPatch(typeof(WarpConduitReceiverConfig), nameof(WarpConduitReceiverConfig.CreateBuildingDef))]
internal static partial class AddPortToSupplyTeleporterReceiver {
    internal static void Postfix(BuildingDef __result) {
        __result.LogicOutputPorts ??= [];
        __result.LogicOutputPorts.Add(
            LogicPorts.Port.RibbonOutputPort(
                SupplyTeleporterLogicSender.RECEIVER_OUTPUT_PORT_ID,
                // placed at 0,0 below the liquid port, because it fits the least worst into the building anim.
                new CellOffset(0, 0),
                MODSTRINGS.BUILDINGS.PREFABS.SUPPLYTELEPORTERLOGICRECEIVER.LOGIC_PORT,
                MODSTRINGS.BUILDINGS.PREFABS.SUPPLYTELEPORTERLOGICRECEIVER.OUTPUT_PORT_ACTIVE,
                MODSTRINGS.BUILDINGS.PREFABS.SUPPLYTELEPORTERLOGICRECEIVER.OUTPUT_PORT_INACTIVE,
                show_wire_missing_icon: false,
                display_custom_name:    true
            )
        );

        // make it a happy little camper. :)
        GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, WarpConduitReceiverConfig.ID);
        __result.AddSearchTerms(STRINGS.SEARCH_TERMS.AUTOMATION);
    }
}
