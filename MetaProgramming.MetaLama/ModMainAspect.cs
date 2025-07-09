using System.Text.RegularExpressions;

using Metalama.Framework.Advising;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using Metalama.Framework.Eligibility;

using SlippyCheeze.MetaProgramming.Metalama;

namespace SlippyCheeze.MetaProgramming.MetaLama;

[CompileTime]
public class ModMainAspect: IAspect<INamedType> {
    public void BuildEligibility(IEligibilityBuilder<INamedType> builder) {}

    // This does a few things, but mostly data injection and compile-time reflection so that our
    // runtime code can simply use the data we pre-calculated instead of trolling through all the
    // types at runtime.
    //
    // This also lets us transform various problems from runtime to compile-time failures, avoiding
    // shipping code that I accidentally break by, eg, forgetting to flag a Harmony patch method as
    // static or, y'know, whatever.
    public void BuildAspect(IAspectBuilder<INamedType> builder) {
        // Debugger.Break();

        // Inject metadata about the mod into the target class.  also available:
        // "IsMod", "IsPacked", "MinimumSupportedBuild", "APIVersion"
        string[] ModInfo = ["ModName", "ModDescription"];
        foreach (string name in ModInfo) {
            if (!builder.Project.TryGetProperty(name, out var value)) {
                builder.Diagnostics.Report(CompileError.MSBuildPropertyMissing.WithArguments(name));
                return;
            }
            builder.IntroduceField(
                fieldName:  name,
                fieldType:  (INamedType) TypeFactory.GetType(typeof(string)),
                whenExists: OverrideStrategy.Fail,
                buildField: m => {
                    m.Accessibility         = Accessibility.Public;
                    m.Writeability          = Writeability.None;
                    m.InitializerExpression = ExpressionFactory.Literal(value);
                }
            );
        }

        // Find all the MODSTRINGS types in the project, and pop them into a nice array for the
        // runtime code to use when the mod is loaded.
        builder.IntroduceField(
            fieldName:  "AllModStringsRoots",
            fieldType:  typeof(Type[]),
            scope:      IntroductionScope.Static,
            whenExists: OverrideStrategy.Fail,
            buildField: m => {
                m.Accessibility         = Accessibility.Public;
                m.Writeability          = Writeability.ConstructorOnly;
                m.InitializerExpression = ToArrayOfTypes(
                    builder.Target.DeclaringAssembly.AllTypes
                    .Where(static type => type.Enhancements().HasAspect<ONITranslationExtensions>())
                    .SelectMany(static type => type.Types)
                    .Where(static type => type.TypeKind == TypeKind.Class && Regex.IsMatch(type.Name, @"^[A-Z]+$"))
                );
            }
        );

        {
            // Find all the HarmonyPatch annotated types, which includes `ModPatch` types, and stash
            // them away for later use during mod loading too.
            INamedType HarmonyPatch = (INamedType) TypeFactory.GetType("HarmonyLib.HarmonyPatch");
            INamedType ModPatch     = (INamedType) TypeFactory.GetType("SlippyCheeze.SupportCode.ModPatch");
            INamedType ModMain      = (INamedType) TypeFactory.GetType("SlippyCheeze.ModMain");

            // I already know ModMain is a HarmonyPatch, and that I want to apply it first, so may
            // as well sort that here. :)
            var patches = builder.Target.DeclaringAssembly.AllTypes
                .Where(type => type != ModMain && type.Attributes.OfAttributeType(HarmonyPatch).Any())
                .Prepend(ModMain);

            var modPatches = patches.Where(type => type.Attributes.OfAttributeType(ModPatch).Any());
            var harmonyPatches = patches.Except(modPatches);

            builder.IntroduceField(
                fieldName:  "ModPatches",
                fieldType:  typeof(Type[]),
                scope:      IntroductionScope.Static,
                whenExists: OverrideStrategy.Fail,
                buildField: m => {
                    m.Accessibility         = Accessibility.Public;
                    m.Writeability          = Writeability.ConstructorOnly;
                    m.InitializerExpression = ToArrayOfTypes(modPatches);
                }
            );

            builder.IntroduceField(
                fieldName:  "HarmonyPatches",
                fieldType:  typeof(Type[]),
                scope:      IntroductionScope.Static,
                whenExists: OverrideStrategy.Fail,
                buildField: m => {
                    m.Accessibility         = Accessibility.Public;
                    m.Writeability          = Writeability.ConstructorOnly;
                    m.InitializerExpression = ToArrayOfTypes(harmonyPatches);
                }
            );
        }

        {
            var UserMod2 = TypeFactory.GetType("KMod.UserMod2");
            var hooks = builder.Target.DeclaringAssembly.AllTypes
                .Where(type => ! type.IsSubclassOf(UserMod2))
                .SelectMany(type => type.Methods)
                .Where(method => method.Name == "OnModLoaded");

            // Generate the method that calls all the OnModLoaded hooks
            builder.IntroduceMethod(
                nameof(this.CallOnModLoadedHooks),
                whenExists: OverrideStrategy.Fail,
                args: new { hooks }
            );
        }

        {
            var ListOfMods = ((INamedType) TypeFactory.GetType(typeof(IReadOnlyList<>)))
                .WithTypeArguments(TypeFactory.GetType("KMod.Mod"));

            var UserMod2 = TypeFactory.GetType("KMod.UserMod2");

            var hooks = builder.Target.DeclaringAssembly.AllTypes
                .Where(type => {
                    bool result = ! type.IsSubclassOf(UserMod2);
                    return result;
                        })
                .SelectMany(type => type.Methods)
                .Where(method => method.Name == "OnAllModsLoaded");

            // Generate the method that calls all the OnModLoaded hooks
            builder.IntroduceMethod(
                nameof(this.CallOnAllModsLoadedHooks),
                whenExists: OverrideStrategy.Fail,
                args: new { hooks },
                buildMethod: m => {
                    m.Parameters["mods"].Type = ListOfMods;
                }
            );
        }
    }

    // CallOnModLoadedHooks: generate code to call each hook sequentially, with appropriate logging
    // and all that.
    [Template]
    private void CallOnModLoadedHooks(IEnumerable<IMethod> hooks) {
        foreach (var hook in hooks) {
            this.RuntimeLog($"Calling {hook.DeclaringType.Name}.{hook.Name}");
            hook.Invoke();
        }
    }

    [Template]
    private void CallOnAllModsLoadedHooks(
        [RunTime]     dynamic mods,
        [CompileTime] IEnumerable<IMethod> hooks
    ) {
        foreach (var hook in hooks) {
            this.RuntimeLog($"Calling {hook.DeclaringType.Name}.{hook.Name}");
            hook.Invoke(mods);
        }
    }

    [Template]
    private void RuntimeLog([CompileTime] string msg) {
        // to get the compiler to inject the file and line parameters we need to invoke without
        // passing them, and sadly the IMethod.Invoke() method doesn't really help, since it insists
        // on the optional params being supplied.
        meta.InsertStatement($"""L.log("{msg}");""");
    }

    private IExpression ToArrayOfTypes(IEnumerable<INamedType> itypes) {
        ExpressionBuilder init = new();
        init.AppendVerbatim("[\n");

        foreach (var itype in itypes) {
            init.AppendVerbatim("    ");  // YOLO, I guess. :)
            init.AppendExpression(itype.ToTypeOfExpression());
            init.AppendVerbatim(",\n");
        }
        init.AppendVerbatim("\n]");

        return init.ToExpression();
    }
}
