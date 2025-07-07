using KSerialization;

namespace SlippyCheeze.LogicRateLimiter;

[SerializationConfig(MemberSerialization.OptIn)]
public class LogicRateLimiter: LogicGate, ISingleSliderControl, ISliderControl {
    private static readonly HashedString OUTPUT_PORT_ID = (HashedString) "RateLimitedLogicOutput";


    private static int SecondsToTicks(float seconds)
        => (int)Math.Round(seconds / LogicCircuitManager.ClockTickInterval, MidpointRounding.AwayFromZero);


    public const float MaxPeriodInSeconds = 60f * 5;
    private readonly int MaxPeriodInTicks = SecondsToTicks(MaxPeriodInSeconds);

    public readonly float MinPeriodInSeconds = LogicCircuitManager.ClockTickInterval;
    private const   int   MinPeriodInTicks   = 1;

    public const float DefaultPeriodInSeconds = 5f;
    private readonly int DefaultPeriodInTicks = SecondsToTicks(DefaultPeriodInSeconds);


    public float Period {
        get => periodInTicks * LogicCircuitManager.ClockTickInterval;
        set => periodInTicks = SecondsToTicks(value);
    }

    // These two count time in LogicCircuitManager "ticks", which are the equivalent of the Unity
    // physics tick for handling logic circuit stuff.  So, we work internally in those units all the
    // time, and leave the float value as our "public" API and all.
    [Serialize]
    private int periodInTicks {
        get => field <= 0 ? field = DefaultPeriodInTicks : field;
        set {
            field = value.Clamp(MinPeriodInTicks, MaxPeriodInTicks);
            // ensure the remaining delay is reduced if our period was reduced.
            delayTicksRemaining = delayTicksRemaining.Clamp(0, field);
        }
    } = -1;

    // The number of  remaining that we are idle for.  This is set to
    // Period when we start sending a green signal, and counts down every tick until it hits zero
    // before we will send another.
    [Serialize]
    private int delayTicksRemaining {
        get;
        set {
            field = value;
            UpdateCountdownDisplay();
        }
    } = 0;


    // ==========================================================================================
    // UI interface for setting our Period
    public string SliderUnits => STRINGS.UI.UNITSUFFIXES.SECOND;

    public string SliderTitleKey => MODSTRINGS.UI.UISIDESCREENS.LOGICRATELIMITER.TITLE_key;
    // strictly, GetSliderTooltipKey is unnecessary, but it (a stupid) part of the interface, so we stuck.
    public string GetSliderTooltipKey(int _) => MODSTRINGS.UI.UISIDESCREENS.LOGICRATELIMITER.TOOLTIP_key;
    public string GetSliderTooltip(int _) => String.Format(MODSTRINGS.UI.UISIDESCREENS.LOGICRATELIMITER.TOOLTIP, Period);

    public int   SliderDecimalPlaces(int _)         => 1;
    public float GetSliderMin(int _)                => LogicCircuitManager.ClockTickInterval;
    public float GetSliderMax(int _)                => MaxPeriodInSeconds;
    public float GetSliderValue(int _)              => Period;
    public void  SetSliderValue(float value, int _) => Period = value;



    public override void OnPrefabInit() {
        base.OnPrefabInit();
        Subscribe<LogicRateLimiter>((int)GameHashes.CopySettings, LogicRateLimiter.OnCopySettingsDelegate);
    }

    public override void OnSpawn() {
        base.OnSpawn();
        UpdateCountdownDisplay(true);
    }

    // we promise solemly not to touch countdown until *after* we initialize it. :)
    private MeterController countdown => field ??= new(
        GetComponent<KBatchedAnimController>(),
        "meter_target",
        "meter",
        Meter.Offset.UserSpecified,
        Grid.SceneLayer.LogicGatesFront,
        Vector3.zero,
        null
    );

    private float meterPosition {
        get;                    // do I actually need this?
        set {
            value = value.Clamp(0f, 1f);
            if (field != value) {
                L.debug($"meterPosition: {field} => {value}");
                field = value;
                countdown.SetPositionPercent(value);
            }
        }
    } = 1f;

    private void UpdateCountdownDisplay(bool force = false) {
        // if we are disconnected from the logic network, or we are in the idle period between
        // pulses, set our countdown indicator to "done".
        float position = 1f;

        if (connected && delayTicksRemaining > 0) {
            // set it based on the remaining time we are refusing to emit a signal.  because this is
            // the "delay" before the next signal I have (a) colored it red, not green, and (b) we
            // "empty" rather than fill.
            position -= (1f / periodInTicks) * delayTicksRemaining;
        }

        meterPosition = position;
        if (force)              // on spawn, to force a visual update.
            countdown.SetPositionPercent(position);
    }



    // In the Klei logic system LogicTick and friends are called at a fixed interval, 0.1 seconds,
    // for something akin to the Unity physics tick: that is the simulation update rate, and it
    // defines the propogation of signals across the network.
    //
    // The flow of operations is:
    //
    // 1. UpdateLogicValues() on all networks
    //    a. LogicTick() on all senders attached to the network.
    //    b. outputValue <= any true value of GetLogicValue() on all senders.
    // 2. SendLogicEvents() on all networks
    //    a. ReceiveLogicEvent() on all receivers attached to the network
    //
    // In terms of our base class, the LogicGate:
    //
    // LogicTick() can be overriden in our code, and does nothing by default.
    //
    // ReceiveLogicEvent() triggers an unconditional UpdateState() on LogicGate.
    //     UpdateState sets `outputValueOne` to `GetCustomValue(new, old)`
    //
    // That means that the flow of events when our input changes:
    // 1. LogicTick() happens, with outputValueOne unchanged from last tick.
    // 2. GetLogicValue() is called, returning outputValueOne as modified by LogicTick()
    // 3. UpdateState() is called, setting outputValueOne to GetCustomValue()
    //        THIS VALUE IS NOT ACTUALLY SENT ON THE NETWORK
    // 4. The tick ends.
    // 5. LogicTick() is called, with outputValueOne NOT YET SENT ON THE NETWORK
    // 6. GetLogicValue() is called, returning outputValueOne
    //        THE OUTPUT IS ACTUALLY SENT ON THE NETWORK
    // 7. The tick ends.
    //
    // That means that GetCustomValue() happens *before* LogicTick(), so I have to handle the state
    // of "want to send signal" in LogicTick, not just "have sent signal".



    // The last value we received from our input.  To support weird hacks like running a logic
    // ribbon into us, etc, I'm going to always output whatever the input value was last time it
    // changed when I want to output anything.
    [Serialize]
    private int lastInputValue = 0;


    // Sadly, while I'd *love* to have a real GameStateMachine behind this, they don't have the
    // hooks needed to make it all work.  Very sad.  So, instead, I have this: an ersatz state
    // machine I wrote myself, full of bugs and pain.  whee!
    enum State { Ready, Emit, EmitWait, Wait }

    [Serialize]
    private State state {
        get;
        set {
            field = value;
            UpdateCountdownDisplay();
        }
    } = State.Ready;


    public override void LogicTick() {
        base.LogicTick();       // not that they do anything, but...

        L.debug($"state={state} delayTicksRemaining={delayTicksRemaining}");

        if (delayTicksRemaining > 0)
            delayTicksRemaining -= 1;

        switch (state) {
            case State.Ready:
                // nothing to be done until we get a value change.
                break;

            case State.Emit:
                // NOTE: if we trigger an emit, but the input line becomes red before we get to
                // sending it on the wire, we *still* send something.

                // if we want one pulse every second, that is every ten ticks.  which means *one*
                // tick with output gren, and *nine* ticks with it red.  so, we need to start
                // counting down from *now*, rather than from the end of the emit process.
                delayTicksRemaining = periodInTicks;

                // send whatever value we last received on the input line, which will be non-zero.
                outputValueOne = lastInputValue > 0 ? lastInputValue : 1;

                // ...and flag that, next tick, we need to turn off the output line and then wait
                // out the rest of our timespan on this planet.
                state = State.EmitWait;
                break;

            case State.EmitWait:
                // turn off output, and wait for the rest of our interval.
                outputValueOne = 0;
                state = State.Wait;
                break;

            case State.Wait when delayTicksRemaining > 0:
                // nothing to do but wait.  whee.
                break;

            case State.Wait when delayTicksRemaining <= 0:
                // if our input is still green, emit another pulse instantly.  otherwise wait for it
                // to become green in our "event handler" GetCustomValue.
                if (lastInputValue != 0)
                    state = State.Emit;
                else
                    state = State.Ready;
                break;
        }
    }



    // Called by our base class if, and only if, the signal on our input has changed; it is called
    // from the private `UpdateState()` method, so it can only handle a small portion of the actual
    // state changes we care about.
    //
    // Since it immediately updates our published state, though, we absolutely must implement it
    // correctly to filter out inappropriate changes while we are idle.
    //
    // Second argument is unused, because we only have one input, thanks base class.
    public override int GetCustomValue(int value, int _) {
        // cache the last value, so we can use it to set our output later, if appropriate.
        lastInputValue = value;

        L.debug($"value={value} state={state} outputValueOne={outputValueOne}");

        // If we are ready, this sends a signal on the output line too.
        if (value != 0 && state == State.Ready)
            state = State.Emit;

        // regardless, as far as the base class is concerned I want to send whatever was there
        // before this event triggered.  so, returning the current value makes this entire function
        // a noop as far as the output network is concerned. :)
        return outputValueOne;
    }


    // ==========================================================================================
    // copy-paste of settings support. :)
#pragma warning disable CS0414 // field is assigned but its value is never used
    [MyCmpAdd]
    private CopyBuildingSettings copyBuildingSettings = null!;
#pragma warning restore CS0414

    private static readonly EventSystem.IntraObjectHandler<LogicRateLimiter> OnCopySettingsDelegate
        = new((component, data) => component.OnCopySettings(data));

    private void OnCopySettings(object data) {
        if (data == null || data is not GameObject go || go == null) {
            L.error($"data == null: {data == null} | data is not GameObject: {data is not GameObject} | both true?  go == null, so the C++ Unity object has died.");
            return;
        }

        if (go.GetComponent<LogicRateLimiter>() is not LogicRateLimiter that || that == null) {
            L.error($"{go.Humanize()} did not have a LogicRateLimiter component, or the component == null");
            return;
        }

        // all that song and dance just to get here.
        this.Period = that.Period;
    }
}
