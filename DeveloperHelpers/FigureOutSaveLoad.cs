// using System.Text;

// using static SlippyCheeze.DeveloperHelpers.SaveLoadPrinting;


// namespace SlippyCheeze.DeveloperHelpers;


// internal static partial class SaveLoadPrinting {
//     internal static int depth = 0;
//     internal static StringBuilder sb = new(512);

//     internal static void enter(string text) {
//         Console.WriteLine(sb.Clear().Append("SAVE:> ").Append(' ', depth * 2).Append(text));
//         depth++;
//     }

//     internal static void exit(string text) {
//         depth--;
//         Console.WriteLine(sb.Clear().Append("SAVE:< ").Append(' ', depth * 2).Append(text));
//     }

//     internal static void exit() => depth--;
// }


// [HarmonyPatch(typeof(KSerialization.Deserializer))]
// internal static partial class Deserializer_DeserializeTypeless {
//     internal static Type target = typeof(ChoreGroupManager);
//     internal static bool active = false;

//     [HarmonyPatch("Deserialize", [typeof(object), typeof(IReader)])]
//     [HarmonyPrefix]
//     internal static void OnDeserialize2(object obj) {
//         if (!active)
//             return;
//         Console.WriteLine($"SAVE: Deserialize(obj={obj?.GetType().ToString() ?? "<null>"})");
//     }

//     [HarmonyPatch("DeserializeTypeless", [typeof(Type), typeof(object), typeof(IReader)])]
//     [HarmonyPrefix]
//     internal static void OnDeserializeTypeless3(Type type) {
//         if (!active)
//             return;
//         Console.WriteLine($"SAVE: DeserializeTypeless(type={type?.ToString() ?? "<null>"}, OBJ, READER)");
//     }

//     [HarmonyPatch("Deserialize", [typeof(Type), typeof(IReader), typeof(object)], [ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Out])]
//     [HarmonyPrefix]
//     internal static void OnDeserialize3(Type type) {
//         if (!active)
//             return;
//         Console.WriteLine($"SAVE: Deserialize(type={type?.ToString() ?? "<null>"}, READER, RESULT)");
//     }

//     [HarmonyPatch(typeof(KSerialization.Deserializer), "DeserializeTypeless", [typeof(object), typeof(IReader)])]
//     internal static partial class OnDeserializeTypeless2 {
//         internal static void Prefix(object obj) {
//             if (target == obj?.GetType()) {
//                 active = true;
//                 enter($"DeserializeTypeless(obj={obj?.GetType().ToString() ?? "<null>"}, READER)");
//             }
//         }

//         internal static void Postfix(object obj) {
//             if (active && target == obj?.GetType()) {
//                 active = false;
//                 exit($"DeserializeTypeless(obj={obj?.GetType().ToString() ?? "<null>"}, READER)");
//             }
//         }
//     }

//     [HarmonyPatch(typeof(KSerialization.DeserializationMapping), "Deserialize")]
//     internal static partial class OnDeserializationMapping_Deserialize {
//         internal static void Prefix(object obj) {
//             if (!active) return;
//             enter($"SAVE: DeserializationMapping.Deserialize(obj isa {obj.GetType()} == {obj})");
//         }

//         internal static void Postfix() { if (active) exit(); }
//     }

//     [HarmonyPatch(typeof(KSerialization.DeserializationMapping), "ReadValue")]
//     internal static partial class OnDeserializationMapping_ReadValue {
//         internal static void Prefix(KSerialization.TypeInfo type_info) {
//             if (!active) return;
//             enter($"SAVE: DeserializationMapping.Deserialize(type_info: type={type_info.type} info={type_info.info})");
//         }

//         internal static void Postfix() { if (active) exit(); }
//     }
// }

// [HarmonyPatch(typeof(SaveLoader), nameof(SaveLoader.LoadHeader))]
// internal static partial class SaveLoader_LoadHeader {
//     public static void Prefix(string filename) {
//         enter($"SaveLoader.LoadHeader({filename})");
//     }

//     public static void Postfix(SaveGame.Header header) {
//         exit($"SaveLoader.LoadHeader => .buildVersion {header.buildVersion} .headerVersion {header.headerVersion}");
//     }
// }

// [HarmonyPatch(typeof(KSerialization.DeserializationTemplate), MethodType.Constructor, new Type[]{ typeof(string), typeof(IReader) })]
// internal static partial class DeserializationTemplate_ctor {
//     public static void Prefix(string template_type_name) {
//         enter($"DeserializationTemplate({template_type_name})");
//     }

//     public static void Postfix(KSerialization.DeserializationTemplate __instance) {
//         exit($"DeserializationTemplate() onDeserializing={__instance.onDeserializing.ShortDescription()} onDeserialized={__instance.onDeserialized.ShortDescription()} customDeserialize={__instance.customDeserialize.ShortDescription()}");
//     }
// }
