namespace SlippyCheeze.MicrochipsOnSolderingStation;

[ModPatch(staticID: "ComplexFabricatorRibbonController", steamID: 3478214979)]
[HarmonyPatch(typeof(AdvancedCraftingTableConfig), nameof(AdvancedCraftingTableConfig.ConfigureRecipes))]
public static class MicrochipsOnSolderingStation {
    // just a convenience for logging.
    private static string TargetName => STRINGS.UI.StripLinkFormatting(STRINGS.BUILDINGS.PREFABS.ADVANCEDCRAFTINGTABLE.NAME);

    public static void Postfix() {
        L.log($"Adding Microchip fabrication recipe to {TargetName}");

        // The recipe is literally a clone of what the PowerControlStation wants, save for the skill
        // perk, which I'm happy enough letting be the T2 Machinery skill, just like the AdvancedCraftingTable.
        // (which is good, because I can't have different perks on the same ComplexFabricator. :)

        // unlike the TinkerStation, which filters the *inputs* to determine what metal is used,
        // I want the user to be able to select it in the recipe just like building an AtmoSuit,
        // which incidentally uses this array managed by the game to configure the alternative metal
        // options for the recipe:
        ComplexRecipe.RecipeElement[] inputs = [
            new(GameTags.BasicRefinedMetals, PowerControlStationConfig.MASS_PER_TINKER) {
                inheritElement = true
            }
        ];

        // TINKER_TOOLS == PowerStationToolsConfig.tag == "Microchip"
        ComplexRecipe.RecipeElement[] outputs = [
            new(PowerControlStationConfig.TINKER_TOOLS, 1f)
        ];

        var ID = ComplexRecipeManager.MakeRecipeID("AdvancedCraftingTable", inputs, outputs);

        // the recipe will self-register, so I don't need to do anything but create the instance.
        new ComplexRecipe(ID, inputs, outputs) {
            time = 160f,   // TinkerStation.toolProductionTime default, used by PowerControlStation too.
            description = String.Format(
                STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.POWER_STATION_TOOLS.RECIPE_DESCRIPTION,
                STRINGS.MISC.TAGS.REFINEDMETAL
            ),
            // "with ingrediont" is static, even when you change the ingredient. :(
            nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
            fabricators = [AdvancedCraftingTableConfig.ID],
            // PowerControlStation:             Acoustics
            // AdvancedCraftingTable:           AdvancedResearch
            // ComplexFabricatorRecipeControl:  ParallelAutomation
            requiredTech = "ParallelAutomation",
            sortOrder = 100,   // might as well be probably definitely last on the list...
        };
    }
}
