namespace SlippyCheeze.SupportCode;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = true)]
public class ModPatch(string staticID, ulong steamID): HarmonyPatch {
    public string StaticID { get; init; } = staticID;
    public ulong  SteamID  { get; init; } = steamID;

    public override string ToString() => $"[ModPatch({StaticID}, steamID: {SteamID})]";

    public static IEnumerable<KMod.Mod> AllModsWithLoadedCode =>
        Global.Instance.modManager.mods
        .Where(mod => (mod.loaded_content & KMod.Content.DLL) != 0);


    private bool ready = false;
    public bool Ready(IEnumerable<KMod.Mod> modList) {
        if (!ready) {           // retry if we fail the first time.
            ready = modList.Any(mod => mod.staticID == StaticID);
        }
        return ready;
    }

    public string? NotReadyBecause() => ready ? null : $"{this} was not found";
}

public static class ModPatchExtensions {
    public static IEnumerable<ModPatch> ModPatches(this Type type) => type.GetCustomAttributes<ModPatch>();

    public static bool HasModPatch(this Type type) => type.ModPatches().Any();

    public static bool ModPatchReady(this Type type, IEnumerable<KMod.Mod>? modList = null) {
        modList ??= ModPatch.AllModsWithLoadedCode;
        return type.ModPatches().All(patch => patch.Ready(modList ?? ModPatch.AllModsWithLoadedCode));
    }

    public static IEnumerable<string> ModPatchNotReadyBecause(this Type type)
        => type.ModPatches().Select(patch => patch.NotReadyBecause()).Where(s => s is not null).Cast<string>();
}
