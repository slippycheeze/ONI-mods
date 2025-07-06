using PeterHan.PLib.Buildings;

namespace SlippyCheezePersonal.LogicRateLimiter;

[HarmonyPatch]
public partial class LogicRateLimiterConfig: LogicGateBaseConfig {
    public const string ID = nameof(LogicRateLimiter);

    public override LogicGateBase.Op GetLogicOp() => LogicGateBase.Op.CustomSingle;

    public override CellOffset[]? InputPortOffsets   => [CellOffset.none];
    public override CellOffset[]? OutputPortOffsets  => [CellOffset.right];
    public override CellOffset[]? ControlPortOffsets => null;

    public override LogicGate.LogicGateDescriptions GetDescriptions() => new() {
        outputOne = new LogicGate.LogicGateDescriptions.Description() {
            name = MODSTRINGS.BUILDINGS.PREFABS.LOGICRATELIMITER.OUTPUT_NAME,
            active = MODSTRINGS.BUILDINGS.PREFABS.LOGICRATELIMITER.OUTPUT_ACTIVE,
            inactive = MODSTRINGS.BUILDINGS.PREFABS.LOGICRATELIMITER.OUTPUT_INACTIVE
        }
    };

    public override BuildingDef CreateBuildingDef() => CreateBuildingDef(ID, "logic_rate_limiter_kanim", width: 2, height: 1);

    public override void DoPostConfigureComplete(GameObject go) {
        // DELIBERATELY not invoking `base.DoPostConfigureComplete` so we don't add a `LogicGate`
        // component directly, but rather, add our derived component.
        //
        // Which also means, thanks Klei, we have to copy most of the code from
        // LogicGateBaseConfig.DoPostConfigureComplete in order to emulate it. :(
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
    public static void AddBuildingToTech() =>
        PBuildingManager.AddExistingBuildingToTech(ResearchID.Computers.AdvancedAutomation, ID);

    [HarmonyPatch(typeof(GeneratedBuildings), nameof(GeneratedBuildings.LoadGeneratedBuildings))]
    [HarmonyPrefix]
    public static void AddBuildingToPlan() => ModUtil.AddBuildingToPlanScreen(
        category:           Category.Automation,
        building_id:        ID,
        subcategoryID:      SubCategory.logicgates,
        relativeBuildingId: LogicGateFilterConfig.ID
    );
}
