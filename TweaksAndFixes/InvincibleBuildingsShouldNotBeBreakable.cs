// https://forums.kleientertainment.com/klei-bug-tracker/oni/destructive-duplicants-may-target-invincible-buildings-such-as-rocket-engines-r40223/
//
// Some building such as rocket engines are invincible, but they are also breakable.
// Destructive dupes throwing a tantrum will target them and end up just standing
// there, doing nothing.
namespace SlippyCheeze.TweaksAndFixes;

[HarmonyPatch]
public static class InvincibleBuildingsShouldNotBeBreakable {
    public static IEnumerable<MethodBase> TargetMethods() {
        yield return typeof(BuildingTemplates).DeclaredMethod(nameof(BuildingTemplates.CreateRocketBuildingDef));
        yield return typeof(BuildingTemplates).DeclaredMethod(nameof(BuildingTemplates.CreateMonumentBuildingDef));
    }

    public static void Postfix(BuildingDef def) {
        if (def.Invincible && def.Breakable) {
            def.Breakable = false;
        }
    }
}
