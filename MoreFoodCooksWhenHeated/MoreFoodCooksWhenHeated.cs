using FoodInfo = EdiblesManager.FoodInfo;

namespace SlippyCheeze.MoreFoodCooksWhenHeated;

[HarmonyPatch]
public static partial class MoreFoodCooksWhenHeated {
    // I'm indecisive if the temperature should vary by item, but for now stick with the simple rule
    // of one temperature for all...
    public const float CookingTemperature = Constants.CELSIUS2KELVIN + 71;

    // raw, cooked
    public static Dictionary<string, string> Recipes = new() {
        // ==================== Electric Grill ====================
        // Mush Fry
        {MushBarConfig.ID,              FriedMushBarConfig.ID},
        // Gristle Berry
        {PrickleFruitConfig.ID,         GrilledPrickleFruitConfig.ID},
        // Fried Mushroom
        {MushroomConfig.ID,             FriedMushroomConfig.ID},
        // Omelette — this is in base game, but my code won't duplicate the comp.
        {RawEggConfig.ID,               CookedEggConfig.ID},
        // Barbeque
        {MeatConfig.ID,                 CookedMeatConfig.ID},
        // Cooked Seafood - both of 'em
        {FishMeatConfig.ID,             CookedFishConfig.ID},
        {ShellfishMeatConfig.ID,        CookedFishConfig.ID},
        // Swampy Delights
        {SwampFruitConfig.ID,           SwampDelightsConfig.ID},
        // Roast Grubfruit Nut
        {WormBasicFruitConfig.ID,       WormBasicFoodConfig.ID},
        // Pikeapple Skewers
        {HardSkinBerryConfig.ID,        CookedPikeappleConfig.ID},
        // Toasted Mimillet, yup, the recipe wants the seed, directly.
        {ButterflyPlantConfig.SEED_ID,  ButterflyFoodConfig.ID},

        // ====================     Smoker     ====================
        //
        // Note: only adding things that don't have an Electric Grill recipe.
        //
        // Also, I'm ... less certain I want this, but OTOH, I /do/ want DinosaurMeat to auto-cook
        // somehow, because that makes sense to me, so...

        // Tough Meat -> Tender Brisket
        {DinosaurMeatConfig.ID,         SmokedDinosaurMeatConfig.ID},
        // Vegie Poppers, from Sweatcorn, which hasn't an Electric Grill recipe.
        {GardenFoodPlantFoodConfig.ID,  SmokedVegetablesConfig.ID},
    };

    [HarmonyPatch(typeof(EntityConfigManager), nameof(EntityConfigManager.LoadGeneratedEntities))]
    [HarmonyPostfix]
    public static void AddTemperatureCookableToEntities() {
        foreach (var (rawID, cookedID) in Recipes) {
            // just in case I release, and someone is running without one of the DLC
            if (Assets.TryGetPrefab(rawID) is not GameObject raw)
                continue;
            if (raw.TryGetComponent<TemperatureCookable>(out var existing)) {
                L.debug($"{rawID} is already TemperatureCookable, to {existing.cookedID} (same? {existing.cookedID == cookedID})");
                continue;       // someone already set us up the cooking path.
            }

            if (Assets.TryGetPrefab(cookedID) is not GameObject cooked)
                continue;

            // we have both entities, so we can convert one to the other automatically.
            var cookable = raw.AddComponent<TemperatureCookable>();
            cookable.cookTemperature = CookingTemperature;
            cookable.cookedID        = cookedID;
        }
    }

    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateFoodEntries))]
    [HarmonyPostfix]
    public static void AddTemperatureCookableCodexInfoToFoods(Dictionary<string, CodexEntry> __result) {
        foreach (var (rawID, cookedID) in Recipes) {
            if (EdiblesManager.GetFoodInfo(rawID) is not FoodInfo raw) {
                L.error($"Can't find FoodInfo for {rawID}");
                continue;
            }

            if (EdiblesManager.GetFoodInfo(cookedID) is not FoodInfo cooked) {
                L.error($"Can't find FoodInfo for {cookedID}");
                continue;
            }

            // Mushroom: "Applications" => ELEMENTCONSUMEDBY
            if (__result.TryGetValue(rawID, out CodexEntry rawEntry)) {
                InsertSectionInto(rawEntry, STRINGS.CODEX.HEADERS.ELEMENTCONSUMEDBY, raw, cooked, CookingTemperature);
            } else {
                L.debug($"unexpectedly missing Food CodexEntry {rawID}");
            }

            // Fried Mushroom: "Produced By" => ELEMENTPRODUCEDBY
            if (__result.TryGetValue(cookedID, out CodexEntry cookedEntry)) {
                InsertSectionInto(cookedEntry, STRINGS.CODEX.HEADERS.ELEMENTPRODUCEDBY, raw, cooked, CookingTemperature);
            } else {
                L.debug($"unexpectedly missing Food CodexEntry {cookedID}");
            }
        }
    }

    private static void InsertSectionInto(CodexEntry entry, string where, FoodInfo raw, FoodInfo cooked, float temperature) {
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

        header.contents.content.Insert(0, new CodexTemperatureCookablePanel(raw, cooked, temperature));
    }
}
