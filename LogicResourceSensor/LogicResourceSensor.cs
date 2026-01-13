using KSerialization;

namespace SlippyCheeze.LogicResourceSensor;

// 2025-05-24 REVISIT: add support for showing only *unreserved* global inventory, using
// `worldInventory.GetAmount`, which is the version that excludes reserved volume from the count.
// Needs UI support too, to decide which of the two modes is being used.

// 2025-05-27 REVISIT: bump the update rate to 200ms or even 33ms, to improve response speed, but
// only search a limited number of cells each tick to batch and load balance the updates
// across time.

[HarmonyPatch]
[SerializationConfig(MemberSerialization.OptIn)]
public partial class LogicResourceSensor: Switch, ISaveLoadable, ISim200ms, IThresholdSwitch {
    public enum SensorMode {
        Distance = 0,
        Room     = 1,
        Global   = 2
    }

    [Serialize]
    private SensorMode __mode = SensorMode.Global;  // default is global.  much better default.
    public SensorMode Mode {
        get => __mode;
        set {
            if (__mode != value) {
                __mode = value;
                OnModeChangedInternal();  // will invoke OnModeChanged as appropriate.
            }
        }
    }
    // the first callback is our internal handler, via a shim to ignore the parameters.
    public event Action<LogicResourceSensor, SensorMode>? OnModeChanged;


    [Serialize]
    private int __distance = 3;
    public int Distance {
        get => __distance;
        set {
            value = value.Clamp(MinDistance, MaxDistance);
            if (__distance != value) {
                __distance = value;
                OnDistanceChangedInternal();  // will invoke OnDistanceChanged.
            }
        }
    }
    public event Action<LogicResourceSensor, int>? OnDistanceChanged;


    // maximum valid distance setting.  based on what the RoomProber considers our maximum valid size.
    // caching stuff because I don't wanna mess with float math every time someone asks.
    // private static RoomProber.Tuning __roomProberTuning => field ??= TuningData<RoomProber.Tuning>.Get();
    // private static int __lastKnownMaxDistance = -1;
    // public static int MaxDistance {
    //     get {
    //         if (__lastKnownMaxDistance != __roomProberTuning.maxRoomSize) {
    //             field = (int)Math.Ceiling(Math.Sqrt(__roomProberTuning.maxRoomSize));
    //             __lastKnownMaxDistance = __roomProberTuning.maxRoomSize;
    //         }
    //         return field;
    //     }
    // }

    public const int MaxDistance = 25;
    public const int MinDistance = 1;

    public const int MaxRoomSize = MaxDistance * MaxDistance;


    [Serialize]
    private bool __includeStorage = false;
    public bool IncludeStorage {
        get => __includeStorage;
        set {
            if (__includeStorage != value) {
                __includeStorage = value;
                OnIncludeStorageChanged?.Invoke(this, value);
            }
        }
    }
    public event Action<LogicResourceSensor, bool>? OnIncludeStorageChanged;

    [Serialize]
    private bool __includeReserved = false;
    public bool IncludeReserved {
        get => __includeReserved;
        set {
            if (__includeReserved != value) {
                __includeReserved = value;
                OnIncludeReservedChanged?.Invoke(this, value);
            }
        }
    }
    public event Action<LogicResourceSensor, bool>? OnIncludeReservedChanged;


    [Serialize]
    public float __threshold;
    public float Threshold {
        get => __threshold;
        set {
            if (__threshold != value) {
                __threshold = value;
                UpdateSwitchedOn();
            }
        }
    }

    [Serialize]
    public bool __activateAboveThreshold = true;
    public bool ActivateAboveThreshold {
        get => __activateAboveThreshold;
        set {
            if (__activateAboveThreshold != value) {
                __activateAboveThreshold = value;
                UpdateSwitchedOn();
            }
        }
    }


    // ======================================================================
    // The most critical current state of the object.  not saved, updated during sim ticks.
    public float CurrentMass {
        get;
        set {
            if (field != value) {
                field = value;
                UpdateSwitchedOn();
            }
        }
    } = 0f;


    // ======================================================================
    // UI Bindings: IThresholdSwitch
    ThresholdScreenLayoutType IThresholdSwitch.LayoutType => ThresholdScreenLayoutType.SliderBar;

    float IThresholdSwitch.CurrentValue => CurrentMass;

    LocString IThresholdSwitch.Title => MODSTRINGS.UI.UISIDESCREENS.RESOURCE_SENSOR_SIDESCREEN.TITLE;
    LocString IThresholdSwitch.ThresholdValueName => MODSTRINGS.UI.UISIDESCREENS.RESOURCE_SENSOR_SIDESCREEN.THRESHOLD.UNITS;
    string IThresholdSwitch.AboveToolTip => MODSTRINGS.UI.UISIDESCREENS.RESOURCE_SENSOR_SIDESCREEN.THRESHOLD.ABOVE;
    string IThresholdSwitch.BelowToolTip => MODSTRINGS.UI.UISIDESCREENS.RESOURCE_SENSOR_SIDESCREEN.THRESHOLD.BELOW;

    LocString IThresholdSwitch.ThresholdValueUnits() => GameUtil.GetCurrentMassUnit();
    string IThresholdSwitch.Format(float value, bool includeUnits)
        => GameUtil.GetFormattedMass(value, massFormat: GameUtil.MetricMassFormat.Kilogram, includeSuffix: includeUnits);


    // Using a Decimal avoids imprecision weirdness in rounding
    float IThresholdSwitch.ProcessedSliderValue(float input)
        => (float)Math.Round((Decimal)input, 0, MidpointRounding.AwayFromZero);

    float IThresholdSwitch.ProcessedInputValue(float input)
        => (float)Math.Round((Decimal)input, 2, MidpointRounding.AwayFromZero);


    private const float rangeMax = 10_000f;

    float IThresholdSwitch.RangeMin => 0f;
    float IThresholdSwitch.RangeMax => rangeMax;
    float IThresholdSwitch.GetRangeMinInputField() => 0f;
    float IThresholdSwitch.GetRangeMaxInputField() => Single.PositiveInfinity;

    int IThresholdSwitch.IncrementScale => ((IThresholdSwitch)this).CurrentValue switch {
        // for politeness, if impossibly below zero we also use a unit of 1kg
        <  1_000  =>     1,     //  10 / 10
        <  2_500  =>     5,     //  25 / 15
        <  5_000  =>    10,     //  50 / 25
        < 10_000  =>    25,     // 100 / 50
        < 25_000  =>   100,
        < 50_000  =>   250,
        _         => 1_000
    };
    NonLinearSlider.Range[] IThresholdSwitch.GetRanges => field ??= [
        new(10, 1_000),
        new(15, 2_500),
        new(25, 5_000),
        new(50, rangeMax),
    ];


    [MyCmpGet]
    private TreeFilterable treeFilterable = null!;
    // a locally cached copy of the acceptable tags, updated when the treeFilterable notifies us
    // that we just got a filter list change.
    private Tag[] filterAcceptsTags = [];

    // used for filtering later, may as well group it here. :)
    private static readonly Tag[] livingThingTags =
        GameTags.Minions.Models.AllModels.Append(GameTags.Creature).ToArray();



    // ======================================================================
    // Other Component References
    [MyCmpGet]
    private LogicPorts logicPorts = null!;

    [MyCmpGet]
    private KSelectable selectable = null!;

    [MyCmpGet]
    private KBatchedAnimController animController = null!;



    public override void OnPrefabInit() {
        base.OnPrefabInit();

        this.gameObject.AddOrGet<CopyBuildingSettings>();  // ensure component is present
        this.Subscribe((int)GameHashes.CopySettings, OnCopySettingsDelegate);
    }


    public override void OnSpawn() {
        base.OnSpawn();

        // hook up the range visualizer callback, so we control where it draws.
        // L.debug($"setting rangeVisualizer.BlockingCB to Cells.ExcludeCellFromVisualizer (Cells={Cells.IsOrIsNotNull()})");
        rangeVisualizer.BlockingCb = Cells.ExcludeCellFromVisualizer;


        // prevent dupes and the player changing our active state.
        manuallyControlled = false;

        // rather than polling for changes, get notified when the selection changes, so we can
        // respond to it directly.  also, fake the first notification, so we are in sync already.
        treeFilterable.OnFilterChanged += OnFilterChanged;
        OnFilterChanged(treeFilterable.AcceptedTags);

        // in SensorMode.Distance we care if a cell is solid or not, and this tells us to reconsider
        // if a cell should be included or excluded.
        // World.Instance.OnSolidChanged += OnSolidChanged;

        // force a refresh of some of our state.
        OnModeChangedInternal();
        // OnRoomChanged();
        UpdateSwitchedOn();     // may change the status as the underlying Switch sees
        UpdateSwitchStatus();   // so we force a "first" update through to initialize state.
    }

    public override void OnCleanUp() {
        base.OnCleanUp();
        Cells?.OnCleanUp();
        treeFilterable?.OnFilterChanged -= OnFilterChanged;
        WatchRoomChanges = false;
    }


    // =============================================================================================
    // copy-paste support. :)
    private static readonly EventSystem.IntraObjectHandler<LogicResourceSensor> OnCopySettingsDelegate
        = new((component, data) => component.OnCopySettings(data));

    private void OnCopySettings(object data) {
        if (data is null || data is not GameObject go) {
            L.error("received {data.Type} instead of the expected GameObject");
            return;
        }

        if (go.TryGetComponent<LogicResourceSensor>(out var that)) {
            this.ActivateAboveThreshold = that.ActivateAboveThreshold;
            this.Threshold              = that.Threshold;
            this.IncludeStorage         = that.IncludeStorage;
            this.IncludeReserved        = that.IncludeReserved;
            this.Distance               = that.Distance;
            this.Mode                   = that.Mode;
        }
    }


    // =============================================================================================
    // The way we handle actually doing the work: use a delegate class based on our mode.  When that
    // changes switch to a new instance of the appropriate type.  Then ask it to handle all the
    // actual work involved in doing the calculation.
    //
    // This avoids stacking up more and more methods on this class that implement the same logic
    // through a careful process of if and switch statements....
    private void OnModeChangedInternal() {
        mustRecalculateCells = true;

        // allow things like our UI to update in response to the change
        OnModeChanged?.Invoke(this, this.Mode);

        // ...finally, an off-schedule update to get things going as they should immediately.
        Sim200ms(0);
    }

    private void OnDistanceChangedInternal() {
        mustRecalculateCells = true;

        // allow things like our UI to update in response to the change
        OnDistanceChanged?.Invoke(this, this.Distance);

        // ...finally, an off-schedule update to get things going as they should immediately.
        Sim200ms(0);
    }


    // Handle the routine work of updating our state, and reporting it.  Since we delegate all the
    // hard work to our ResourceScanner, there isn't much to do here.
    public void Sim200ms(float dt) {
        // perform any pending updates to our state.
        if (mustRecalculateCells)
            RecalculateCells();

        // Finally, update CurrentMass appropriately to our mode.
        if (Mode == SensorMode.Global)
            CurrentMass = CountItemsInGlobalInventory();
        else
            CurrentMass = Cells.TotalMass;
    }


    private void UpdateSwitchedOn() {
        // the test here matches other sensors, which are exclusive of the Threshold value.
        bool meetsThreshold = ActivateAboveThreshold switch {
            true  => CurrentMass > Threshold,
            false => CurrentMass < Threshold
        };
        bool hasFilter = filterAcceptsTags.Length > 0;

        SetState(hasFilter && meetsThreshold);
    }


    // =======================================================================================
    // LogicMassSwitch and LogicSwitch, copied, because neither can serve as a useful base, and
    // there is no common "LogicSwitchBase" class to use.

    // Switch uses a different status item, `SwitchStatus{Active,Inactive}`, and every logic switch
    // overrides with this same code. -_____-
    private static StatusItem LogicSensorStatusActive   = Db.Get().BuildingStatusItems.LogicSensorStatusActive;
    private static StatusItem LogicSensorStatusInactive = Db.Get().BuildingStatusItems.LogicSensorStatusInactive;
    private static StatusItemCategory PowerCategory     = Db.Get().StatusItemCategories.Power;

    // Called by Switch when our state actually changes.
    public override void UpdateSwitchStatus() {
        selectable.SetStatusItem(
            PowerCategory,
            IsSwitchedOn ? LogicSensorStatusActive : LogicSensorStatusInactive
        );

        PlayWorkingAnimation = IsSwitchedOn;

        // we are just a one-bit logic sender, so bit 1 is toggled on the wire.
        logicPorts.SendSignal(LogicSwitch.PORT_ID, IsSwitchedOn ? 1 : 0);
    }

    // our animation "state machine", which only has two states, matching our switch state of ON or
    // OFF.  ...however, we need *three* states here, because we only update when our "current"
    // state actually changes, but we need to do that unconditionally the very first time we are set.
    //
    // ...so, the classic boolean values of TRUE, FALSE, and FILE_NOT_FOUND, most easily represented
    // in C# as Nullable<bool>, which uses `true`, `false`, and `null`, for the three states.
    [DisallowNull]              // static analysis should yell if you try and assign FILE_NOT_FOUND. ;)
    internal bool? PlayWorkingAnimation {
        get;
        private set {
            if (field == value)
                return;

            field = value;
            if (field == true) {
                animController.Play("Working_pre");
                animController.Queue("Working_loop", KAnim.PlayMode.Loop);
            } else {
                animController.Play("Working_pst");
                animController.Queue("off");
            }
        }
    }



    // =======================================================================================
    // handle changes to our filters.
        private void OnFilterChanged(HashSet<Tag> tags) {
        // keep a cached local copy for future performance reasons.
        filterAcceptsTags = [.. tags];

        // and make that we will need to recalculate what to pick up later.
        Cells.MarkAllCellsDirty();
    }



    // =======================================================================================
    // some general support infrastructure for the Cells tracking.
    private int Cell {
        get {
            if (field == Grid.InvalidCell)
                field = this.NaturalBuildingCell();
            return field;
        }
    } = Grid.InvalidCell;

    private Vector2I CellXY {
        get {
            if (field.x == Grid.InvalidCell)
                field = Grid.CellToXY(Cell);
            return field;
        }
    } = new(Grid.InvalidCell, Grid.InvalidCell);



    // =======================================================================================
    // Track our Cells, along with the weight present in each one.  This allows
    // fine-grained updates from our subscription to change notifiers in the future.
    //
    // We also maintain a Visualizer which will highlight the cells we scan, when appropriate.

    // A one-shot trigger to batch all changes to our Cells between updates, and process
    // them all at once.  I really do want the non-threaded version of AutoResetEvent. -_-
    private bool mustRecalculateCells {
        get {
            bool result = field;
            field = false;
            return result;
        }
        set;
    }

    // The Klei RangeVisualizer is used to show the cells we are watching, if appropriate.
    // This uses the data from cellsOfInterest to quickly communicate if it should be highlighted.
    [MyCmpGet]
    private RangeVisualizer rangeVisualizer = null!;


    // The amount of mass in each cell of interest; this contains exclusively the cells that *could*
    // have something in them, that is, valid, in the same world, and not solid.
    private class CellData(LogicResourceSensor sensor, int Cell, RangeVisualizer rangeVisualizer) {
        // ensure we do whatever is needed to clean up, keeping in mind this *could* happen when our
        // custom component is removed from a GameObject, which target will not necessarily be destroyed.
        public void OnCleanUp() {
            // that'll shut down all the background processing and subscriptions, and ensure we are
            // ready to be destroyed without wasting time or effort.
            this.Clear();
        }

        // all cells that were proposed to us, which is used later during the OnSolidChanged handler
        // to determine if the cell should be updated or not.
        private HashSet<int> AllCells = [];
        public IEnumerable<int> GetAllCandidateCells() => AllCells;

        // per-cell mass tracking, to allow fine grained updating over time.
        private Dictionary<int, float> Mass = [];

        public int Count => Mass.Count;
        public void SetCellMass(int cell, float mass) {
            if (!Mass.ContainsKey(cell)) {
                L.error($"Cells does not contain cell={cell} when setting mass={mass}");
                return;
            }
            Mass[cell] = mass;
            totalMass = null;   // force recalculation next time it is required.
        }

        private Vector2I CellXY = Grid.CellToXY(Cell);

        private float? totalMass = null;
        public float TotalMass {
            get {
                if (totalMass == null) {
                    L.debug($"recalculating TotalMass for {"cell".ToQuantity(Mass.Count)} ({"dirty cell".ToQuantity(dirtyCells.Count)})");
                    totalMass = Mass.Values.Sum();
                }
                return (float)totalMass;
            }
        }

        private static Vector2I InvalidCellXY = new(Grid.InvalidCell, Grid.InvalidCell);

        public Vector2I MinXY { get; private set; } = InvalidCellXY;
        public int Min { get; private set; } = Grid.InvalidCell;

        public Vector2I MaxXY { get; private set; } = InvalidCellXY;
        public int Max { get; private set; } = Grid.InvalidCell;

        // is this cell in our set of interesting cells?  used by the rangeVisualizer, which first
        // checks if the cell it is drawing is within the bounds it was given, to determine if the
        // specific cell should have the effect played.
        //
        // which, for us, means "it was **NOT** added to our set of valid cells", since they ask
        // "should we exclude this cell?"
        public bool ExcludeCellFromVisualizer(int cell) {
            bool result = !Mass.ContainsKey(cell);
            // L.debug($"cell={cell} @{Grid.CellToXY(cell)}: {result}");
            return result;
        }


        public void Clear() {
            AllCells.Clear();
            Mass.Clear();
            GrowBounds(Grid.InvalidCell);

            BackgroundScanning = false;
            dirtyCells.Clear();
            totalMass = null;   // force recalculation of totalMass too.
        }

        public void TryAdd(int x, int y) => TryAdd(Grid.XYToCell(x, y));

        public void TryAdd(int cell) {
            // L.debug($"TryAdd({cell})");
            if (!Grid.AreCellsInSameWorld(cell, Cell))
                return;

            AllCells.Add(cell);  // record for later, in case the next check changes. :)

            if (Grid.Solid[cell])
                return;

            // This is a legal cell to track, add it. :)
            Mass[cell] = 0f;

            GrowBounds(cell);         // update our bounds and the rangeVisualizer
            MarkCellDirty(cell);        // schedule for background update of the cell mass.
            BackgroundScanning = true;  // start background scanning, if it isn't already running.
            return;
        }

        public void Remove(int cell) {
            Mass.Remove(cell);
            ShrinkBounds(cell);
        }


        // ================================================================================
        // internal maintenance logic, and so much of it. :(
        private void GrowBounds(int cell) {
            bool boundsChanged = false;

            if (cell == Grid.InvalidCell) {
                // reset everything.
                Min   = Max   = Grid.InvalidCell;
                MinXY = MaxXY = InvalidCellXY;
                boundsChanged = true;
            } else {
                if (cell < Min || Min == Grid.InvalidCell)
                    Min = cell;

                if (cell > Max || Max == Grid.InvalidCell)
                    Max = cell;

                // now we have our edges, we also need to update our bounds, which may differ: we
                // could add a cell that is further left or right than any existing cell, but which
                // is still between existing min and max cell since there are (potentially) gaps on
                // each row at the horizontal edges.
                Grid.CellToXY(cell, out int x, out int y);

                if (x < MinXY.x || MinXY.x == Grid.InvalidCell) { MinXY = MinXY with { x = x }; boundsChanged = true; }
                if (y < MinXY.y || MinXY.y == Grid.InvalidCell) { MinXY = MinXY with { y = y }; boundsChanged = true; }
                if (x > MaxXY.x || MaxXY.x == Grid.InvalidCell) { MaxXY = MaxXY with { x = x }; boundsChanged = true; }
                if (y > MaxXY.y || MaxXY.y == Grid.InvalidCell) { MaxXY = MaxXY with { y = y }; boundsChanged = true; }
            }

            if (boundsChanged)
                OnBoundsChanged();
        }

        private void ShrinkBounds(int cell) {
            if (!Grid.IsValidCell(cell))
                return;

            // OK, to make this happen we start with the quick test: is the cell inside our border,
            // on all sides?  if so, removing it can't possibly have changed our borders, even if it
            // hollowed out the middle, so we have nothing more to do.
            Grid.CellToXY(cell, out int x, out int y);
            if (x > MinXY.x && x < MaxXY.x && y > MinXY.y && y < MaxXY.y)
                return;         // not on the boundary line, buh-bye.

            // It was not safely inside the borders, so recalculate them from first principals.
            var xy = Mass.Keys.Select(Grid.CellToXY);
            var newMin = xy.Aggregate(Vector2I.Min);
            var newMax = xy.Aggregate(Vector2I.Max);

            if (newMin == MinXY && newMax == MaxXY)
                return;         // weird but possible?

            OnBoundsChanged();  // they definitely did
        }

        private void OnBoundsChanged() {
            // L.debug($"new area is {MinXY} => {MaxXY}");
            UpdateRangeVisualizer();
            UpdateGameChangeWatcher();
        }

        private void UpdateRangeVisualizer() {
            if (Min == Grid.InvalidCell || Max == Grid.InvalidCell) {
                // disable the rangeVisualizer, which uses as *inclusive* pair of values.
                rangeVisualizer.RangeMin = Vector2I.zero;
                rangeVisualizer.RangeMax = Vector2I.minusone;
                return;
            }

            // rangeVisualizer uses an *inclusive* XY offset from origin.
            // we have *inclusive* absolute cell positions.
            // so we get to translate between those two systems, yay.
            rangeVisualizer.RangeMin = MinXY - CellXY;  // -inf < N <= 0,  since min <= origin
            rangeVisualizer.RangeMax = MaxXY - CellXY;  // 0 <= N <= +inf, since max >= origin
        }


        // ================================================================================
        // background processing: scanning the cells for updates in the background
        private Queue<int> dirtyCells = new();
        private Coroutine? coroutineHandle = null;

        private void MarkCellDirty(int cell) {
            if (!dirtyCells.Contains(cell))
                dirtyCells.Enqueue(cell);
        }

        public void MarkAllCellsDirty() {
            dirtyCells.Clear();
            int[] cells = Mass.Keys.ToArray();  // can't modify while iterating, and I'm gonna modify...
            foreach (int cell in cells) {
                Mass[cell] = 0;  // reset to zero, so we don't have incorrect results while waiting.
                dirtyCells.Enqueue(cell);
            }
            totalMass = null;   // force recalculation of totalMass too.
        }


        private bool BackgroundScanning {
            get => coroutineHandle != null;
            set {
                if (value == true) {  // ensure running
                    if (coroutineHandle == null) {
                        L.debug($"starting BackgroundScanning coroutine");
                        coroutineHandle = sensor.StartCoroutine(ScanDirtyCells());
                    }
                } else {              // ensure stopped
                    if (coroutineHandle != null) {
                        L.debug($"stopping BackgroundScanning coroutine");
                        sensor.StopCoroutine(coroutineHandle);
                        coroutineHandle = null;
                    }
                }
            }
        }

        IEnumerator<object?> ScanDirtyCells() {
            L.debug($"coroutine initialized");
            CoroutineYieldTimer timer = new(msBeforeYielding: 0.25);

            // immediately pause until the next frame when started, to allow time for additional
            // work to be enqueued before we start processing it.
            yield return null;
            timer.Start();

            while (true) {      // we are stopped externally, without our involvement.
                if (dirtyCells.Count == 0) {
                    L.debug($"no cells in queue, waiting for more work");
                    yield return new WaitUntil(() => dirtyCells.Count > 0);
                    timer.Reset();
                }

                int scanned = 0;
                while (!timer.ShouldYield && dirtyCells.Count > 0) {
                    scanned++;

                    int cell = dirtyCells.Dequeue();

                    // ignore out-of-area messages.  not that we really should have any, but just in case.
                    if (!Mass.ContainsKey(cell)) {
                        L.warn($"Ignoring scan request in unmonitored cell {cell} ({Grid.CellToXY(cell)})");
                        continue;
                    }

                    SetCellMass(cell, sensor.CountItemsInCell(cell));
                }

                L.debug($"coroutine yielding after processing {"cell".ToQuantity(scanned)} in {timer.lastRuntimeMS}ms");
                yield return null;
                timer.Resume();
            }
        }


        // ============================================================================================
        // event driven processing: listen for changes in pickupables from the GameScenePartitioner, and
        // use that information to queue background scanning of the changed cell.

        // the handles from registering a GameScenePartitioner change tracking callback.
        private HandleVector<int>.Handle pickupablesChangedEntry;

        // called when the bounds of the current object have changed, so we need to refresh our
        // subscription to the GameScenePartitioner.
        private void UpdateGameChangeWatcher() {
            // Whatever happens here we are going to unsubscribe the *current* callback, because it
            // is tied specifically to the bounds of our set of cells, and those just changed.
            GameScenePartitioner.Instance.Free(ref pickupablesChangedEntry);
            World.Instance.OnSolidChanged -= OnSolidChanged;

            // if the bounds are invalid, don't add a new callback, so we just free it.
            if (MinXY == InvalidCellXY || MaxXY == InvalidCellXY)
                return;


            // bounds are valid, and have changed, so we have at least one cell to watch.
            // update our subscription to cover the region.

            // size is +1 because we are inclusive, but their width and heigh args are exclusive.
            Vector2I size = (MaxXY - MinXY) + 1;
            L.debug($"subscribing GameScenePartitioner callbacks on {MinXY} + {size} (MaxXY={MaxXY})");

            pickupablesChangedEntry = GameScenePartitioner.Instance.Add(
                "LogicResourceSensor.Cells.OnPickupablesChanged",
                sensor.gameObject,     // not entirely sure what this is used for...
                MinXY.x, MinXY.y,
                size.x, size.y,  // width and height, technically.
                GameScenePartitioner.Instance.pickupablesChangedLayer,  // thanks, LogicMassSensor
                OnPickupablesChanged
            );

            L.debug($"subscribing to World.Instance.OnSolidChanged");
            World.Instance.OnSolidChanged += OnSolidChanged;
        }

        internal void OnPickupablesChanged(object data) {
            if (data is null || data is not Pickupable item) {
                L.error($"data is {(data == null ? "<null>" : data.GetType().FullName)}, not Pickupable, can't cope!?");
                return;
            }

            // ignore minions and critters.   maybe plants too?
            if (item.HasAnyTags(livingThingTags))
                return;

            // otherwise, queue the cell to be scanned again in future.  I'm definitely not brave
            // enough at this time to be adjusting cell mass based on the messages here.
            int cell = item.GetCell();
            MarkCellDirty(cell);
        }


        // sadly, can't use the GameScenePartitioner to subscribe for changes to solids, as it
        // doesn't pass sufficient information to us (or with the correct timing) to understand what
        // has happened.
        //
        // instead, we subscribe to change events from the World, which are sadly global, but which
        // also ensure the world is up-to-date on the state of the cell when we ask.
        internal void OnSolidChanged(int cell) {
            if (!AllCells.Contains(cell))
                return;         // not in our candidate cell set.

            // if it was proposed, and now changed, great, update based on it.
            if (Grid.Solid[cell])
                Remove(cell);
            else
                TryAdd(cell);
        }
    }
    private CellData Cells => field ??= new(this, Cell, rangeVisualizer);


    private void RecalculateCells() {
        // L.debug($"starting recalculation of Cells with Mode={Mode} Distance={Distance}");

        Cells.Clear();          // all the branches start this way, so...

        switch (Mode) {
            case SensorMode.Global:
                WatchRoomChanges = false;
                break;

            case SensorMode.Distance:
                WatchRoomChanges = false;

                // it is more efficient to run horizontally than vertically when touching the Grid
                //
                // reusing the sequence saves on allocation and computation, and won't be optimized
                // away by the compiler, so....
                for (int y = CellXY.y - Distance; y <= CellXY.y + Distance; y++) {
                    for (int x = CellXY.x - Distance; x <= CellXY.x + Distance; x++) {
                        Cells.TryAdd(x, y);
                    }
                }
                break;

            case SensorMode.Room:
                WatchRoomChanges = true;
                if (currentRoom?.cavity is not null) {
                    // we can actually do something here, which is good.  watch all cells that are
                    // part of the room, or, rather, the cavity associated with it.
                    foreach (int cell in currentRoom.CavityCells())
                        Cells.TryAdd(cell);
                }
                break;

            default:
                throw new InvalidOperationException($"REVISIT: implement SensorMode.{Mode} support in RecalculateCells");
        }
    }

    private string FormatBoundsForComparison() {
        StringBuilder result = new();

        // gonna align everything nicely, go me?
        int minX = Math.Max(Cells.MinXY.x.ToString().Length, rangeVisualizer.RangeMin.x.ToString().Length);
        int minY = Math.Max(Cells.MinXY.y.ToString().Length, rangeVisualizer.RangeMin.y.ToString().Length);
        int maxX = Math.Max(Cells.MaxXY.x.ToString().Length, rangeVisualizer.RangeMax.x.ToString().Length);
        int maxY = Math.Max(Cells.MaxXY.y.ToString().Length, rangeVisualizer.RangeMax.y.ToString().Length);

        void AppendVec(StringBuilder sb, Vector2I vec, int min, int max) => sb
            .Append("(")
            .Append(vec.x.ToString().PadRight(min, ' '))
            .Append(" × ")
            .Append(vec.y.ToString().PadRight(max, ' '))
            .Append(")");

        const string origin = "         Origin: ";
        const string cells  = "          Cells: ";
        const string range  = "rangeVisualizer: ";

        result.AppendLine("calculated bounds:");

        result.Append(origin);
        AppendVec(result, CellXY, 0, 0);
        result.AppendLine();

        result.Append(cells);
        AppendVec(result, Cells.MinXY, minX, minY);
        result.Append(" ⇒ ");
        AppendVec(result, Cells.MaxXY, maxX, maxY);
        result.AppendLine();

        result.Append(range);
        AppendVec(result, rangeVisualizer.RangeMin, minX, minY);
        result.Append(" ⇒ ");
        AppendVec(result, rangeVisualizer.RangeMax, maxX, maxY);
        result.AppendLine();

        return result.ToString();
    }



    // =======================================================================================
    // tracking our current room, and handling associated error reporting.
    //
    // when we are in SensorMode.Room, this matters: we need to
    private bool WatchRoomChanges {
        get;
        set {
            if (value) {
                RoomProberObserver.OnRoomProberUpdated += OnRoomProberUpdated;
                currentRoom = Game.Instance.roomProber.GetRoomOfGameObject(gameObject);
            } else {
                RoomProberObserver.OnRoomProberUpdated -= OnRoomProberUpdated;
                currentRoom = null;
            }
            field = value;
        }
    } = false;

    private void OnRoomProberUpdated(RoomProber roomProber) {
        if (roomProber == null || this?.gameObject == null)
            return;

        Room? room = roomProber.GetRoomOfGameObject(this.gameObject);
        // L.debug(room.IsOrIsNotNull());
        currentRoom = room;
    }

    private CavityInfo? currentCavity = null;
    private Room? haveComplainedAboutBadRoom = null;
    private Room? currentRoom {
        get;
        set {
            if (value != null) {
                // if the room isn't valid, don't allow it to be assigned.
                if (value.cavity.NumCells > MaxRoomSize) {
                    if (haveComplainedAboutBadRoom != value) {
                        haveComplainedAboutBadRoom = value;
                        L.warn($"currentRoom is invalid: numCells {value.cavity.NumCells} > {MaxRoomSize} MaxRoomSize, ignoring");
                    }
                    value = null;
                }
            }

            if (field != value) {
                field = value;
                bool cavityChanged = field?.cavity == currentCavity;
                currentCavity = field?.cavity;
                OnRoomChanged(cavityChanged: cavityChanged);
            }

            if (field == null)
                currentCavity = null;

            // always, unconditionally, update the warning if someone tried to set our value.
            // something might should have changed in a way that'll need the update.
            UpdateRoomStatusWarning();
        }
    }


    // when we are in a room-oriented mode, and outside a room, a status item is added.  later, we
    // need remove it, for which we track it in this GUID here:
    // - if null, no status item should be shown.
    // - if not null, the status itum GUID for removal.
    private Guid notInAnyRoomStatusGUID = Guid.Empty;
    private void UpdateRoomStatusWarning() {
        // handle the status alert situation for being in a room or not.
        if (Mode == SensorMode.Room && currentRoom == null) {
            // we need a room, and we don't have one.  show the status, if we are not already.
            // this is the only state in which we need one.
            if (notInAnyRoomStatusGUID == Guid.Empty) {
                notInAnyRoomStatusGUID = selectable.AddStatusItem(Db.Get().BuildingStatusItems.NotInAnyRoom);
            }
        } else if (notInAnyRoomStatusGUID != Guid.Empty) {
            // any other combination of factors, no status item, but we have one ... so remove it.
            selectable.RemoveStatusItem(notInAnyRoomStatusGUID);
            notInAnyRoomStatusGUID = Guid.Empty;
        }
    }


    private void OnRoomChanged(bool cavityChanged) {
        if (currentRoom is null || cavityChanged) {
            // can't do incremental, rebuild everything.
            mustRecalculateCells = true;
            return;
        }

        // ...but if it didn't we can try for an incremental change.
        var newCells = currentRoom.CavityCells();
        var oldCells = Cells.GetAllCandidateCells();

        var removed = oldCells.Except(newCells);
        var added   = newCells.Except(oldCells);

        L.debug($"room changed, but same cavity: incremental update found {"cells".ToQuantity(added.Count())} added and {"cells".ToQuantity(removed.Count())} removed");

        foreach (int cell in removed)
            Cells.Remove(cell);

        foreach (int cell in added)
            Cells.TryAdd(cell);
    }

    // private void OnRoomChanged() {
    //     // from this point on, only room mode need apply; we are going to rebuild our cells of
    //     // interest based on the room we now occupy.
    //     if (Mode != SensorMode.Room)
    //         return;

    //     ClearCellsOfInterest();

    //     if (currentRoom == null) {
    //         DisableRangeVisualizer();
    //         return;
    //     }

    //     if (currentRoom.cavity == null)   // no cavity, no worky
    //         return;
    //     CavityInfo myCavity = currentRoom.cavity!;
    //     RoomProber roomProber = Game.Instance.roomProber!;

    //     // first, set the visualizer range based on the cavity bounding box.
    //     // sadly, have to convert into an offset from my cell, but such is life.
    //     rangeVisualizer.RangeMin = new Vector2I(myCavity.minX, myCavity.minY) - myCellXY;
    //     rangeVisualizer.RangeMax = new Vector2I(myCavity.maxX, myCavity.maxY) - myCellXY;

    //     // second, scan that bounding box and figure out which cells are actually part of the room.
    //     L.debug($"currentRoom={currentRoom} .roomType={currentRoom?.roomType}");
    //     L.debug($"myCavity={myCavity} .dirty={myCavity?.dirty} .numCells={myCavity?.numCells} bounds={myCavity?.minX}×{myCavity?.minY}⇒{myCavity?.maxX}×{myCavity?.maxY}");
    //     L.debug(TuningData<RoomProber.Tuning>.Get().maxRoomSize);
    //     for (int x = myCavity!.minX; x <= myCavity.maxX; x++) {
    //         for (int y = myCavity.minY; y <= myCavity.maxY; y++) {
    //             int cell = Grid.XYToCell(x, y);
    //             if (!Grid.IsValidCell(cell)) {
    //                 L.debug("cell {cell} at {x}×{y} is not valid!");
    //                 continue;
    //             }
    //             if (!Grid.AreCellsInSameWorld(cell, myCell)) {
    //                 L.debug("cell {cell} at {x}×{y} is not in the same world as myCell {myCell}");
    //                 continue;
    //             }
    //             var cellCavity = roomProber.GetCavityForCell(cell);
    //             bool inMyCavity = (myCavity == cellCavity);
    //             bool inMyRoom = (currentRoom == cellCavity?.room);
    //             if (inMyCavity) {
    //                 L.debug($"myCell {myCellXY} cell {x}×{y} inMyCavity={inMyCavity} inMyRoom={inMyRoom}");
    //                 AddCellOfInterest(cell);
    //             }
    //         }
    //     }
    //     L.debug(cells.Count);
    // }


    private float CountItemsInGlobalInventory() {
        // using var _ = new Perf.Timer();

        const bool includeRelatedWorlds = false;

        var inventory = gameObject.GetMyWorld().worldInventory;
        Func<Tag, bool, float> getMethod = IncludeReserved ? inventory.GetTotalAmount : inventory.GetAmount;

        float totalMass = 0;
        foreach (Tag tag in filterAcceptsTags) {
            totalMass += getMethod(tag, includeRelatedWorlds);
        }
        return totalMass;
    }

    private float CountItemsInCell(int cell) {
        float totalMass = 0f;

        for (var listItem = Grid.Objects[cell, (int)ObjectLayer.Pickupables]
                 ?.GetComponent<Pickupable>()
                 ?.objectLayerListItem;
             listItem != null;
             listItem = listItem.nextItem
        ) {
            GameObject item = listItem.gameObject;
            if (item == null)
                continue;

            if (item.HasAnyTags(livingThingTags))
                continue;

            if (item.HasAnyTags(filterAcceptsTags))
                totalMass += item.GetComponent<PrimaryElement>()?.Mass ?? 0;
        }
        return totalMass;
    }

    // private float CountItemsInBuilding(GameObject go) {
    //     if (go.TryGetComponent(out BuildingUnderConstruction _))
    //         return 0;

    //     // 2025-05-24 found this didn't account for the StorageTile, which kind of sucks, and
    //     // obviously none of the mod storage items.
    //     //
    //     // if (!go.TryGetComponent(out StorageLocker _)
    //     //     && !go.TryGetComponent(out StorageLockerSmart _)
    //     //     && !go.TryGetComponent(out RationBox _)
    //     //     && !go.TryGetComponent(out Refrigerator _))
    //     //     return 0;

    //     // for now, consider *any* storage that dupes can remove items from to be valid!
    //     var storage = go.GetComponent<Storage>();
    //     if (storage == null || storage.allowItemRemoval == false)
    //         return 0;

    //     return storage.items
    //         .Where(item => item.HasAnyTags(filterAcceptsTags))
    //         .Sum(item => item.GetComponent<PrimaryElement>()?.Mass ?? 0);
    // }
}
