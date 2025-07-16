// This provides the baseline ModMain that *all* my mods use.  Individual mods shouldn't be
// customizing anything here, they should be implementing one of the hooks this works with to do
// their things.
using System.Diagnostics;

using KMod;

using PeterHan.PLib.Buildings;
using PeterHan.PLib.Core;

using SlippyCheeze.SupportCode.LogErrorNotifier;


namespace SlippyCheeze;
[HarmonyPatch]                  // for our ModPatch helper hooks. :)
public partial class ModMain: UserMod2 {
    // globally available values, at least as soon as we can possibly supply them.
    public static ModMain Instance = null!;
    public static Harmony Harmony  = null!;

    // instance values.  not so much available everywhere, but close enough.
    public PBuildingManager BuildingManager = null!;
    public static void Register(PBuilding building) => ModMain.Instance.BuildingManager.Register(building);

    public override void OnLoad(Harmony harmony) {
        Instance = this;    // as early as possible, yo.
        Harmony = harmony;

        PUtil.InitLibrary(false);
        BuildingManager = new();

        // This implicitly causes the LogErrorNotifier remote components to register with PLib.
        var RemoteLogVersion = RemoteLogListener.Instance.Version;
        L.log($"{ModName} is now loading (Assembly: {assembly}, RemoteLog: {RemoteLogVersion})\n    {ModDescription}");


        // `base.OnLoad` calls `harmony.PatchAll(this.assembly)`, triggering all Harmony patch
        // processing.  Thankfully, it has done nothing else close enough to forever that I'm OK
        // skipping calling it, and handling Harmony myself since I want to do a bunch of custom
        // stuff around timing it, and around patching other mods.
        var stopwatch = Stopwatch.StartNew();

        // Register STRINGS translation keys; ONI handles everything once we hand it the top level
        // category Type object, which we collected at compile-time.
        foreach (Type root in AllModStringsRoots) {
            L.log($"Registering MODSTRINGS root {root.Name}");
            LocString.CreateLocStringKeys(root);
        }

        // Now, run through all the HarmonyPatch annotated classes in the mod, which includes
        // anything with a `ModPatch` annotation.  For each, if we are able to, apply the
        // Harmony patches.
        //
        // If we can't apply a ModPatch yet, because ONI hasn't loaded the dependent mod, we pop it
        // into a list of deferred patches and apply it as soon as those dependent mods load.
        //
        // If that doesn't happen then we report it in OnAllModsLoaded, since we know it either
        // happened, or will never happen, at that point.
        TryApplyingAllPatches(harmony);

        CallOnModLoadedHooks();

        // vanity, but whatever, might as well know how much it cost to do these things.
        stopwatch.Stop();
        L.debug($"total time spent scanning and patching: {stopwatch.Elapsed.Humanize(16)}");
    }

    public override void OnAllModsLoaded(Harmony harmony, IReadOnlyList<Mod> mods) {
        base.OnAllModsLoaded(harmony, mods);
        CallOnAllModsLoadedHooks(mods);

        // Report any ModPatch load failures.
        if (PendingModPatches.Count > 0) {
            L.warn($"Never applied {PendingModPatches.Count} ModPatch classes:");
            foreach (var type in PendingModPatches)
                L.warn($"For {type}:\n - {String.Join("\n - ", type.ModPatchNotReadyBecause())}");
        }
    }

    private void TryApplyingAllPatches(Harmony harmony) {
        // OK, so, fun times, but I can't use `harmony.PatchAll()` if I want to be able to delay
        // *some* mods from loading, and the ONI version of Harmony doesn't support the whole
        // HarmonyCategory thing. -_-
        //
        // so, instead, I hand-roll a version of PatchAll myself.  whee.  OTOH, that method is
        // literally just this:
        //
        // AccessTools.GetTypesFromAssembly(assembly).Do(type => CreateClassProcessor(type).Patch());

        if (HarmonyPatches.Length > 0) {
            L.log($"Applying {HarmonyPatches.Length} HarmonyPatch marked {"class".ToQuantity(HarmonyPatches.Length, ShowQuantityAs.None)}");
            foreach (Type type in HarmonyPatches) {
                string? comment = type.GetComment();
                if (!comment.IsNullOrEmpty())
                    L.WriteLogLine(L.INFO, comment, type.Name);

                // we know it has a HarmonyPatch, but no ModPatch, simple to apply now.
                ApplyHarmonyPatchesFrom(type);
            }
        }

        if (ModPatches.Length > 0) {
            L.log($"Checking {ModPatches.Length} ModPatch marked {"class".ToQuantity(ModPatches.Length, ShowQuantityAs.None)}");
            // mark all the ModPatch types pending, and...
            PendingModPatches.AddRange(ModPatches);
            // ...invoke the "mod loaded" handler for the first time, to apply any that are ready.
            OnModAssemblyLoaded(this.mod);
        }

        int pending = PendingModPatches.Count;
        int total   = HarmonyPatches.Length + ModPatches.Length;
        L.log($"Applied {"patch".ToQuantity(total - pending)} and have {pending} pending mod loading");
    }


    private static bool ApplyHarmonyPatchesFrom(Type type) {
        L.log(type.FullName);   // since the function name tells us everything else. :)
        ModMain.Harmony.CreateClassProcessor(type).Patch();
        return true;
    }


    // A list of named Type objects which all have a ModPatch attribute, which we will attempt to
    // load as soon as they are ready.
    private static readonly List<Type> PendingModPatches = [];

    // hook into KMod DLL loading, so we can trigger harmony processing of a type after the mod it
    // depends on is loaded.
    [HarmonyPatch(typeof(KMod.DLLLoader), nameof(KMod.DLLLoader.LoadDLLs))]
    [HarmonyPostfix]
    public static void OnModAssemblyLoaded(KMod.Mod ownerMod) {
        var mods = ModPatch.AllModsWithLoadedCode;  // minor optimization, but whatevs.
        PendingModPatches.RemoveAll(type => type.ModPatchReady(mods) && ApplyHarmonyPatchesFrom(type));
    }
}
