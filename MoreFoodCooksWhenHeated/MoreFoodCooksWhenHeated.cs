using FoodInfo = EdiblesManager.FoodInfo;

namespace SlippyCheeze.MoreFoodCooksWhenHeated;

[HarmonyPatch]
public static partial class MoreFoodCooksWhenHeated {
    // I'm indecisive if the temperature should vary by item, but for now stick with the simple rule
    // of one temperature for all...
    public const float CookingTemperature = Constants.CELSIUS2KELVIN + 71;

    public record struct CookingResult(string stationID, string rawID, string cookedID, float Temperature = CookingTemperature) {
        public bool Exists {
            get {
                if (Raw is null) {
                    L.warn($"{rawID} did not have a FoodInfo");
                    return false;
                }
                if (Cooked is null) {
                    L.warn($"{cookedID} did not have a FoodInfo");
                    return false;
                }
                if (Recipe is null) {
                    L.warn($"{rawID} to {cookedID} recipe '{RecipeID}' not found");
                    return false;
                }

                return true;
            }
        }

        public FoodInfo Raw    => EdiblesManager.GetFoodInfo(rawID);
        public FoodInfo Cooked => EdiblesManager.GetFoodInfo(cookedID);

        public GameObject RawPrefab    => Assets.GetPrefab(rawID);
        public GameObject CookedPrefab => Assets.GetPrefab(cookedID);

        public string RecipeID => ComplexRecipeManager.MakeRecipeID(
            stationID, [new(rawID, 0)], [new(cookedID, 0)]
        );

        // NOTE: DO **NOT** USE `WhateverConfig.recipe`, since Klei overwrite the
        // CookedMeatConfig.recipe value with the CookedFishConfig recipe due to programmer error,
        // and obvs they don't depend on them being correct since they didn't fix it.
        public ComplexRecipe Recipe => ComplexRecipeManager.Get().GetRecipe(RecipeID);

        public float CookedMassPerRawKG {
            get {
                var recipe = Recipe;
                return recipe.results[0].amount / recipe.ingredients[0].amount;
            }
        }
    }

    // only used during initialization, so no need to hold this persistently in memory forever.
    public static IEnumerable<CookingResult> CookingResults =>
        ((CookingResult[])[
            // ==================== Electric Grill ====================
            // Mush Fry
            new(CookingStationConfig.ID,    MushBarConfig.ID,               FriedMushBarConfig.ID),
            // Gristle Berry
            new(CookingStationConfig.ID,    PrickleFruitConfig.ID,          GrilledPrickleFruitConfig.ID),
            // Fried Mushroom
            new(CookingStationConfig.ID,    MushroomConfig.ID,              FriedMushroomConfig.ID),
            // Omelette — this is in base game, but my code won't duplicate the comp.
            new(CookingStationConfig.ID,    RawEggConfig.ID,                CookedEggConfig.ID),
            // Barbeque
            new(CookingStationConfig.ID,    MeatConfig.ID,                  CookedMeatConfig.ID),
            // Cooked Seafood - both of 'em
            new(CookingStationConfig.ID,    FishMeatConfig.ID,              CookedFishConfig.ID),
            new(CookingStationConfig.ID,    ShellfishMeatConfig.ID,         CookedFishConfig.ID),
            // Swampy Delights
            new(CookingStationConfig.ID,    SwampFruitConfig.ID,            SwampDelightsConfig.ID),
            // Roast Grubfruit Nut
            new(CookingStationConfig.ID,    WormBasicFruitConfig.ID,        WormBasicFoodConfig.ID),
            // Pikeapple Skewers
            new(CookingStationConfig.ID,    HardSkinBerryConfig.ID,         CookedPikeappleConfig.ID),
            // Toasted Mimillet, yup, the recipe wants the seed, directly.
            new(CookingStationConfig.ID,    ButterflyPlantConfig.SEED_ID,   ButterflyFoodConfig.ID),

            // ====================     Smoker     ====================
            //
            // Note: only adding things that don't have an Electric Grill recipe.
            //
            // Also, I'm ... less certain I want this, but OTOH, I /do/ want DinosaurMeat to auto-cook
            // somehow, because that makes sense to me, so...

            // Tough Meat -> Tender Brisket
            // new(SmokerConfig.ID,            DinosaurMeatConfig.ID,          SmokedDinosaurMeatConfig.ID),
            // Vegie Poppers, from Sweatcorn, which hasn't an Electric Grill recipe.
            // new(SmokerConfig.ID,            GardenFoodPlantFoodConfig.ID,   SmokedVegetablesConfig.ID),
        ])
        .Where(item => item.Exists);


    [HarmonyPatch(typeof(Assets), nameof(Assets.CreatePrefabs))]
    [HarmonyPostfix]
    public static void AddTemperatureCookableToEntities() {
        foreach (var result in CookingResults) {
            if (result.RawPrefab.TryGetComponent<TemperatureCookable>(out TemperatureCookable existing)) {
                if (existing.cookedID != result.cookedID) {
                    L.debug($"{result.rawID} is already TemperatureCookable, to {existing.cookedID} instead of {result.cookedID}");
                    continue;       // someone already set us up the cooking path.
                }

                UnityEngine.Object.Destroy(existing);  // buh-bye.  don't want you any longer.
            }

            // we have both entities, so we can convert one to the other automatically.
            var cookable = result.RawPrefab.AddComponent<ImprovedTemperatureCookable>();
            cookable.cookTemperature      = result.Temperature;
            cookable.cookedID             = result.cookedID;
            cookable.cookedMassMultiplier = result.CookedMassPerRawKG;

            L.debug($"{result.rawID} cooks to {result.cookedID} at 1:{result.CookedMassPerRawKG}");
        }
    }

    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateFoodEntries))]
    [HarmonyPostfix]
    public static void AddTemperatureCookableCodexInfoToFoods(Dictionary<string, CodexEntry> __result) {
        foreach (var result in CookingResults) {
            // Mushroom: "Applications" => ELEMENTCONSUMEDBY
            if (__result.TryGetValue(result.rawID, out CodexEntry rawEntry)) {
                InsertSectionInto(rawEntry, STRINGS.CODEX.HEADERS.ELEMENTCONSUMEDBY, result);
            } else {
                L.debug($"unexpectedly missing Food CodexEntry {result.rawID}");
            }

            // Fried Mushroom: "Produced By" => ELEMENTPRODUCEDBY
            if (__result.TryGetValue(result.cookedID, out CodexEntry cookedEntry)) {
                InsertSectionInto(cookedEntry, STRINGS.CODEX.HEADERS.ELEMENTPRODUCEDBY, result);
            } else {
                L.debug($"unexpectedly missing Food CodexEntry {result.cookedID}");
            }
        }
    }

    private static void InsertSectionInto(CodexEntry entry, string where, CookingResult result) {
        // performance: runs once per food item, at game load, and usually terminates early, so I'm
        // OK with the use of Linq here despite the GC pressure.
        CodexCollapsibleHeader? header = entry.contentContainers
            .SelectMany(static container => container.content)
            .OfType<CodexCollapsibleHeader>()
            .FirstOrDefault(header => header.label == where);

        if (header == null) {
            L.warn($"Expected to insert section into '{entry.sortString}' container '{where}', but it was not present");
            return;
        }

        header.contents.content.Insert(
            0,
            new CodexTemperatureCookablePanel(
                result.Raw,
                result.Cooked,
                result.CookedMassPerRawKG,
                result.Temperature
            )
        );
    }
}
