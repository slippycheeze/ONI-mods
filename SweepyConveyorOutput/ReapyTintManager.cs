namespace SlippyCheeze.SweepyConveyorOutput;

public partial class ReapyTintManager: KMonoBehaviour {
    [MyCmpGet]
    private KBatchedAnimController anim = null!;

    private static Color32 Tint = new(r: 50, g: 250, b: 50, a: 255);

    public override void OnSpawn()          => anim.OnTintChanged += OnTintChanged;
    public override void OnForcedCleanUp()  => anim.OnTintChanged -= OnTintChanged;

    private static Color white = Color.white;  // allocates new every time

    internal void OnTintChanged(Color value) {
        if (value == white)     // this is what we "reset" to when exiting view mode.
            anim.TintColour = Tint;
    }
}
