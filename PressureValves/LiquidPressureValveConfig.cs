namespace SlippyCheeze.PressureValves;

// NOTE: based on the LiquidLimitValve anim, so should follow their model for animation control.
public partial class LiquidPressureValveConfig(): BasePressureValveConfig(ID) {
    public const string ID = "LiquidPressureValve";

    public static void OnModLoaded() => ModMain.Register(StaticBuilding);

    public static PBuilding StaticBuilding => field ??= MakeBuilding(
        ID:           ID,
        name:         MODSTRINGS.BUILDINGS.PREFABS.LIQUIDPRESSUREVALVE.NAME,
        conduitType:  ConduitType.Liquid,
        animation:    "liquid_pressure_valve_kanim",
        tech:         ResearchID.Liquids.ImprovedPlumbing,
        category:     KleiPlan.Plumbing.ID,
        subCategory:  KleiPlan.Plumbing.valves,
        addAfter:     LiquidLimitValveConfig.ID
    );

    internal override PBuilding Building      => LiquidPressureValveConfig.StaticBuilding;
    internal override ConduitType ConduitType => ConduitType.Liquid;
}
