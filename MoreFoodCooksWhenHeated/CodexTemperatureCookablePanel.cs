using Styles    = System.Collections.Generic.Dictionary<CodexTextStyle, TextStyleSetting>;
using FoodInfo  = EdiblesManager.FoodInfo;
using UnityEngine.UI;
using PeterHan.PLib.UI;

namespace SlippyCheeze.MoreFoodCooksWhenHeated;

// based on the Klei CodexTemperatureTransitionPanel, but lightly adapted to support entities for
// input and output.  uses the same prefab.
[HarmonyPatch]
public class CodexTemperatureCookablePanel(FoodInfo raw, FoodInfo cooked, float outputPercent, float temperature):
    CodexWidget<CodexTemperatureCookablePanel>
{
    public override void Configure(GameObject content, Transform pane, Styles styles) {
        var refs = content.GetComponent<HierarchyReferences>();

        // NOTE: Klei did this in the middle.  I don't /think/ it'll make a difference what order
        // I poke our content into place, but just in case, a reminder to myself.
        ConfigureTemperature(refs.GetReference<RectTransform>("TemperaturePanel").gameObject);


        // DEBUG dumping of the HeaderLabel reference.  whee.
        var header = refs.GetReference<LocText>("HeaderLabel");
        // don't go looking up in the Strings database, though I don't think you will at runtime.
        // either way, looks like some places in the code check explicitly for `key != ""`, so we
        // should emulate that as the "IS NOT SET YOYO" value.
        //
        // this is, notably, also how Klei do it in `PopupConfirmDialog`, and is *NOT* what
        // `SetText` will do; worse luck, it isn't clear to me if either is correct, because two
        // different Klei Codex widgets use two different approaches.
        //
        // anyways, this one here is the appropriate next step: I tried `.SetText` and got
        // weirdness, so lets try this one instead.
        header.key = "";
        header.text = String.Format(
            STRINGS.CODEX.FORMAT_STRINGS.TRANSITION_LABEL_TO_ONE_ELEMENT,
            raw.ConsumableName,
            cooked.ConsumableName
        );


        // Destroying all the children comes from Klei; we are reused, because there is a Klei
        // management pool of instances used when rendering.  It'll always be one item full, since
        // we can only have one transition, so we will be reused *constantly*, causing all this GC
        // thrash as we add and remove prefab ID stuff while navigating.
        //
        // Which kinda defeats the point of an object pool, neh?
        var sourceContainer  = refs.GetReference<RectTransform>("SourceContainer").gameObject;
        foreach (Component child in sourceContainer.transform)
            UnityEngine.Object.Destroy(child.gameObject);

        var resultsContainer = refs.GetReference<RectTransform>("ResultsContainer").gameObject;
        foreach (Component child in resultsContainer.transform)
            UnityEngine.Object.Destroy(child.gameObject);

        var materialPrefab = refs.GetReference<RectTransform>("MaterialPrefab").gameObject;

        ConfigureFoodPrefab(materialPrefab, sourceContainer,  raw,    1f);
        ConfigureFoodPrefab(materialPrefab, resultsContainer, cooked, outputPercent);

        // last of all, apply the layout, from our base class.
        // 2025-07-15: disabled, since it seems to break header bar layout?
        // ConfigurePreferredLayout(content);
    }

    private void ConfigureTemperature(GameObject go) {
        var panel = go.GetComponent<HierarchyReferences>();

        string formattedTemperature = GameUtil.GetFormattedTemperature(temperature);;

        var label   = panel.GetReference<LocText>("Label");
        label.text  = formattedTemperature;
        label.color = Color.red;

        panel.GetReference<Image>("Icon").sprite = Assets.GetSprite("crew_state_temp_up");

        panel.GetReference<ToolTip>("ToolTip").toolTip = String.Format(
            STRINGS.CODEX.FORMAT_STRINGS.TEMPERATURE_OVER,
            formattedTemperature
        );
    }

    private void ConfigureFoodPrefab(GameObject prefab, GameObject container, FoodInfo food, float amount) {
        var refs = Util.KInstantiateUI(prefab, container, true).GetComponent<HierarchyReferences>();

        Image icon   = refs.GetReference<Image>("Icon");
        var sprite   = Def.GetUISprite(food.ConsumableId);
        icon.sprite  = sprite.first;
        icon.color   = sprite.second;

        StringBuilder sb = new(50);
        GameUtil.AppendFormattedCalories(sb, food.CaloriesPerUnit * amount, true);
        sb.AppendLine();
        GameUtil.AppendFormattedMass(sb, amount, massFormat: GameUtil.MetricMassFormat.UseThreshold, includeSuffix: true);

        var title   = refs.GetReference<LocText>("Title");
        title.text  = sb.ToString();
        title.color = Color.black;

        // not honestly sure what I should do about this now.  maybe hide it instead?
        refs.GetReference<ToolTip>("ToolTip").toolTip = food.ConsumableName;

        // if I have to have a closure, at least it can be a small one.
        string targetID = food.ConsumableId.ToTag().ProperName().ExtractLinkID();
        refs.GetReference<KButton>("Button").onClick +=
            () => ManagementMenu.Instance.codexScreen.ChangeArticle(targetID);
    }


    [HarmonyPatch(typeof(CodexScreen), nameof(CodexScreen.SetupPrefabs))]
    [HarmonyPostfix]
    public static void SetupCodexPrefab(CodexScreen __instance) {
        // since I cloned their code, I reuse their prefab. :)
        //
        // anyway, this is the correct hook-point for the display side of things, and Klei code will
        // generically set up things like the ObjectPool used to display our content.

        // actually, gonna go ahead and clone their prefab, so I can be sure we are not accidentally
        // sharing changes or anything.  just in case.
        GameObject original = __instance.ContentPrefabs[typeof(CodexTemperatureTransitionPanel)];
        GameObject clone    = UnityEngine.Object.Instantiate(original, null, true);
        clone.name = "PrefabTemperatureCookablePanel";  // make it identifiable in debug dumps.
        __instance.ContentPrefabs[typeof(CodexTemperatureCookablePanel)] = clone;
    }

    // REVISIT: I should consider if I want to hook into CodexCache.widgetTagMappings for YAML
    // loading later?  I really don't think so, though, since I'm pretty specialized...
}
