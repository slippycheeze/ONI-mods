using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;

namespace SlippyCheeze.MetaProgramming.MetaLama;

public class MemoizeFabric: ProjectFabric {
    public override void AmendProject(IProjectAmender project) => AddMemoizeAspect(project);

    public static void AddMemoizeAspect(IProjectAmender project) {
        INamedType MemoizeAttribute = MetaTypes.RequireType("SlippyCheeze.MetaProgramming.MemoizeAttribute");
        project.SelectTypes()
            .SelectMany(t => t.Methods)
            .Where(m => m.Attributes.Any(MemoizeAttribute))
            .RequireAspect<MemoizeAspect>();

        project.SelectTypes()
            .SelectMany(t => t.FieldsAndProperties)
            .Where(p => p.Attributes.Any(MemoizeAttribute))
            .RequireAspect<MemoizeAspect>();
    }
}

public class MemoizeTransitiveFabric: TransitiveProjectFabric {
    public override void AmendProject(IProjectAmender project) => MemoizeFabric.AddMemoizeAspect(project);
}
