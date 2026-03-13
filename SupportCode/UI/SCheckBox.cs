using PeterHan.PLib.UI;

namespace SlippyCheeze.SupportCode;

// A wrapper using PCheckBox to define a UI control, and also provide a nicer runtime interface to
// managing it.  Captures the code I seem to end up writing every time I do this.
public partial class SCheckBox {
    public SCheckBox(
        // required stuff: name, and the delegate handling meaning, not just UI state
        string            name,
        string            text,
        string            tooltip,
        Action<SCheckBox, bool> OnClick,

        // optional, but probably the most frequent choices, I'd guess. :)
        bool InitialState = false,

        // optional configuration, passed to the PCheckBox
        bool DynamicSize            = false,
        Vector2? FlexSize           = null,
        RectOffset? Margin          = null,
        TextAnchor TextAlignment    = TextAnchor.MiddleLeft,
        TextStyleSetting? TextStyle = null
    ) {
        this.name    = name;
        this.OnClick = OnClick;

        this.pCheckBox = new PCheckBox(name) {
            Text          = text,
            ToolTip       = tooltip,
            // pass through various initial properties.
            DynamicSize   = DynamicSize,
            FlexSize      = FlexSize ?? Vector2.right,
            Margin        = Margin ?? new(0, 4, 0, 4),  // sensible default, based on Klei UI.
            TextAlignment = TextAlignment,
            TextStyle     = TextStyle ?? PUITuning.Fonts.TextDarkStyle,
            InitialState  = InitialState ? PCheckBox.STATE_CHECKED : PCheckBox.STATE_UNCHECKED,
            // and add our own parts too.
            OnChecked     = OnCheckedInternal,
        }
            .AddOnRealize(OnRealize);
    }

    public readonly string name;
    public readonly Action<SCheckBox, bool> OnClick;

    // track the various GameObject and component that represent our state.  in all cases the
    // invariant is enforced by the time we actually support interacting with the component.
    private GameObject gameObject = null!;
    private LocText    locText    = null!;
    private ToolTip    toolTip    = null!;

    // We "are" a PCheckBox for many practical purposes.
    private PCheckBox? pCheckBox = null;

    public static implicit operator PCheckBox(SCheckBox cb)
        => cb.pCheckBox.ThrowIfNull("converted to PCheckBox after realized");


    // Wrappers around state management, so the caller doesn't need write them every time.
    public bool IsChecked {
        get => PCheckBox.GetCheckState(gameObject) == PCheckBox.STATE_CHECKED;
        set => PCheckBox.SetCheckState(gameObject, value ? PCheckBox.STATE_CHECKED : PCheckBox.STATE_UNCHECKED);
    }

    public string Text {
        get => locText.text;
        set => locText.text = value;
    }

    public string ToolTip {
        get => toolTip.multiStringToolTips[0];
        set => toolTip.toolTip = value;
    }


    // Some helpers for the PLibUI "create me" functions.
    //
    // 2026-03-10 REVISIT: many of these will end up in a common base class of all my personal
    // UI components wrapping PLibUI components.  but for now, this.
    public SCheckBox AddTo(Transform parent,  int index = -2) => this.AddTo(parent.gameObject, index);
    public SCheckBox AddTo(GameObject parent, int index = -2) {
        ((PCheckBox)this).AddTo(parent, index);
        return this;
    }


    // Some helpers for Unity functions.
    public SCheckBox SetActive(bool active) {
        gameObject.SetActive(active);
        return this;
    }



    // When the object in created save away the GameObject and other parts for future reference, and
    // clear out the PCheckBox; we only support being built once, so this is it.
    private void OnRealize(GameObject go) {
        pCheckBox = null;
        gameObject = go;
        locText    = go.GetComponentInChildren<LocText>();
        toolTip    = go.GetComponentInChildren<ToolTip>();
    }

    // Handle the user clicking the control, in which we change state first, and then report it to
    // our delegate.  Fun times.
    private void OnCheckedInternal(GameObject go, int rawState) {
        if (this.gameObject != go) {
            L.error($"{this} OnChecked callback for {go.Humanize()}, which is not my GameObject, {gameObject.Humanize()}!");
            return;
        }
        bool newState = !(rawState == PCheckBox.STATE_CHECKED);
        IsChecked = newState;
        // WARNING: the user can call any method on this object during this callback.
        OnClick(this, newState);  // pass the new value, since they doubtless want to know.
    }
}
