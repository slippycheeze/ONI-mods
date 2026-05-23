using System.Collections.Immutable;
using KSerialization;
using PeterHan.PLib.UI;

using Random = UnityEngine.Random;


namespace SlippyCheeze.ImprovedFacadeSelection;


[HarmonyPatch]
[SerializationConfig(MemberSerialization.OptIn)]
internal partial class ImprovedProductInfoScreenFacadeSelection: KMonoBehaviour {
    [HarmonyPatch(typeof(ProductInfoScreen), nameof(ProductInfoScreen.Awake))]
    [HarmonyPostfix]
    internal static void AttachToProductInfoScreen(ProductInfoScreen __instance) {
        L.debug($"attached to {__instance.Humanize()}");
        __instance.AddOrGet<ImprovedProductInfoScreenFacadeSelection>();
    }

    [MyCmpGet]
    ProductInfoScreen productInfoScreen = null!;  // never null at runtime, but.

    public override void OnPrefabInit() {
        // GameObject[ProductInfoScreen]
        // -> GameObject[FacadeSelectionPanel]
        //    Component[FacadeSelectionPanel]
        //    -> GameObject[FacadeSelector]
        //       -> GameObject[Header]
        //       -> GameObject[Scrollrect]
        //          Scrollrect contains all the selection stuff...
        //
        //
        // inject this control just after the header, since that is stable and avoids the weirdness
        // I found where some-but-not-all instances have a Scrollrect wrapping the actual content.
        //
        // plus, I kind of feel like this is moderately better as a fixed thing than a scrolling
        // thing, in terms of how it fits with the UI design...
        if (this.transform.Find("FacadeSelectionPanel/FacadeSelector/Header") is not Transform header) {
            L.error($"{this.Humanize()}: failed to find object ProductInfoScreen/FacadeSelectionPanel/FacadeSelector/Header");
            PUIUtils.DebugObjectTree(this.gameObject);
            return;
        }

        PButton pbutton = new("PickRandomFacade") {
            FlexSize = Vector2.right,
            // Margin         = new(left: 10, right: 10, top: 8, bottom: 8),
            Text = MODSTRINGS.UI.IMPROVED_FACADE_SELECTION.RANDOM_FACADE_BUTTON.TEXT,
            ToolTip = MODSTRINGS.UI.IMPROVED_FACADE_SELECTION.RANDOM_FACADE_BUTTON.TOOLTIP,
            Color = PUITuning.Colors.ButtonBlueStyle,
            TextStyle = PUITuning.Fonts.TextLightStyle,
            TextAlignment = TextAnchor.MiddleCenter,
            OnClick = this.OnRandomFacadeClicked,
        };
        pbutton.AddOnRealize(go => go.SetMinUISize(new(64, 24)));
        GameObject button = pbutton.AddTo(header.transform.parent.gameObject, header.GetSiblingIndex() + 1);
        button.SetActive(true);
    }

    public void OnRandomFacadeClicked(GameObject source) {
        // L.debug($"{this.Humanize()} Random Facade button was clicked...");
        var panel = productInfoScreen.FacadeSelectionPanel;

        // because "randomly" picking the same one just isn't going to fly with humans, where by
        // "humans" I mean "me", mostly.
        var keys = panel.activeFacadeToggles.Keys.ToImmutableArray();
        string random;
        do {
            random = keys[Random.Range(0, keys.Length)];
        } while (random == panel.SelectedFacade);  // hopefully just the once...

        panel.SelectedFacade = random;
    }
}
