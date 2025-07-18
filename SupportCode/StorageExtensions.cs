namespace SlippyCheeze.SupportCode;

public static class StorageExtensions {
    // Get **A** PrimaryElement for the element with the highest mass.  In some cases a Storage has
    // more than one object with a matching PrimaryElement, so the total mass available may be
    // greater than the mass of the individual item returned.
    //
    // OTOH, this makes one pass over the list, where multiple calls to any of the `GetMass` methods
    // would result in one pass over the list for each thing we query on.
    public static PrimaryElement? PrimaryElementWithHighestMass(this Storage storage) {
        if (storage == null)
            return null;

        PrimaryElement? result = null;
        float resultMass = -1;

        int count = storage.items.Count;
        Dictionary<SimHashes, float> massByElement = new(count);
        for (int i = 0; i < count; i++) {
            if (storage.items[i] is not GameObject go)
                continue;       // WTF?   but whatever, safety first.

            if (!go.TryGetComponent<PrimaryElement>(out PrimaryElement element))
                continue;

            // don't care if exists or not: if not, it'll supply `default(float)` AKA 0.0f
            massByElement.TryGetValue(element.ElementID, out float mass);
            mass += element.Mass;

            if (mass > resultMass) {
                // keep the *first* found PrimaryElement instance as our result.
                result     = element.ElementID == result?.ElementID ? result : element;
                resultMass = mass;
            }

            // either way, update the "so far" counter.
            massByElement[element.ElementID] = mass;
        }

        // in the event we found only empty PrimaryElement holders, which can theoretically happen
        // if someone messes with my stored objects, then return null: they don't count, in this case.
        return resultMass > 0 ? result : null;
    }
}
