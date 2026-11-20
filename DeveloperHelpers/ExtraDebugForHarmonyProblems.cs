using Newtonsoft.Json;

namespace SlippyCheeze.DeveloperHelpers;

[HarmonyPatch]
public static partial class DebugTypeGetMethodFailures {
    public static IEnumerable<MethodBase> TargetMethods() {
        foreach (var method in typeof(Type).GetMethods())
            if (method.Name == "GetMethod")
                yield return method;
    }
    public static void Finalizer(Exception __exception, Type __instance, object[] __args) {
        if (__exception is null)
            return;

        Console.WriteLine($"EXCEPTION on {__instance} with Type.GetMethod({JsonConvert.SerializeObject(__args)}) => {__exception.GetType()}(\"{__exception.Message}\")");
    }
}
