namespace SlippyCheeze.SupportCode;

public static class KleiExtensions {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ToKelvin(this float celsius) => Constants.CELSIUS2KELVIN + celsius;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ToKelvin(this int celsius) => Constants.CELSIUS2KELVIN + celsius;

    // gonna manually add the optional parameters as needed, to avoid copying (too many) default
    // values from Klei code into my own.  wish I could `using static` part of a class. :(
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string FormattedMass(this float mass) => GameUtil.GetFormattedMass(mass);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string FormattedTemperature(this float temperature) => GameUtil.GetFormattedTemperature(temperature);


    // According to Peter Han, Unity `TryGetComponent` is faster than `GetComponent is null`, so
    // lets respect that and take advantage.
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasComponent<T>(this GameObject go) where T: Component
        => go.TryGetComponent<T>(out T _);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasComponent<T>(this Component cmp) where T: Component
        => cmp.gameObject.HasComponent<T>();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deconstruct(this Vector2I vec, out int x, out int y) {
        x = vec.x;
        y = vec.y;
    }


    private static int[] noCellsInCavity = [];
    public static IEnumerable<int> CavityCells(this Room room) => room?.cavity?.CavityCells() ?? noCellsInCavity;
    public static IEnumerable<int> CavityCells(this CavityInfo cavity) {
        RoomProber prober = Game.Instance.roomProber;
        for (int y = cavity.minY; y <= cavity.maxY; y++) {
            for (int x = cavity.minX; x <= cavity.maxX; x++) {
                // cavity equality is the robust indicator of being in the same room.
                int cell = Grid.XYToCell(x, y);
                if (cavity == prober.GetCavityForCell(cell))
                    yield return cell;
            }
        }
    }
}


// constants is probably the wrong name now. :(
public static class LogicPortDefs {
    public static LogicPorts.Port SingleInputPort(HashedString ID, int x = 0, int y = 0)
        => LogicPorts.Port.InputPort(
            ID,
            new CellOffset(x, y),
            STRINGS.UI.LOGIC_PORTS.GATE_SINGLE_INPUT_ONE_NAME,
            STRINGS.UI.LOGIC_PORTS.GATE_SINGLE_INPUT_ONE_ACTIVE,
            STRINGS.UI.LOGIC_PORTS.GATE_SINGLE_INPUT_ONE_INACTIVE,
            show_wire_missing_icon: true,
            display_custom_name:    true
        );

    public static LogicPorts.Port SingleOutputPort(HashedString ID, int x = 0, int y = 0)
        => LogicPorts.Port.OutputPort(
            ID,
            new(x, y),
            STRINGS.UI.LOGIC_PORTS.GATE_SINGLE_OUTPUT_ONE_NAME,
            STRINGS.UI.LOGIC_PORTS.GATE_SINGLE_OUTPUT_ONE_ACTIVE,
            STRINGS.UI.LOGIC_PORTS.GATE_SINGLE_OUTPUT_ONE_INACTIVE,
            show_wire_missing_icon: true,
            display_custom_name:    true
        );

    // sadly, this one can't have predefined strings, so we have to force getting the string from elsewhere.
    public static LogicPorts.Port FilteredStorageOutput(string root, int x = 0, int y = 0)
        => LogicPorts.Port.OutputPort(
            FilteredStorage.FULL_PORT_ID,
            new CellOffset(x, y),
            Strings.Get($"{root}.LOGIC_PORT"),
            Strings.Get($"{root}.LOGIC_PORT_ACTIVE"),
            Strings.Get($"{root}.LOGIC_PORT_INACTIVE")
        );

    public static LogicPorts.Port LogicOperationalControllerInput(int x = 0, int y = 0)
        // one of the very few classes that does provide this, every else copy/pastes the def. :(
        => LogicOperationalController.CreateSingleInputPortList(new CellOffset(x, y))[0];

    public static LogicPorts.Port SmartReservoirOutput(int x = 0, int y = 0)
        => LogicPorts.Port.OutputPort(
            SmartReservoir.PORT_ID,
            new CellOffset(x, y),
            STRINGS.BUILDINGS.PREFABS.SMARTRESERVOIR.LOGIC_PORT,
            STRINGS.BUILDINGS.PREFABS.SMARTRESERVOIR.LOGIC_PORT_ACTIVE,
            STRINGS.BUILDINGS.PREFABS.SMARTRESERVOIR.LOGIC_PORT_INACTIVE
        );
}
