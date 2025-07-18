namespace SlippyCheeze.PressureValves;

// NOTE: based on the LiquidLimitValve anim, so should follow their model for animation control.
public abstract partial class BasePressureValveConfig(string ID): IBuildingConfig {
    internal abstract PBuilding   Building    { get; }
    internal abstract ConduitType ConduitType { get; }

    internal static PBuilding MakeBuilding(
        string ID,
        string name,
        ConduitType conduitType,
        string animation,
        string tech,
        string category,
        string subCategory,
        string? addAfter = null
    ) => new(ID, name) {
        Tech              = tech,
        Category          = category,
        SubCategory       = subCategory,
        AddAfter          = addAfter,

        Animation         = animation,

        Description       = null,     // use strings lookup, have to be explicit
        EffectText        = null,

        HP                = 30,
        Width             = 1,
        Height            = 2,

        Floods            = false,

        Decor             = TUNING.BUILDINGS.DECOR.PENALTY.TIER0,

        Ingredients = { new(TUNING.MATERIALS.RAW_METALS, tier: 3) },
        ConstructionTime = 10.0f,
        AudioCategory    = "Metal",

        ViewMode   = conduitType switch {
            ConduitType.Liquid => OverlayModes.LiquidConduits.ID,
            ConduitType.Gas    => OverlayModes.GasConduits.ID,
        },
        Placement  = BuildLocationRule.Anywhere,
        RotateMode = PermittedRotations.R360,

        PowerInput = new(10f, new CellOffset(0, 1)),

        InputConduits  = { new(conduitType, new CellOffset(0, 0)) },
        OutputConduits = { new(conduitType, new CellOffset(0, 1)) },

        // 2025-07-17 REVISIT: I'm kind of torn about adding a logic port to control the building,
        // especially as I'd like it to act as a strict "pass-through" with no power consumption
        // when disabled and I don't think Klei ... really have that as a feature in their comps.
    };

    public override BuildingDef CreateBuildingDef() {
        var def = Building.CreateDef();

        var overlay = ConduitType switch {
            ConduitType.Liquid => OverlayScreen.LiquidVentIDs,
            ConduitType.Gas    => OverlayScreen.GasVentIDs,
        };
        GeneratedBuildings.RegisterWithOverlay(overlay, ID);

        return def;
    }

    public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag) {
        base.ConfigureBuildingTemplate(go, prefab_tag);
        Building.ConfigureBuildingTemplate(go);

        // anim controller used by LiquidLimitValveConfig, matches our pilfered animation.
        go.AddOrGetDef<PoweredActiveTransitionController.Def>();
        BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);

        var storage = go.AddComponent<Storage>();
        // Klei, also, use the gas modifiers for liquids.
        storage.SetDefaultStoredItemModifiers(GasReservoirConfig.ReservoirStoredItemModifiers);
        storage.showDescriptor   = false;
        storage.allowItemRemoval = false;
        storage.storageFilters   = ConduitType switch {
            ConduitType.Liquid => TUNING.STORAGEFILTERS.LIQUIDS,
            ConduitType.Gas    => TUNING.STORAGEFILTERS.GASES,
        };

        // exceed a small number of different input types and you are likely but not certain to see
        // partial packets output.  anything below that should definitely not exceed the
        // output limit.
        storage.capacityKg       = 2 * ConduitType switch {
            ConduitType.Liquid => ConduitFlow.MAX_LIQUID_MASS,
            ConduitType.Gas    => ConduitFlow.MAX_GAS_MASS
        };

        switch (ConduitType) {
            case ConduitType.Liquid:
                go.AddComponent<LiquidPressureValve>();
                break;
            case ConduitType.Gas:
                go.AddComponent<GasPressureValve>();
                break;
        }
    }

    public override void DoPostConfigureUnderConstruction(GameObject go) {
        base.DoPostConfigureUnderConstruction(go);
        Building.CreateLogicPorts(go);
    }

    public override void DoPostConfigurePreview(BuildingDef def, GameObject go) {
        base.DoPostConfigureUnderConstruction(go);
        Building.CreateLogicPorts(go);
    }

    public override void DoPostConfigureComplete(GameObject go) {
        Building.DoPostConfigureComplete(go);
        Building.CreateLogicPorts(go);

        if (go.TryGetComponent<ConduitConsumer>(out var consumer))
            UnityEngine.Object.DestroyImmediate(consumer);
        if (go.TryGetComponent<ConduitDispenser>(out var dispenser))
            UnityEngine.Object.DestroyImmediate(dispenser);

        // we manage our own conduit connectivity, thanks.
        go.GetComponent<RequireInputs>().SetRequirements(power: true, conduit: false);
    }
}
