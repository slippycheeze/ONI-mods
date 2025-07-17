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



public static class ElementTags {
    public static readonly Tag Aerogel                = (Tag) "Aerogel";
    public static readonly Tag Algae                  = (Tag) "Algae";
    public static readonly Tag Aluminum               = (Tag) "Aluminum";
    public static readonly Tag AluminumGas            = (Tag) "AluminumGas";
    public static readonly Tag AluminumOre            = (Tag) "AluminumOre";
    public static readonly Tag Amber                  = (Tag) "Amber";
    public static readonly Tag Bitumen                = (Tag) "Bitumen";
    public static readonly Tag BleachStone            = (Tag) "BleachStone";
    public static readonly Tag Brick                  = (Tag) "Brick";
    public static readonly Tag Brine                  = (Tag) "Brine";
    public static readonly Tag BrineIce               = (Tag) "BrineIce";
    public static readonly Tag COMPOSITION            = (Tag) "COMPOSITION";
    public static readonly Tag Carbon                 = (Tag) "Carbon";
    public static readonly Tag CarbonDioxide          = (Tag) "CarbonDioxide";
    public static readonly Tag CarbonFibre            = (Tag) "CarbonFibre";
    public static readonly Tag CarbonGas              = (Tag) "CarbonGas";
    public static readonly Tag Cement                 = (Tag) "Cement";
    public static readonly Tag CementMix              = (Tag) "CementMix";
    public static readonly Tag Ceramic                = (Tag) "Ceramic";
    public static readonly Tag Chlorine               = (Tag) "Chlorine";
    public static readonly Tag ChlorineGas            = (Tag) "ChlorineGas";
    public static readonly Tag Cinnabar               = (Tag) "Cinnabar";
    public static readonly Tag Clay                   = (Tag) "Clay";
    public static readonly Tag Cobalt                 = (Tag) "Cobalt";
    public static readonly Tag CobaltGas              = (Tag) "CobaltGas";
    public static readonly Tag Cobaltite              = (Tag) "Cobaltite";
    public static readonly Tag ContaminatedOxygen     = (Tag) "ContaminatedOxygen";
    public static readonly Tag Copper                 = (Tag) "Copper";
    public static readonly Tag CopperGas              = (Tag) "CopperGas";
    public static readonly Tag Corium                 = (Tag) "Corium";
    public static readonly Tag Creature               = (Tag) "Creature";
    public static readonly Tag CrudeOil               = (Tag) "CrudeOil";
    public static readonly Tag CrushedIce             = (Tag) "CrushedIce";
    public static readonly Tag CrushedRock            = (Tag) "CrushedRock";
    public static readonly Tag Cuprite                = (Tag) "Cuprite";
    public static readonly Tag DepletedUranium        = (Tag) "DepletedUranium";
    public static readonly Tag Diamond                = (Tag) "Diamond";
    public static readonly Tag Dirt                   = (Tag) "Dirt";
    public static readonly Tag DirtyIce               = (Tag) "DirtyIce";
    public static readonly Tag DirtyWater             = (Tag) "DirtyWater";
    public static readonly Tag Electrum               = (Tag) "Electrum";
    public static readonly Tag EnrichedUranium        = (Tag) "EnrichedUranium";
    public static readonly Tag Ethanol                = (Tag) "Ethanol";
    public static readonly Tag EthanolGas             = (Tag) "EthanolGas";
    public static readonly Tag Fallout                = (Tag) "Fallout";
    public static readonly Tag Fertilizer             = (Tag) "Fertilizer";
    public static readonly Tag FoolsGold              = (Tag) "FoolsGold";
    public static readonly Tag Fossil                 = (Tag) "Fossil";
    public static readonly Tag FrozenPhytoOil         = (Tag) "FrozenPhytoOil";
    public static readonly Tag Fullerene              = (Tag) "Fullerene";
    public static readonly Tag Glass                  = (Tag) "Glass";
    public static readonly Tag Gold                   = (Tag) "Gold";
    public static readonly Tag GoldAmalgam            = (Tag) "GoldAmalgam";
    public static readonly Tag GoldGas                = (Tag) "GoldGas";
    public static readonly Tag Granite                = (Tag) "Granite";
    public static readonly Tag Graphite               = (Tag) "Graphite";
    public static readonly Tag Gunk                   = (Tag) "Gunk";
    public static readonly Tag HardPolypropylene      = (Tag) "HardPolypropylene";
    public static readonly Tag Helium                 = (Tag) "Helium";
    public static readonly Tag Hydrogen               = (Tag) "Hydrogen";
    public static readonly Tag Ice                    = (Tag) "Ice";
    public static readonly Tag IgneousRock            = (Tag) "IgneousRock";
    public static readonly Tag Iridium                = (Tag) "Iridium";
    public static readonly Tag IridiumGas             = (Tag) "IridiumGas";
    public static readonly Tag Iron                   = (Tag) "Iron";
    public static readonly Tag IronGas                = (Tag) "IronGas";
    public static readonly Tag IronOre                = (Tag) "IronOre";
    public static readonly Tag Isoresin               = (Tag) "Isoresin";
    public static readonly Tag Katairite              = (Tag) "Katairite";
    public static readonly Tag Lead                   = (Tag) "Lead";
    public static readonly Tag LeadGas                = (Tag) "LeadGas";
    public static readonly Tag Lime                   = (Tag) "Lime";
    public static readonly Tag LiquidCarbonDioxide    = (Tag) "LiquidCarbonDioxide";
    public static readonly Tag LiquidGunk             = (Tag) "LiquidGunk";
    public static readonly Tag LiquidHelium           = (Tag) "LiquidHelium";
    public static readonly Tag LiquidHydrogen         = (Tag) "LiquidHydrogen";
    public static readonly Tag LiquidMethane          = (Tag) "LiquidMethane";
    public static readonly Tag LiquidOxygen           = (Tag) "LiquidOxygen";
    public static readonly Tag LiquidPhosphorus       = (Tag) "LiquidPhosphorus";
    public static readonly Tag LiquidPropane          = (Tag) "LiquidPropane";
    public static readonly Tag LiquidSulfur           = (Tag) "LiquidSulfur";
    public static readonly Tag MaficRock              = (Tag) "MaficRock";
    public static readonly Tag Magma                  = (Tag) "Magma";
    public static readonly Tag Mercury                = (Tag) "Mercury";
    public static readonly Tag MercuryGas             = (Tag) "MercuryGas";
    public static readonly Tag Methane                = (Tag) "Methane";
    public static readonly Tag Milk                   = (Tag) "Milk";
    public static readonly Tag MilkFat                = (Tag) "MilkFat";
    public static readonly Tag MilkIce                = (Tag) "MilkIce";
    public static readonly Tag MoltenAluminum         = (Tag) "MoltenAluminum";
    public static readonly Tag MoltenCarbon           = (Tag) "MoltenCarbon";
    public static readonly Tag MoltenCobalt           = (Tag) "MoltenCobalt";
    public static readonly Tag MoltenCopper           = (Tag) "MoltenCopper";
    public static readonly Tag MoltenGlass            = (Tag) "MoltenGlass";
    public static readonly Tag MoltenGold             = (Tag) "MoltenGold";
    public static readonly Tag MoltenIridium          = (Tag) "MoltenIridium";
    public static readonly Tag MoltenIron             = (Tag) "MoltenIron";
    public static readonly Tag MoltenLead             = (Tag) "MoltenLead";
    public static readonly Tag MoltenNickel           = (Tag) "MoltenNickel";
    public static readonly Tag MoltenNiobium          = (Tag) "MoltenNiobium";
    public static readonly Tag MoltenSalt             = (Tag) "MoltenSalt";
    public static readonly Tag MoltenSteel            = (Tag) "MoltenSteel";
    public static readonly Tag MoltenSucrose          = (Tag) "MoltenSucrose";
    public static readonly Tag MoltenSyngas           = (Tag) "MoltenSyngas";
    public static readonly Tag MoltenTungsten         = (Tag) "MoltenTungsten";
    public static readonly Tag MoltenUranium          = (Tag) "MoltenUranium";
    public static readonly Tag Mud                    = (Tag) "Mud";
    public static readonly Tag Naphtha                = (Tag) "Naphtha";
    public static readonly Tag NaturalResin           = (Tag) "NaturalResin";
    public static readonly Tag NaturalSolidResin      = (Tag) "NaturalSolidResin";
    public static readonly Tag Nickel                 = (Tag) "Nickel";
    public static readonly Tag NickelGas              = (Tag) "NickelGas";
    public static readonly Tag NickelOre              = (Tag) "NickelOre";
    public static readonly Tag Niobium                = (Tag) "Niobium";
    public static readonly Tag NiobiumGas             = (Tag) "NiobiumGas";
    public static readonly Tag NuclearWaste           = (Tag) "NuclearWaste";
    public static readonly Tag Obsidian               = (Tag) "Obsidian";
    public static readonly Tag OxyRock                = (Tag) "OxyRock";
    public static readonly Tag Oxygen                 = (Tag) "Oxygen";
    public static readonly Tag Peat                   = (Tag) "Peat";
    public static readonly Tag Petroleum              = (Tag) "Petroleum";
    public static readonly Tag PhosphateNodules       = (Tag) "PhosphateNodules";
    public static readonly Tag Phosphorite            = (Tag) "Phosphorite";
    public static readonly Tag Phosphorus             = (Tag) "Phosphorus";
    public static readonly Tag PhosphorusGas          = (Tag) "PhosphorusGas";
    public static readonly Tag PhytoOil               = (Tag) "PhytoOil";
    public static readonly Tag Polypropylene          = (Tag) "Polypropylene";
    public static readonly Tag Propane                = (Tag) "Propane";
    public static readonly Tag Radium                 = (Tag) "Radium";
    public static readonly Tag RefinedCarbon          = (Tag) "RefinedCarbon";
    public static readonly Tag RefinedLipid           = (Tag) "RefinedLipid";
    public static readonly Tag Regolith               = (Tag) "Regolith";
    public static readonly Tag Resin                  = (Tag) "Resin";
    public static readonly Tag RockGas                = (Tag) "RockGas";
    public static readonly Tag Rust                   = (Tag) "Rust";
    public static readonly Tag Salt                   = (Tag) "Salt";
    public static readonly Tag SaltGas                = (Tag) "SaltGas";
    public static readonly Tag SaltWater              = (Tag) "SaltWater";
    public static readonly Tag Sand                   = (Tag) "Sand";
    public static readonly Tag SandCement             = (Tag) "SandCement";
    public static readonly Tag SandStone              = (Tag) "SandStone";
    public static readonly Tag SedimentaryRock        = (Tag) "SedimentaryRock";
    public static readonly Tag Shale                  = (Tag) "Shale";
    public static readonly Tag Slabs                  = (Tag) "Slabs";
    public static readonly Tag SlimeMold              = (Tag) "SlimeMold";
    public static readonly Tag Snow                   = (Tag) "Snow";
    public static readonly Tag SolidCarbonDioxide     = (Tag) "SolidCarbonDioxide";
    public static readonly Tag SolidChlorine          = (Tag) "SolidChlorine";
    public static readonly Tag SolidCrudeOil          = (Tag) "SolidCrudeOil";
    public static readonly Tag SolidEthanol           = (Tag) "SolidEthanol";
    public static readonly Tag SolidHydrogen          = (Tag) "SolidHydrogen";
    public static readonly Tag SolidMercury           = (Tag) "SolidMercury";
    public static readonly Tag SolidMethane           = (Tag) "SolidMethane";
    public static readonly Tag SolidNaphtha           = (Tag) "SolidNaphtha";
    public static readonly Tag SolidNuclearWaste      = (Tag) "SolidNuclearWaste";
    public static readonly Tag SolidOxygen            = (Tag) "SolidOxygen";
    public static readonly Tag SolidPetroleum         = (Tag) "SolidPetroleum";
    public static readonly Tag SolidPropane           = (Tag) "SolidPropane";
    public static readonly Tag SolidResin             = (Tag) "SolidResin";
    public static readonly Tag SolidSuperCoolant      = (Tag) "SolidSuperCoolant";
    public static readonly Tag SolidSyngas            = (Tag) "SolidSyngas";
    public static readonly Tag SolidViscoGel          = (Tag) "SolidViscoGel";
    public static readonly Tag SourGas                = (Tag) "SourGas";
    public static readonly Tag StableSnow             = (Tag) "StableSnow";
    public static readonly Tag Steam                  = (Tag) "Steam";
    public static readonly Tag Steel                  = (Tag) "Steel";
    public static readonly Tag SteelGas               = (Tag) "SteelGas";
    public static readonly Tag Sucrose                = (Tag) "Sucrose";
    public static readonly Tag SugarWater             = (Tag) "SugarWater";
    public static readonly Tag Sulfur                 = (Tag) "Sulfur";
    public static readonly Tag SulfurGas              = (Tag) "SulfurGas";
    public static readonly Tag SuperCoolant           = (Tag) "SuperCoolant";
    public static readonly Tag SuperCoolantGas        = (Tag) "SuperCoolantGas";
    public static readonly Tag SuperInsulator         = (Tag) "SuperInsulator";
    public static readonly Tag Syngas                 = (Tag) "Syngas";
    public static readonly Tag Tallow                 = (Tag) "Tallow";
    public static readonly Tag TempConductorSolid     = (Tag) "TempConductorSolid";
    public static readonly Tag ToxicMud               = (Tag) "ToxicMud";
    public static readonly Tag ToxicSand              = (Tag) "ToxicSand";
    public static readonly Tag Tungsten               = (Tag) "Tungsten";
    public static readonly Tag TungstenGas            = (Tag) "TungstenGas";
    public static readonly Tag Unobtanium             = (Tag) "Unobtanium";
    public static readonly Tag UraniumOre             = (Tag) "UraniumOre";
    public static readonly Tag Vacuum                 = (Tag) "Vacuum";
    public static readonly Tag ViscoGel               = (Tag) "ViscoGel";
    public static readonly Tag Void                   = (Tag) "Void";
    public static readonly Tag Water                  = (Tag) "Water";
    public static readonly Tag Wolframite             = (Tag) "Wolframite";
    public static readonly Tag WoodLog                = (Tag) "WoodLog";
    public static readonly Tag Yellowcake             = (Tag) "Yellowcake";
}


[HarmonyPatch(typeof(ElementLoader), nameof(ElementLoader.Load))]
public static class KleiSimHashesAndTags {
    private static Dictionary<SimHashes, Tag> toTag      = [];
    private static Dictionary<Tag, SimHashes> toSimHash  = [];

    private static void Add(Element element) {
        toTag[element.id] = element.tag;
        toSimHash[element.tag] = element.id;
    }

    [HarmonyPriority(Priority.Last)]
    public static void Potsfix() {
        foreach (var element in ElementLoader.elements)
            KleiSimHashesAndTags.Add(element);
    }

    public static SimHashes ToSimHash(this Tag tag) {
        if (toSimHash.TryGetValue(tag, out var value))
            return value;

        Element? element = ElementLoader.FindElementByTag(tag) ?? throw new InvalidOperationException($"Tag {tag} is not an element!");

        L.error($"Tag {tag} was not found in our cache, but was in ElementLoader, this should never happen!");
        KleiSimHashesAndTags.Add(element);
        return element.id;
    }

    public static Tag ToTag(this SimHashes simHash) {
        if (toTag.TryGetValue(simHash, out var value))
            return value;

        Element? element = ElementLoader.FindElementByHash(simHash) ?? throw new InvalidOperationException($"SimHashes {simHash} is not an element!");

        L.error($"SimHashes {simHash} was not found in our cache, but was in ElementLoader, this should never happen!");
        KleiSimHashesAndTags.Add(element);
        return element.tag;
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
