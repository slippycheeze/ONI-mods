namespace SlippyCheeze.MetaProgramming;

// This is only valid on class called `MODSTRINGS`, and will throw an error if applied to anything
// else in the project.  In fact, it shouldn't be applied manually: let the MetaLama Fabric apply it
// to the class automatically to trigger the rewriting.
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class ONITranslationsAttribute(): Attribute;
