using System.Diagnostics;

using Newtonsoft.Json;

namespace SlippyCheeze.DeveloperHelpers;

[HarmonyPatch]
internal static partial class DumpGameDatabases {
    private static JsonSerializerSettings serializerSettings = new() {
        Formatting            = Formatting.Indented,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
    };
    private static JsonSerializer json => field ??= JsonSerializer.Create(serializerSettings);

    private static string DatabaseRoot {
        get {
            if (field.IsNullOrEmpty()) {
                var info = new FileInfo(Path.Combine(KMod.Manager.GetDirectory(), "db"));
                if (!info.Exists) {
                    Directory.CreateDirectory(info.FullName);
                } else if (!((info.Attributes & FileAttributes.Directory) == FileAttributes.Directory)) {
                    throw new Exception($"{info.FullName} exists, but is not a directory?");
                }
                field = info.FullName;
            }
            return field;
        }
    }

    internal partial class DumpDatabaseJob(string name, object? db, bool thread = false) {
        public override string ToString() => $"{(thread ? "T" : "X")} database={name}";

        private static HashSet<string> AlreadyScheduled = [];

        public void ScheduleDump() {
            if (db == null) {   // just in case it a unity object, and overrides `==`, eh?
                L.error($"{this}: db is null, doing nothing.");
                return;
            }

            // don't schedule a dump for the same database twice.
            if (!AlreadyScheduled.Add(name)) {
                L.debug($"{this}: a dump is already scheduled, ignoring duplicate request.");
                return;
            }

            // we try and schedule for the next frame, but if the GameScheduler isn't available, we
            // immediately start the dump.
            if (GameScheduler.Instance is null) {
                L.debug($"{this}: GameScheduler.Instance is null, dumping immediately");
                StartDatabaseDump();
                return;
            }

            GameScheduler.Instance.ScheduleNextFrame(nameof(DumpDatabase), StartDatabaseDump);
        }

        internal void StartDatabaseDump(object? _ = null) {
            if (thread) {
                Task.Run(DumpDatabase);
            } else {
                DumpDatabase();
            }
        }

        public void DumpDatabase() {
            try {
                var timer = Stopwatch.StartNew();

                string basepath = Path.Combine(DatabaseRoot, $"{name}");
                string path     = basepath + ".json";
                string newpath  = basepath + ".new.json";
                string prevpath = basepath + ".prev.json";

                using (StreamWriter stream = File.CreateText(newpath)) {
                    json.Serialize(stream, db);
                }

                bool createBackup = true;

                // Microsoft are FUN: `File.Replace` **FORCES** a TOCTOU issue, by throwing if the file
                // being replaced does not exist.  Which is really precious, so I end up with this stream of
                // pain where we have to *assume* all sorts of things about the root cause of errors. :(
                //
                // anyway, first check and see if the file already exists, in a way that is actually
                // *atomic*, rather than race-vulnerable.
                try {
                    using (FileStream file = File.Open(path, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Read)) {
                        // we created the file, so no backup, please.
                        createBackup = false;
                    }
                } catch (IOException) {
                    // we did *NOT* create the file, backup is needed.
                    createBackup = true;
                }

                // if the file didn't exist before, it does now, so `File.Replace` can work correctly.
                File.Replace(newpath, path, createBackup ? prevpath : null);

                timer.Stop();
                L.debug($"{name}: complete in {timer.Elapsed.Humanize(16)}");
            } catch (Exception ex) {
                L.error($"{this}: error dumping database:\n{ex}\n");
            } finally {
                AlreadyScheduled.Remove(name);
            }
        }
    }

    internal static void DumpDatabase(string name, object? db)
        => new DumpDatabaseJob(name, db, false).ScheduleDump();

    internal static void DumpThreadSafeDatabase(string name, object? db)
        => new DumpDatabaseJob(name, db, true).ScheduleDump();


    [HarmonyPatch(typeof(Immigration), nameof(Immigration.OnPrefabInit))]
    [HarmonyPostfix]
    public static void DumpCarePackages() => DumpThreadSafeDatabase("CarePackages", Immigration.Instance.carePackages);

    [HarmonyPatch(typeof(Game), nameof(Game.OnSpawn))]
    [HarmonyPostfix]
    public static void DumpDatabasesOnGameStart() {
        DumpThreadSafeDatabase(
            "Assets.Sprites",
            Assets.instance.SpriteAssets.Select(
                static s => new { Name = s.name, Texture2D = s.texture?.name }
            )
        );
        DumpThreadSafeDatabase("DiscoveredResources", DiscoveredResources.Instance.DiscoveredCategories);
    }


    [HarmonyPatch(typeof(ComplexRecipeManager), nameof(ComplexRecipeManager.PostProcess))]
    [HarmonyPostfix]
    public static void DumpComplexRecipes(ComplexRecipeManager __instance)
        => DumpDatabase("ComplexRecipeManager.Recipes", __instance.recipes);


    // this is going to be called quite frequently, so be clever about it, eh?
    [HarmonyPatch(typeof(DiscoveredResources), nameof(DiscoveredResources.Discover), [typeof(Tag), typeof(Tag)])]
    [HarmonyPrefix]
    public static void DumpDiscoveredResourcesOnDiscovery(DiscoveredResources __instance, Tag tag) {
        if (__instance.IsDiscovered(tag))
            return;             // we do get called even when already discovered, so ignore those.

        DumpThreadSafeDatabase("DiscoveredResources", DiscoveredResources.Instance.DiscoveredCategories);
    }
}
