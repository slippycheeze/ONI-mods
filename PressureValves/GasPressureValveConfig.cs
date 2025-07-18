namespace SlippyCheeze.PressureValves;

// NOTE: based on the LiquidLimitValve anim, so should follow their model for animation control.
public partial class GasPressureValveConfig(): BasePressureValveConfig(ID) {
    public const string ID = "GasPressureValve";

    public static void OnModLoaded() => ModMain.Register(StaticBuilding);

    public static PBuilding StaticBuilding => field ??= MakeBuilding(
        ID:           ID,
        name:         MODSTRINGS.BUILDINGS.PREFABS.GASPRESSUREVALVE.NAME,
        conduitType:  ConduitType.Gas,
        animation:    "gas_pressure_valve_kanim",
        tech:         ResearchID.Gases.ImprovedVentilation,
        category:     KleiPlan.HVAC.ID,
        subCategory:  KleiPlan.HVAC.valves,
        addAfter:     GasLimitValveConfig.ID
    );

    internal override PBuilding Building      => GasPressureValveConfig.StaticBuilding;
    internal override ConduitType ConduitType => ConduitType.Gas;
}
