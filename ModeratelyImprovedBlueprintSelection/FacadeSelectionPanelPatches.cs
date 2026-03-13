using PeterHan.PLib.UI;

namespace SlippyCheeze.ModeratelyImprovedBlueprintSelection;


// for the "pick at random" icon / image
// codexTipsAndinfo              - steel blue question mark in circle
// mode_segue                    - question mark, grey with black outline
// new_lore                      - blueprint rolled and tied with a ribbon
// icon_categories_placeholder   - keyring with tags, an image on each of them
// SupplyClosetIcon_LootBox      - (UNUSED?) loot box icon in Klei style (sadly, shows shirt image)
// us_elemenents-other           - black-ish bubbles, with a sketch question mark on top
// rotate_building               - 3D effect two arrows making a circle
// requirements                  - question mark in circle, but only grey, and tiny
// codexIconBuildings            - coal gen, musherator, algae O2 machine, sort randomly spattered
// SupplyClosetIcon_Blueprints   - randomly scattered "all types" examples, also large
// reveal                        - eye icon, grey with red pupil
// icon_display_screen_status    - dashed line single circle arrow in red
// statusIcon                    - speech bubble with dashed line single circle arrow in red
// SupplyClosetIcons_Blueprint   - shows the portal ALA "blueprint won" spawn, on a blueprint
// icon_archetype_random         - white "two arrows in a circle" type image

[HarmonyPatch(typeof(FacadeSelectionPanel))]
internal static partial class FacadeSelectionPanelPatches {
    // Debugging: dump the visual layout of the prefab, so I can figure out where to inject my checkbox.
    [HarmonyPatch(nameof(FacadeSelectionPanel.OnPrefabInit))]
    [HarmonyPostfix]
    internal static void OnPrefabInit(FacadeSelectionPanel __instance) {
        PUIUtils.DebugObjectHierarchy(__instance.gameObject);
        PUIUtils.DebugObjectTree(__instance.gameObject);
    }

    // Support for "remember this facade" option
    [HarmonyPatch(nameof(FacadeSelectionPanel.SetBuildingDef))]
    [HarmonyPrefix]
    internal static void UseRememberedFacade(string defID, ref string currentFacadeID) {
        L.debug($"defID={defID} currentFacadeID={currentFacadeID}");

        if (currentFacadeID != null)
            return;             // a selection has already been made here...

        // REVISIT: check the saved data, if we have something then set currentFacadeID to it.
    }
}
