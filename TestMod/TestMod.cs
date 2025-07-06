using HarmonyLib;

using KMod;

namespace SlippyCheeze.TestMod;

public partial class ModMain: UserMod2 {
    public override void OnLoad(Harmony harmony) {
        Console.WriteLine(MODSTRINGS.UI.TEST);
        Console.WriteLine(MODSTRINGS.UI.TEST_key);
    }
}
