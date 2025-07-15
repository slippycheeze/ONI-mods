using PeterHan.PLib.Core;


// CRITICAL: if this changes *AT ALL* you **MUST** bump the version, and cope with older
// versions too.  so, uh, be sure?
using SharedLogHook = System.Action<
    string, /* time */
    int,    /* level, cast from LogLevel */
    string, /* thread */
    string, /* caller */
    string  /* message */
>;

namespace SlippyCheeze.SupportCode.LogErrorNotifier;

// this class exists in every mod, and provides the "client" side of the LogErrorNotifier
// integration across mods.  one version is elected "leader" by PLib, and will act as the holder for
// the remote call that'll actually perform the "add log message" part of the process.
public partial class RemoteLogListener: PForwardedComponent {
    // the instance of the class, which is set if we were the winner of the election.
    public static RemoteLogListener Instance => field ??= new();

    public RemoteLogListener() => RegisterForForwarding();

    // It is **CRITICAL** that the version is bumped if there could be version skew between mods.
    // While these are just private and I rebuild everything on change, fine, but if I ever release
    // these on steam or whatever I **MUST** respect that.
    //
    // This is a semi-stupid DotNet version, so has these shitty semantics:
    // - each number must be >= 0
    //
    // - supply two or four numbers, never three, which might be randomly enforced in places, but
    //   isn't very well error checked in the code.
    //
    // - GetHashCode is pretty much fucked, since it limits us to:
    //   - 0 <= major     <= 0x00F |   16       |  4 bits | >> 28 & 0xF
    //   - 0 <= minor     <= 0x0FF |  255       |  8 bits | >> 20 & 0xFF
    //   - 0 <= build     <= 0x0FF |  255       |  8 bits | >> 12 & 0xFF
    //   - 0 <= revision  <= 0xFFF | 4095       | 12 bits | >> 0  & 0xFFF
    //
    // absent use of GetHashCode, this'll compare a 31-bit positive int in each slot, but I'm going
    // to just use a boring single integer version and increment it on change, mapping that down to
    // the 32 bits we are actually permitted to use, and not caring beyond that.
    //
    // enjoy the zeros, losers, I guess.   I wonder if it'll optimize the constants out.  should.
    public override Version Version => field ??= new(
        (LogErrorNotifier.Version >> 28) & 0xF,
        (LogErrorNotifier.Version >> 20) & 0xFF,
        (LogErrorNotifier.Version >> 12) & 0xFF,
        (LogErrorNotifier.Version >>  0) & 0xFFF
    );

    // called on the newest version of the component, or one selected at random if there are
    // multiple with the same value.  we use this to determine who gets to slap their component on
    // Game, and export the queue to the rest of the instances.
    public override void Initialize(Harmony harmony) {
        harmony.Patch(
            typeof(Game).DeclaredMethod(nameof(Game.OnPrefabInit)),
            prefix: new(typeof(LogErrorNotifier).DeclaredMethod(nameof(LogErrorNotifier.BeforeGamePrefabInit)))
        );

        // because we won, we can export the shared message queue to everyone else.  beware: this
        // **MUST** be a Type that comes from the same Assembly in all of my mod instances, so it
        // gotta be something from System. or Unity. (or one of the Klei types, I guess)
        //
        // that, uh, includes `Func<>` as long as the types are all from System. too, though,
        // so... we can just do this little friendly fun fellow, and type *aliases* with `using` are
        // fine, so we can do this thing to export some code.
        //
        // ...hehe.  all the casting is just being explicit, mostly.
        SetSharedData((object)( (SharedLogHook)( SharedLogHookShim ) ));

        // we are the master instance; we will tell everyone else, later in PostInitialize, that
        // they are not.
        IsMasterInstance = true;
    }

    private bool IsMasterInstance = false;

    private const uint OnPostInitialize = (uint)740976147;  // no meaning, just random.

    public override void PostInitialize(Harmony _) => InvokeAllProcess(OnPostInitialize, this);
    public override void Process(uint operation, object data) {
        switch (operation) {
            case OnPostInitialize:
                if (IsMasterInstance) {
                    SetRealLogHandler(LogErrorNotifier.Add);
                } else {
                    sharedLogHook = (SharedLogHook) Instance.GetSharedData<SharedLogHook>();
                    SetRealLogHandler(SendRemoteLogHandler);
                }
                break;
        }
    }


    private static void SharedLogHookShim(string time, int level, string thread, string caller, string message)
        => LogErrorNotifier.Add(time, (LogLevel)level, thread, caller, message);


    // this is the "type" map of our log message hook.
    public delegate void OnLogMessageDelegate(string time, LogLevel level, string thread, string caller, string message);

    // this is the public API that Logging calls.  it switches implementation over time as we move
    // through our state, at least slightly reducing our overheads.
    public static OnLogMessageDelegate OnLogMessage = BeforeInitializedLogHandler;

    // This is our "initial" state: we check if we have initialized, and if not we queue any log
    // message locally.  when finally initialized we sub in the local or remote handler, and replay
    // the local queue of messages into it.
    public static void BeforeInitializedLogHandler(string time, LogLevel level, string thread, string caller, string message)
        => AddToLocalQueue(new(time, level, thread, caller, message));

    private static List<LogErrorNotifier.LogLine>? localQueue = null;
    private static void AddToLocalQueue(LogErrorNotifier.LogLine line) => (localQueue ??= []).Add(line);

    // I promise this'll never be used before being set. ;)
    private static SharedLogHook sharedLogHook = null!;
    private static void SendRemoteLogHandler(string time, LogLevel level, string thread, string caller, string message)
        => sharedLogHook(time, (int)level, thread, caller, message);


    // and the logic around changing the handler to a "real" one.
    private static void SetRealLogHandler(OnLogMessageDelegate handler) {
        OnLogMessage = handler;

        if (localQueue is not null) {
            // just in case we do some weird recursive thing on accident, clear the "shared" local
            // queue state.  also means we will free the instance (which'll never be used again)
            // when we return.
            var queue = localQueue;
            localQueue = null;

            foreach (var line in queue) {
                // the LogMessageHandler was replaced *before* we were called, so we just replace
                // through it to whatever destination should accept our logs.
                OnLogMessage(line.time, line.level, line.thread, line.caller, line.message);
            }
        }
    }
}
