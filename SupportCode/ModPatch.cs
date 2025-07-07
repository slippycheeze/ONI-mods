namespace SlippyCheeze.SupportCode;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = true)]
public class ModPatch(string staticID, ulong steamID): HarmonyPatch {
    public string StaticID { get; init; } = staticID;
    public ulong  SteamID  { get; init; } = steamID;

    public override string ToString() => $"[ModPatch({StaticID}, steamID: {SteamID})]";

    private bool ready = false;
    public bool Ready(IEnumerable<KMod.Mod>? modList = null) {
        if (!ready) {           // retry if we fail the first time.
            modList ??= Global.Instance.modManager.mods
                .Where(mod => (mod.loaded_content & KMod.Content.DLL) != 0);

            ready = modList.Any(mod => mod.staticID == StaticID);
        }
        return ready;
    }
}

public static class ModPatchExtensions {
    public static IEnumerable<ModPatch> ModPatches(this Type type) => type.GetCustomAttributes<ModPatch>();

    public static bool HasModPatch(this Type type) => type.ModPatches().Any();

    public static bool ModPatchReady(this Type type, IEnumerable<KMod.Mod>? modList = null)
        => type.ModPatches().All(patch => patch.Ready(modList));
}
