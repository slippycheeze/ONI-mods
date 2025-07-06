namespace SlippyCheeze.SupportCode;

public static class LinqExtensions {
    public static (IEnumerable<T>, IEnumerable<T>) Partition<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        => (source.Where(predicate), source.Where(t => !predicate(t)));

    public static void ForEach<T>(this IEnumerable<T> seq, Action<T> action) {
        foreach (T item in seq)
            action(item);
    }

    public static void ForEach<T>(this IEnumerable<T> seq, Action<T, int> action) {
        int index = 0;
        foreach (T item in seq)
            action(item, index++);
    }


    public static T? MaxBy<T, U>(this IEnumerable<T> seq, Func<T, U> transform) {
        if (!seq.Any())
            return default;

        var comparer = Comparer<U>.Default;

        T max = seq.First();
        U val = transform(max);

        foreach (var item in seq.Skip(1)) {
            U candidate = transform(item);
            if (comparer.Compare(candidate, val) > 0) {
                max = item;
                val = candidate;
            }
        }

        return max;
    }
}
