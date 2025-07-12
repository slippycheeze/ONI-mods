using PeterHan.PLib.Buildings;

namespace SlippyCheeze.LogicRateLimiter;

[HarmonyPatch]
public partial class LogicRateLimiterConfig: LogicGateBaseConfig {
    public const string ID   = nameof(LogicRateLimiter);
    public const string TECH = ResearchID.Computers.AdvancedAutomation;
    public static readonly (string category, string subcategory, string after) PLAN = (
        Category.Automation,
        SubCategory.logicgates,
        LogicMemoryConfig.ID
    );

    public override LogicGateBase.Op GetLogicOp() => LogicGateBase.Op.CustomSingle;

    public override CellOffset[]? InputPortOffsets   => [CellOffset.none];
    public override CellOffset[]? OutputPortOffsets  => [CellOffset.right];
    public override CellOffset[]? ControlPortOffsets => null;

    public override LogicGate.LogicGateDescriptions GetDescriptions() => new() {
        outputOne = new LogicGate.LogicGateDescriptions.Description() {
            name     = MODSTRINGS.BUILDINGS.PREFABS.LOGICRATELIMITER.OUTPUT_NAME,
            active   = MODSTRINGS.BUILDINGS.PREFABS.LOGICRATELIMITER.OUTPUT_ACTIVE,
            inactive = MODSTRINGS.BUILDINGS.PREFABS.LOGICRATELIMITER.OUTPUT_INACTIVE
        }
    };

    public override BuildingDef CreateBuildingDef() {
        L.debug($"CreateBuildingDef({ID}) called");
        return CreateBuildingDef(ID, "logic_rate_limiter_kanim", width: 2, height: 1);
    }

    public override void DoPostConfigureComplete(GameObject go) {
        // DELIBERATELY not invoking `base.DoPostConfigureComplete` so we don't add a `LogicGate`
        // component directly, but rather, add our derived component.
        //
        // Which also means, thanks Klei, we have to copy most of the code from
        // LogicGateBaseConfig.DoPostConfigureComplete in order to emulate it. :(
        //
        // Worse, they actually do the same thing in their gates that don't want the base component
        // added. O_o
        var logic = go.AddComponent<LogicRateLimiter>();
        logic.op                 = this.GetLogicOp();
        logic.inputPortOffsets   = this.InputPortOffsets;
        logic.outputPortOffsets  = this.OutputPortOffsets;
        logic.controlPortOffsets = this.ControlPortOffsets;

        var prefabID = go.GetComponent<KPrefabID>();
        prefabID.prefabInitFn += (go) => go.GetComponent<LogicRateLimiter>().SetPortDescriptions(this.GetDescriptions());
        prefabID.AddTag(GameTags.OverlayBehindConduits);
    }


    // because we are using a Klei provided generic, not PBuilding, we need to manually inject our
    // building into the appropriate places. :(  At least I can use helper functions in both cases.
    [HarmonyPatch(typeof(Db), nameof(Db.Initialize))]
    [HarmonyPostfix]
    public static void AddBuildingToTech() {
        L.log($"Registering {ID} to Tech {TECH}");
        Db.Get().Techs.TryGet(TECH).unlockedItemIDs.Add(ID);
    }

    [HarmonyPatch(typeof(GeneratedBuildings), nameof(GeneratedBuildings.LoadGeneratedBuildings))]
    [HarmonyPrefix]
    public static void AddBuildingToPlan() {
        var (category, subcategory, after) = PLAN;
        L.log($"Registering {ID} to PlanScreen under {category} ⇒ {subcategory}, after {after}");
        ModUtil.AddBuildingToPlanScreen(
            category:           category,
            building_id:        ID,
            subcategoryID:      subcategory,
            relativeBuildingId: after
        );
    }
}
