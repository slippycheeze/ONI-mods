using TUNING;

using PeterHan.PLib.Buildings;


namespace SlippyCheeze.LogicResourceSensor;

public class LogicResourceSensorConfig: IBuildingConfig {
    public const string ID = "LogicResourceSensor";

    public static void OnModLoaded() => ModMain.Register(LogicResourceSensor);

    internal static PBuilding LogicResourceSensor => field ??= new(ID, MODSTRINGS.BUILDINGS.PREFABS.LOGICRESOURCESENSOR.NAME) {
        // I *hope* this allows my existing string registration to work.
        Description = null,
        EffectText = null,

        Tech = ResearchID.Computers.GenericSensors,  // no constants, far as I can see, for these.

        // Add to build menu just after the liquid and gas sensors.
        AddAfter = LogicElementSensorLiquidConfig.ID,
        // 2025-05-24 can't find any constants for these, so I guess it'll be hand-written.
        Category = KleiPlan.Automation.ID,
        SubCategory = KleiPlan.Automation.sensors,

        Animation = "logic_resource_sensor_kanim",

        Width = 1,
        Height = 1,
        HP = 30,

        Placement = BuildLocationRule.Anywhere,
        ConstructionTime = 30f,
        Ingredients = { new(MATERIALS.REFINED_METALS, tier: 0) },
        AudioCategory = TUNING.AUDIO.CATEGORY.METAL,

        AlwaysOperational = true,
        OverheatTemperature = null,  // does not overheat
        Floods = false,
        Entombs = false,

        Decor = TUNING.BUILDINGS.DECOR.PENALTY.TIER0,
        Noise = NOISE_POLLUTION.NONE,

        ViewMode = OverlayModes.Logic.ID,
        SceneLayer = Grid.SceneLayer.Building,

        LogicIO = {
                LogicPorts.Port.OutputPort(
                    LogicSwitch.PORT_ID,
                    new CellOffset(0, 0),
                    MODSTRINGS.BUILDINGS.PREFABS.LOGICRESOURCESENSOR.LOGIC_PORT,
                    MODSTRINGS.BUILDINGS.PREFABS.LOGICRESOURCESENSOR.LOGIC_PORT_ACTIVE,
                    MODSTRINGS.BUILDINGS.PREFABS.LOGICRESOURCESENSOR.LOGIC_PORT_INACTIVE,
                    show_wire_missing_icon: true
                )
            }
    };

    public override BuildingDef CreateBuildingDef() {
        GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, ID);

        // ?????  far as I can tell all the Klei things do this, so does ResourceSensor, which ... whatevs, I guess?
        SoundEventVolumeCache.instance.AddVolume("switchgaspressure_kanim", "PowerSwitch_on", NOISE_POLLUTION.NOISY.TIER3);
        SoundEventVolumeCache.instance.AddVolume("switchgaspressure_kanim", "PowerSwitch_off", NOISE_POLLUTION.NOISY.TIER3);

        return LogicResourceSensor.CreateDef();
    }

    public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag) {
        base.ConfigureBuildingTemplate(go, prefab_tag);
        LogicResourceSensor.ConfigureBuildingTemplate(go);
    }

    public override void DoPostConfigureComplete(GameObject prefab) {
        LogicResourceSensor.DoPostConfigureComplete(prefab);
        LogicResourceSensor.CreateLogicPorts(prefab);

        prefab.AddTag(GameTags.OverlayInFrontOfConduits);

        Storage storage = prefab.AddOrGet<Storage>();
        storage.allowItemRemoval = false;
        storage.showDescriptor = false;
        storage.storageFilters
            = TUNING.STORAGEFILTERS.SOLID_TRANSFER_ARM_CONVEYABLE
            .Union(TUNING.STORAGEFILTERS.SPECIAL_STORAGE)
            .Union(TUNING.STORAGEFILTERS.LIQUIDS)
            .Union(TUNING.STORAGEFILTERS.GASES)
            .ToList();
        storage.allowSettingOnlyFetchMarkedItems = false;
        storage.showInUI = true;
        storage.showCapacityStatusItem = false;
        storage.showCapacityAsMainStatus = false;

        TreeFilterable treeFilterable = prefab.AddOrGet<TreeFilterable>();
        treeFilterable.showUserMenu = true;

        prefab.AddOrGet<LogicResourceSensor>();

        var visualizer = prefab.AddOrGet<RangeVisualizer>();
        // with TestLineOfSight false only the BlockingCB matters, and it is called for every cell
        // in the range we are visualizing.
        visualizer.TestLineOfSight     = false;
        visualizer.BlockingTileVisible = false;
        visualizer.OriginOffset        = Vector2I.zero;
        visualizer.RangeMin            = Vector2I.zero;
        visualizer.RangeMax            = Vector2I.minusone;
        // rangeVisualizer.BlockingCb will be set during OnSpawn!
    }

    // According to PLib, these are necessary times to call CreateLogicPorts.
    public override void DoPostConfigureUnderConstruction(GameObject go) {
        base.DoPostConfigureUnderConstruction(go);
        LogicResourceSensor.CreateLogicPorts(go);
    }

    public override void DoPostConfigurePreview(BuildingDef def, GameObject go) {
        base.DoPostConfigurePreview(def, go);
        LogicResourceSensor.CreateLogicPorts(go);
   }
}
