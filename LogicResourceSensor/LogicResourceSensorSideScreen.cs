using PeterHan.PLib.UI;

using UnityEngine.UI;

using static SlippyCheeze.LogicResourceSensor.LogicResourceSensor;

namespace SlippyCheeze.LogicResourceSensor;

[HarmonyPatch]
public class LogicResourceSensorSideScreen: SideScreenContent {
    // before ThresholdSwitchSideScreen, after TreeFilterableSideScreen.
    //
    // NOTE: this assumes my other patch sets TreeFilterableSideScreen SortOrder to 10, otherwise
    // all my UI will show above it.  Which is acceptable enough, even if not what I desire.
    public const int sideScreenSortOrder = 5;  // make accessible to the other SideScreenContent classes
    public override int GetSideScreenSortOrder() => sideScreenSortOrder;


    public override bool IsValidForTarget(GameObject target) {
        // L.debug($"IsValidForTarget(target={target.Humanize()})");
        return target.TryGetComponent<LogicResourceSensor>(out var _);
    }


    public override void SetTarget(GameObject go) {
        // L.debug($"SetTarget(target={go.Humanize()})");
        target = go.GetComponent<LogicResourceSensor>().ThrowIfNull("unexpectedly missing LogicResourceSensor");
    }

    public override void ClearTarget() => target = null;

    // GameHashes.UIRefreshData ????

    // the LogicResourceSensor we are currently targetting, if any.  setting this automatically
    // updates our subscription to the event it sends telling us when the mode setting changes.
    //
    // when that happens we will trigger various things, including a refresh of the currently
    // displayed SideScreen system,
    private LogicResourceSensor? target {
        get;
        set {
            checkboxLabelsDirty = true;

            if (field != null) {  // unsubscribe from previous target, if any.
                field.OnModeChanged -= OnSensorModeChanged;
                field.OnIncludeStorageChanged  -= OnIncludeBooleanChanged;
                field.OnIncludeReservedChanged -= OnIncludeBooleanChanged;
            }

            field = value;

            if (field != null) {  // subscribe to new target, if we have one
                field.OnModeChanged += OnSensorModeChanged;
                field.OnIncludeStorageChanged  += OnIncludeBooleanChanged;
                field.OnIncludeReservedChanged += OnIncludeBooleanChanged;

                UpdateStateFromTarget(field);
            }
        }
    }

    // Called from our LogicResourceSensor when target.Mode *actually* changes; it isn't called if
    // we try and set the mode to the same value it currently is.
    internal void OnSensorModeChanged(LogicResourceSensor sender, SensorMode mode) {
        // Right now I don't call this unless the mode changes in response to user action *while* we
        // are being displayed, so it indicates a genuine change in the state of our subsystem.
        //
        // Since I'm counting on the Klei DetailsScreen to handle "which mode-based options are
        // shown", I need to trigger a refresh of it now to update what is displayed.
        //
        // Uses of this method in the Klei code are on assorted GameObject instances for things™ in
        // the game like rockets and the WarpPortal, but the pattern is:
        //
        // public void RefreshSideScreen() {
        //     if (!this.GetComponent<KSelectable>().IsSelected)
        //         return;
        //     DetailsScreen.Instance.Refresh(this.gameObject);
        // }

        // this is my equivalent: verify the change *actually* came from this.target, rather than
        // just assuming it, since this.target is only set when, y'know, that GameObject is selected.
        if (sender != target) {
            L.error($"sender {sender.Humanize()} != target {target.Humanize()}, missed unsubscribe?");
            sender.OnModeChanged -= OnSensorModeChanged;
            return;
        }

        // L.debug($"before DetailsScreen.Instance.Refresh(target.gameObject)");
        DetailsScreen.Instance.Refresh(target.gameObject);
        // L.debug($"after  DetailsScreen.Instance.Refresh(target.gameObject)");
    }


    // the toggle button UI elements we use to determine mode.
    private KToggle? distanceModeToggle {
        get;
        set {
            if (field != null)
                field.onClick -= onDistanceModeToggleClicked;
            field = value;
            if (field != null)
                field.onClick += onDistanceModeToggleClicked;
        }
    }
    private void onDistanceModeToggleClicked() => OnToggleClicked(distanceModeToggle, SensorMode.Distance);

    private KToggle? roomModeToggle {
        get;
        set {
            if (field != null)
                field.onClick -= onRoomModeToggleClicked;
            field = value;
            if (field != null)
                field.onClick += onRoomModeToggleClicked;
        }
    }
    private void onRoomModeToggleClicked() => OnToggleClicked(roomModeToggle, SensorMode.Room);

    private KToggle? globalModeToggle {
        get;
        set {
            if (field != null)
                field.onClick -= onGlobalModeToggleClicked;
            field = value;
            if (field != null)
                field.onClick += onGlobalModeToggleClicked;
        }
    }
    private void onGlobalModeToggleClicked() => OnToggleClicked(globalModeToggle, SensorMode.Global);


    // hold the Klei component that manages the "include X" option for the current mode.  the value
    // of X is defined during the UpdateStateFromTarget() method.
    private MultiToggle? includeCheckbox {
        get;
        set {
            if (field != null) field.onClick -= OnIncludeCheckboxClicked;

            field = value;

            if (field != null) field.onClick += OnIncludeCheckboxClicked;
        }
    }


    private bool updateStateFromTargetBeforeRealize = false;
    private void UpdateStateFromTarget(LogicResourceSensor target) {
        // The very first time we are used we receive a SetTarget() call *before* OnPrefabInit(),
        // which means our UI components have not been realized by the PLibUI code yet.
        //
        // Record that fact for OnPrefabInit() to notice later, and ignore the update request.
        if (distanceModeToggle == null || roomModeToggle == null || globalModeToggle == null || includeCheckbox == null) {
            updateStateFromTargetBeforeRealize = true;
            return;
        }
        UpdateToggleState(distanceModeToggle, target.Mode == SensorMode.Distance);
        UpdateToggleState(roomModeToggle,     target.Mode == SensorMode.Room);
        UpdateToggleState(globalModeToggle,   target.Mode == SensorMode.Global);
        UpdateCheckboxState();
    }


    private void UpdateToggleState(KToggle toggle, bool shouldBeOn) {
        if (toggle.isOn == shouldBeOn) return;  // filter out unnecessary changes because
        toggle.isOn = shouldBeOn;               // setting this *always* causes visual effects
    }

    public void OnToggleClicked(KToggle? toggle, SensorMode mode) {
        if (toggle == null)
            throw new InvalidOperationException($"somehow got clicked from a null reference!");

        // Without a target we can't do anything, and the state will be overwritten later anyhow.
        if (target == null) {
            L.error($"{toggle.Humanize()} for mode={mode} clicked when target is null");
            return;
        }

        // this may or may not change the state in the LogicResourceSensor; if it did change then
        // it'll trigger a refresh of this SideScreen.  if not, nothing needs change here: the
        // correct SensorMode was already selected.
        // L.debug($"given {target.Humanize()}, setting target.Mode = {mode}");
        target.Mode = mode;
        // L.debug($"finished setting target.Mode = {mode}");
    }


    // updates pushed from the target to us.
    private void OnIncludeBooleanChanged(LogicResourceSensor sender, bool state) {
        if (sender != target) {
            L.error($"invoked when sender != target: {sender.Humanize()} {target.Humanize()}, unsubscribing and ignoring");
            sender.OnIncludeStorageChanged  -= OnIncludeBooleanChanged;
            sender.OnIncludeReservedChanged -= OnIncludeBooleanChanged;
            return;
        }
        UpdateCheckboxState();
    }


    private void OnIncludeCheckboxClicked() {
        if (target == null) return;  // can't cope, off to mordor.

        // push a change to the sensor based on the current mode.
        if (target.Mode == SensorMode.Global)
            target.IncludeReserved = ! target.IncludeReserved;
        else
            target.IncludeStorage = ! target.IncludeStorage;
        // and if that actually changed state it'll push an update back to us.
    }

    private bool checkboxLabelsDirty = true;
    private void UpdateCheckboxState() {
        // L.debug($"{includeCheckbox.Humanize()} checkboxLabelsDirty={checkboxLabelsDirty} {target.Humanize()} {includeCheckbox.Humanize()}");
        if (target == null || includeCheckbox == null) return;  // can't do anything with this.

        if (checkboxLabelsDirty) UpdateCheckboxLabels();

        bool shouldBeChecked = target.Mode == SensorMode.Global ? target.IncludeReserved : target.IncludeStorage;
        // MultiToggle does sensible filtering of same-value assignments.
        includeCheckbox.ChangeState(shouldBeChecked ? PCheckBox.STATE_CHECKED : PCheckBox.STATE_UNCHECKED);
    }

    private void UpdateCheckboxLabels() {
        // L.debug($"{includeCheckbox.Humanize()}");
        if (target == null || includeCheckbox == null) return;  // nothing we can do.

        // Text    => LocText component on child GameObject with name="Text"
        // ToolTip => ToolTip component on root

        includeCheckbox.GetComponentInChildren<LocText>().text = target.Mode switch {
            SensorMode.Global => MODSTRINGS.UI.UISIDESCREENS.RESOURCE_SENSOR_SIDESCREEN.OPTIONS.INCLUDERESERVED.TITLE,
            _                 => MODSTRINGS.UI.UISIDESCREENS.RESOURCE_SENSOR_SIDESCREEN.OPTIONS.INCLUDESTORAGE.TITLE
        };

        includeCheckbox.GetComponentInChildren<ToolTip>().toolTip = target.Mode switch {
            SensorMode.Global => MODSTRINGS.UI.UISIDESCREENS.RESOURCE_SENSOR_SIDESCREEN.OPTIONS.INCLUDERESERVED.TOOLTIP,
            _                 => MODSTRINGS.UI.UISIDESCREENS.RESOURCE_SENSOR_SIDESCREEN.OPTIONS.INCLUDESTORAGE.TOOLTIP
        };

        checkboxLabelsDirty = false;  // at least until your target changes.
    }



    public override void OnPrefabInit() {
        // L.debug($"OnPrefabInit() was called");

        this.titleKey = MODSTRINGS.UI.UISIDESCREENS.RESOURCE_SENSOR_SIDESCREEN.TITLE_key;

        // Adjust the top level layout component PLib added for us.
        var root = new PPanel("ResourceSensorSideScreen") {
            Alignment = TextAnchor.MiddleCenter,
            Direction = PanelDirection.Vertical,
            Margin    = new(left: 13, right: 13, top: 6, bottom: 6),
            Spacing   = 8,
            FlexSize  = Vector2.right,
        };

        // Scan for resources based on:
        // <DISTANCE> <ROOM> <GLOBAL>
        var instructions = new PLabel("Instructions") {
            FlexSize = Vector2.right,
            Text = MODSTRINGS.UI.UISIDESCREENS.RESOURCE_SENSOR_SIDESCREEN.MODE.INSTRUCTIONS,
            TextAlignment = TextAnchor.MiddleCenter,
            TextStyle = PUITuning.Fonts.TextDarkStyle,
        };
        root.AddChild(instructions);

        var buttonGrid = new PPanel("ButtonGrid") {
            // Margin    = new(left: 6, right: 6, top: 0, bottom: 0),
            FlexSize  = Vector2.right,
            Direction = PanelDirection.Horizontal,
            Alignment = TextAnchor.UpperCenter,
            Spacing   = 2,
        };
        root.AddChild(buttonGrid);

        // When you come back here and think, Ah!  A group of toggles!  I'll use a ToggleGroup,
        // please remember that KToggle completely ignores the parts of UnityEngine.UI.Toggle that
        // make that work.
        //
        // You need to reimplement it, calling the *Klei* layer, to make it work. :(
        buttonGrid.AddChild(
            SensorModeToggleButton(
                SensorMode.Distance,
                MODSTRINGS.UI.UISIDESCREENS.RESOURCE_SENSOR_SIDESCREEN.MODE.DISTANCE.TITLE,
                MODSTRINGS.UI.UISIDESCREENS.RESOURCE_SENSOR_SIDESCREEN.MODE.DISTANCE.TOOLTIP,
                (go) => this.distanceModeToggle = go.GetComponent<KToggle>()
            )
        );

        buttonGrid.AddChild(
            SensorModeToggleButton(
                SensorMode.Room,
                MODSTRINGS.UI.UISIDESCREENS.RESOURCE_SENSOR_SIDESCREEN.MODE.ROOM.TITLE,
                MODSTRINGS.UI.UISIDESCREENS.RESOURCE_SENSOR_SIDESCREEN.MODE.ROOM.TOOLTIP,
                (go) => this.roomModeToggle = go.GetComponent<KToggle>()
            )
        );

        buttonGrid.AddChild(
            SensorModeToggleButton(
                SensorMode.Global,
                MODSTRINGS.UI.UISIDESCREENS.RESOURCE_SENSOR_SIDESCREEN.MODE.GLOBAL.TITLE,
                MODSTRINGS.UI.UISIDESCREENS.RESOURCE_SENSOR_SIDESCREEN.MODE.GLOBAL.TOOLTIP,
                (go) => this.globalModeToggle = go.GetComponent<KToggle>()
            )
        );


        // turns out that all the modes have a single checkbox worth of "include X", so I'll just
        // manage that from here, the one place that needs it.
        var includeCheckbox = new PCheckBox("IncludeCheckbox") {
            DynamicSize   = true,
            FlexSize      = Vector2.right,  // fill full width, which should result in left-aligned conent...
            Text          = "PLACEHOLDER",
            ToolTip       = "PLACEHOLDER",
            TextStyle     = PUITuning.Fonts.TextDarkStyle,
            TextAlignment = TextAnchor.MiddleLeft,
            InitialState  = PCheckBox.STATE_UNCHECKED,
        }
            .AddOnRealize(go => this.includeCheckbox = go.GetComponent<MultiToggle>().ThrowIfNull());

        root.AddChild(includeCheckbox);

        // finally, this builds the UI and attaches it to the GameObject, which we then configure as
        // the "UI" element for the Klei SideScreen system to interact with.
        root.AddTo(gameObject);
        ContentContainer = gameObject;

        // so long delayed because I /should/ have had all the above from my Unity Prefab asset, but
        // I don't have one of them, so ... here we are. :)
        base.OnPrefabInit();

        // Apparently the first time we are constructed the call to `SetTarget` happens *before* the
        // OnPrefabInit function is called?  Which is wierd?  Anyway, trigger an update to fix that.
        if (updateStateFromTargetBeforeRealize) {
            if (target != null) {
                // L.warn($"UpdateStateFromTarget() happened before OnPrefabInit(), will replay the call because target is not null");
                updateStateFromTargetBeforeRealize = false;  // reset this so we catch if it happens *again*
                UpdateStateFromTarget(target);
            } else {
                // L.warn($"UpdateStateFromTarget() happened before OnPrefabInit(), will *NOT* replay the call: target == null");
            }
        }
    }

    public override void OnForcedCleanUp() {
        // set these to null so they unsubscribe from events on the target.
        target = null;

        // help, can't unsubscribe from a lambda?
    }

    private PToggleButton SensorModeToggleButton(
        SensorMode mode,
        string Text,
        string ToolTip,
        Action<GameObject> callback
    ) {
        string name = $"{mode}ModeToggle";
        PToggleButton toggle = new(name) {
            FlexSize       = Vector2.right,
            // Margin         = new(left: 10, right: 10, top: 8, bottom: 8),
            Text           = Text,
            ToolTip        = ToolTip,
            Color          = PUITuning.Colors.ButtonBlueStyle,
            TextStyle      = PUITuning.Fonts.TextLightStyle,
            TextAlignment  = TextAnchor.MiddleCenter,
        };
        toggle.AddOnRealize(go => {
            // L.debug($"OnRealize for {name} invoked with {go.Humanize()}");
            go.SetMinUISize(new(64, 24));
            callback(go);
        });

        return toggle;
    }


    [HarmonyPatch(typeof(DetailsScreen), nameof(DetailsScreen.OnPrefabInit))]
    [HarmonyPostfix]
    public static void RegisterSideScreen() {
        L.info($"registering {nameof(LogicResourceSensorSideScreen)}");
        PUIUtils.AddSideScreenContent<LogicResourceSensorSideScreen>();
    }
}


[HarmonyPatch(typeof(DetailsScreen), nameof(DetailsScreen.OnPrefabInit))]
public class DistanceSliderSideScreen: SideScreenContent {
    private LogicResourceSensor? target = null;

    // these are all 100 percent sure to be set before use, or your game explodes (deliberatly)!
    private KSlider valueSlider            = null!;
    private KNumberInputField numberInput  = null!;
    private RectTransform numberInputRect  = null!;
    private LocText unitsLabel             = null!;
    private LocText minLabel               = null!;
    private LocText maxLabel               = null!;

    public override void OnPrefabInit() {
        // L.debug($"{this.Humanize()} called");
        ContentContainer = this.gameObject;

        // wire up the controls that Klei created for us in the prefab. ;)
        valueSlider = GetComponentInChildren<KSlider>();
        valueSlider.handleRect.GetComponent<ToolTip>().OnToolTip = GetToolTip;
        valueSlider.minValue = LogicResourceSensor.MinDistance;
        valueSlider.maxValue = LogicResourceSensor.MaxDistance;
        valueSlider.wholeNumbers = true;

        valueSlider.onReleaseHandle += ReceiveValueFromSlider;
        valueSlider.onPointerDown   += ReceiveValueFromSlider;
        valueSlider.onDrag          += ReceiveValueFromSlider;
        valueSlider.onMove          += ReceiveValueFromSlider;


        numberInput = GetComponentInChildren<KNumberInputField>().ThrowIfNull();
        numberInputRect = numberInput.GetComponent<RectTransform>().ThrowIfNull();

        numberInput.minValue = LogicResourceSensor.MinDistance;
        numberInput.maxValue = LogicResourceSensor.MaxDistance;
        numberInput.decimalPlaces = 0;

        numberInput.onEndEdit += ReceiveValueFromInput;

        unitsLabel = transform.Find("Contents/UnitsLabel").GetComponent<LocText>().ThrowIfNull();

        minLabel = transform.Find("Contents/MinLabel").GetComponent<LocText>().ThrowIfNull();
        minLabel.text = Units(LogicResourceSensor.MinDistance);

        maxLabel = transform.Find("Contents/MaxLabel").GetComponent<LocText>().ThrowIfNull();
        maxLabel.text = Units(LogicResourceSensor.MaxDistance);

        // vertically center the UnitsLabel and NumberInputField, rather than aligning them to baseline.
        void AdjustLayout() {
            // adjust the top-level containers.
            var input = numberInput.GetComponent<RectTransform>();
            var units = unitsLabel.GetComponent<RectTransform>();
            float height = Mathf.Max(input.rect.height, units.rect.height);

            if (input.rect.height < height) {
                float delta = (input.rect.height - height) / 2f;
                // L.debug($"input.anchoredPosition={input.anchoredPosition}, delta={delta}");
                Vector2 pos = input.anchoredPosition;
                input.anchoredPosition = pos with { y = pos.y - delta };
            }
            if (units.rect.height < height) {
                float delta = (units.rect.height - height) / 2f;
                // L.debug($"units.anchoredPosition={units.anchoredPosition}, delta={delta}");
                Vector2 pos = units.anchoredPosition;
                units.anchoredPosition = pos with { y = pos.y - delta };
            }

            // adjust the content of numberInput, which has one child, "Text Area", itself
            // containing three children — and those children down there are the ones we want to
            // center vertically in their total space.
            var textArea = numberInput.transform.GetChild(0);
            List<RectTransform> children = [];
            for (int n = 0; n < textArea.childCount; n++)
                children.Add((RectTransform)textArea.GetChild(n));
            height = children.Max(child => child.rect.height);
            foreach (var child in children) {
                float delta = child.rect.height - height;
                // L.debug($"{child.Humanize()}: delta={delta} child.height={child.rect.height} maxHeight={height}");
                if (delta == 0)  // not the smallest
                    continue;
                var pos = child.anchoredPosition;
                child.anchoredPosition = pos with { y = pos.y - delta };
            }
        }
        AdjustLayout();


        // gameObject.DebugObjectTree();

        base.OnPrefabInit();

        if (target != null)
            SetTarget(target.gameObject);  // replace the exact call we want to replicate.
    }

    // just after the other controls. :)
    public override int GetSideScreenSortOrder() => LogicResourceSensorSideScreen.sideScreenSortOrder - 1;

    public override bool IsValidForTarget(GameObject target)
        => target.GetComponent<LogicResourceSensor>()?.Mode == SensorMode.Distance;

    public override void SetTarget(GameObject go) {
        // we get called the very first time *before* OnPrefabInit is called, in which case we stash
        // away the target for later, and OnPrefabInit will call `UpdateFromTarget()` for us.
        target = go.GetComponent<LogicResourceSensor>().ThrowIfNull("unexpectedly missing LogicResourceSensor");
        if (valueSlider == null)
            return;

        // WARNING: only subscribe *after* OnPrefabInit, or the first pass we end up subscribed
        // twice to the LogicResourceSensor, and that sounds ... undesirable.
        target.OnDistanceChanged += UpdateFromTarget;

        // only done when SetTarget is called on SliderSet, which means it is only done once, so
        // replicate that here.
        numberInput.Activate();

        UpdateFromTarget(target, target.Distance);
    }

    public override void ClearTarget() {
        target?.OnDistanceChanged -= UpdateFromTarget;
        target = null;
    }

    public void UpdateFromTarget(LogicResourceSensor sender, int distance) {
        // SetTarget is called before OnPrefabInit the very first time it happens, and it hooks us
        // up to change notifications from the target LogicResourceSensor.  Which means that
        // UpdateFromTarget might be triggered before OnPrefabInit as well; in that case we ignore
        // it, and OnPrefabInit will call us again once everything is wired up.
        if (valueSlider == null)
            return;

        // safety check
        if (sender != target) {
            L.error($"{this.Humanize()} missed unsubscribe from {sender.Humanize()}");
            sender.OnDistanceChanged -= UpdateFromTarget;
            return;
        }

        valueSlider.value = distance;

        unitsLabel.text = Units(distance, includeNumber: false);

        // resize the numberInput to reflect the limits above.
        numberInput.field.characterLimit = Mathf.FloorToInt(1f + Mathf.Log10(LogicResourceSensor.MaxDistance));

        // can't modify a member of sizeDelta, gotta replace the whole thing.
        numberInputRect.sizeDelta = numberInputRect.sizeDelta
            // extra +5 at the end because it feels a touch cramped right now.
            with { x = ((numberInput.field.characterLimit + 1) * 10) + 5 };

        numberInput.SetDisplayValue(distance.ToString());
    }

    private string Units(int number, bool includeNumber = true) {
        return ((string)MODSTRINGS.UI.UISIDESCREENS.RESOURCE_SENSOR_SIDESCREEN.DISTANCE.UNITS)
            .ToQuantity(number, includeNumber ? ShowQuantityAs.Numeric : ShowQuantityAs.None);
    }

    private string GetToolTip() {
        if (target is null)
            return "ERROR: target is null";
        return String.Format(
            MODSTRINGS.UI.UISIDESCREENS.RESOURCE_SENSOR_SIDESCREEN.DISTANCE.TOOLTIP,
            target.Distance,
            Units(target.Distance, includeNumber: false)
        );
    }

    private void ReceiveValueFromSlider() => target?.Distance = (int)this.valueSlider.value;
    private void ReceiveValueFromInput()  => target?.Distance = (int)this.numberInput.currentValue;


    [HarmonyPostfix]
    public static void RegisterSideScreen() {
        L.info($"registering {nameof(DistanceSliderSideScreen)}");

        // clone the original SideScreenRef content, and put it in our new SideScreen.
        DetailsScreen.SideScreenRef original = DetailsScreen.Instance.sideScreens
            .Find(s => s.screenPrefab is IntSliderSideScreen);

        GameObject parent = DetailsScreen.Instance.sidescreenTabs
            .First(tab => tab.type == DetailsScreen.SidescreenTabTypes.Config)
            .bodyInstance;

        GameObject screen = Util.KInstantiateUI<SideScreenContent>(original.screenPrefab.gameObject, parent).gameObject;

        // L.debug($"screen.IsInitialized={screen.GetComponent<KMonoBehaviour>().IsInitialized()}");

        var intSlider = screen.GetComponent<IntSliderSideScreen>();
        UnityEngine.Object.Destroy(intSlider);

        DistanceSliderSideScreen prefab = screen.AddComponent<DistanceSliderSideScreen>();
        prefab.name = nameof(DistanceSliderSideScreen);

        screen.SetActive(false);

        // OK, now we have a suitable prefab for the UI...
        DetailsScreen.Instance.sideScreens.Add(
            new DetailsScreen.SideScreenRef {
                name           = prefab.name,
                screenPrefab   = prefab,
                screenInstance = prefab,
                tab            = original.tab,
                offset         = original.offset
            }
        );
    }
}
