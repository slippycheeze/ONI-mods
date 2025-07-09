using System.Diagnostics;

namespace SlippyCheeze.SupportCode;

/*
 * Expected use:
 *
 * CoroutineYieldTimer timer = new();
 * while (...) {
 *     if (timer.ShouldYield) {
 *         yield return null;
 *         timer.Resume();      // forget to resume?  you will always yield.
 *     }
 *     // whatever you want to do in your coroutine, yo.
 * }
 * 
 */
public class CoroutineYieldTimer(double msBeforeYielding = 0.25) {
    // mathy math, to be more efficient during operations.
    private static readonly double msPerTick = (1_000_000_000.0 / Stopwatch.Frequency) / 1_000_000.0;
    private readonly long ticksBeforeYield = (long)(msBeforeYielding / msPerTick);

    private Stopwatch timer = new();

    public double totalRuntimeMS { get; private set; } = 0.0;
    public double lastRuntimeMS  { get; private set; } = 0.0;

    // start the timer running.  does not reset internal accumulated time.
    public CoroutineYieldTimer Start() { timer.Start(); return this; }
    public void Stop()  => timer.Stop();

    // call this to reset your timer, and start the countdown from scratch.
    public void Reset() => timer.Restart();

    // call this to resume the timer after you have returned from yielding.
    public void Resume() => timer.Restart();

    public bool ShouldYield {
        get {
            if (!timer.IsRunning) {
                L.warn($"timer was not running when ShouldYield was called, this is an error in logic");
                timer.Start();
                return false;
            }

            long elapsed = timer.ElapsedTicks;
            if (elapsed < ticksBeforeYield)
                return false;

            timer.Stop(); // no need to keep counting when we are not actively working.
            lastRuntimeMS = elapsed * msPerTick;
            totalRuntimeMS += lastRuntimeMS;

            return true;
        }
    }
}
