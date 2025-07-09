using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;

using SlippyCheeze.MetaProgramming.Metalama;

namespace SlippyCheeze.MetaProgramming.MetaLama;

// this will be called to modify compilation of any project that *references* this assembly, but
// won't look at itself.  so, specifically, it automates metalama injections into the mods that
// consume it at compile time.
public class ModFabric: TransitiveProjectFabric {
    public override void AmendProject(IProjectAmender project) {
        // auto-generate static reflection data for the project, attached to the KMod.UserMod2
        // derivative class.  which should always be the SupportCode ModMain.cs, but plan for the
        // future, I guess?
        var UserMod2 = TypeFactory.GetType("KMod.UserMod2");
        project.SelectTypesDerivedFrom(UserMod2).RequireAspect<ModMainAspect>();

        // automatic application of OnModLoadedHook attribute to any `OnModLoaded` method.
        var possibleHookMethods = project.SelectTypes()
            // exclude anything derived from UserMod2 :)
            .Where(type => ! type.IsSubclassOf(UserMod2))
            .SelectMany(type => type.Methods);

        possibleHookMethods
            .Where(method => method.Name == "OnModLoaded")
            .RequireAspect<OnModLoadedHookAspect>();

        possibleHookMethods
            .Where(method => method.Name == "OnAllModsLoaded")
            .RequireAspect<OnAllModsLoadedHookAspect>();

        // auto-generate some type-safe helpers for translation string keys and prefixes.
        project
            .SelectTypes()
            .Where(type => type.Name == "MODSTRINGS")
            .RequireAspect<ONITranslationExtensions>();
    }
}
