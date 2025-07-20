namespace SlippyCheeze.SupportCode;

// make ModMain visible, even if it lives "above" this project.
public static partial class SupportCode {
    public static void Initialize(IModMain Modmain) => SupportCode.ModMain = ModMain;
    public static IModMain ModMain { get; private set; } = null!;
}
