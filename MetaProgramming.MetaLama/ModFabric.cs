using System.Diagnostics;

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;

namespace SlippyCheeze.MetaProgramming.MetaLama;

// this will be called to modify compilation of any project that *references* this assembly, but
// won't look at itself.  so, specifically, it automates metalama injections into the mods that
// consume it at compile time.
public class ModFabric: TransitiveProjectFabric {
    public override void AmendProject(IProjectAmender project) {
        Debugger.Break();

        // 2025-07-06: verified to correctly find the target type in TestMod.
        project
            .SelectDeclarationsWithAttribute(typeof(ONITranslationsAttribute))
            .AddAspect<ONITranslationsImplementation>();
    }
}
