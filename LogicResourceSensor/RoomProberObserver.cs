namespace SlippyCheeze.LogicResourceSensor;

[HarmonyPatch(typeof(RoomProber), nameof(RoomProber.RefreshRooms))]
public static class RoomProberObserver {
    // This event is invoked when the RoomProber has finished updating all rooms.  Unlike the Klei
    // `GameHashes.RoomUpdate` IPC, this doesn't care if you are a building subscribed to the room
    // or not.
    public static event Action<RoomProber>? OnRoomProberUpdated;

    // dev note: this method is hooked because it is the one exact place that sets
    // `RoomProber.dirty` to false, meaning that it considers any pending changes fully processed.
    // obvs postfix because of same.
    internal static void Postfix(RoomProber __instance) => OnRoomProberUpdated?.Invoke(__instance);
}
