using Newtonsoft.Json;

namespace SlippyCheeze.DeveloperHelpers;

[HarmonyPatch]
internal static partial class DumpGameDatabases {
    private static JsonSerializerSettings serializerSettings = new() { Formatting = Formatting.Indented };
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

    public static void DumpDatabaseInternal(string name, object db) {
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
    }

    private static void DumpDatabase(object db, [CallerArgumentExpression(nameof(db))] string? name = null)
        => GameScheduler.Instance.ScheduleNextFrame(
            nameof(DumpDatabase),
            (_) => DumpDatabaseInternal(
                name ?? throw new ArgumentNullException($"{db.GetType().FullName} null CallerArgumentExpression"),
                db
            )
        );



    [HarmonyPatch(typeof(Immigration), nameof(Immigration.OnPrefabInit))]
    [HarmonyPostfix]
    public static void DumpCarePackages() => DumpDatabase(Immigration.Instance.carePackages);

    [HarmonyPatch(typeof(Game), nameof(Game.OnSpawn))]
    [HarmonyPostfix]
    public static void DumpKnownSprites() => DumpDatabase(
        Assets.instance.SpriteAssets.Select(
            static s => new { Name = s.name, Texture2D = s.texture?.name }
        ),
        "Assets.Sprites"
    );
}
