using System.Collections.ObjectModel;

namespace GenericsApp;

public static class EnumerableExtensions
{
    // R1 - Page 1 is the first page. Arguments are validated immediately (not lazily),
    // then the real work is done by a private iterator method.
    public static IEnumerable<T> Page<T>(this IEnumerable<T> source, int pageNumber, int pageSize)
    {
        ArgumentNullException.ThrowIfNull(source);

        if (pageNumber < 1)
            throw new ArgumentOutOfRangeException(nameof(pageNumber), pageNumber,
                "Page number must be 1 or greater.");
        if (pageSize < 1)
            throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize,
                "Page size must be 1 or greater.");

        return PageIterator(source, pageNumber, pageSize);
    }

    private static IEnumerable<T> PageIterator<T>(IEnumerable<T> source, int pageNumber, int pageSize)
    {
        // long avoids int overflow for very large page numbers.
        long start = (long)(pageNumber - 1) * pageSize;
        long end = start + pageSize;
        long index = 0;

        foreach (var item in source)
        {
            if (index >= end)
                yield break;          // past the page: stop early, don't read the rest

            if (index >= start)
                yield return item;

            index++;
        }
    }

    // R2 - first item with this id, or default (null for classes).
    public static T? FindById<T>(this IEnumerable<T> source, int id) where T : IHasId
    {
        ArgumentNullException.ThrowIfNull(source);

        foreach (var item in source)
        {
            if (item.Id == id)
                return item;
        }
        return default;
    }

    // R3 - id -> item lookup that callers cannot modify.
    public static IReadOnlyDictionary<int, T> ToIdDictionary<T>(this IEnumerable<T> source) where T : IHasId
    {
        ArgumentNullException.ThrowIfNull(source);

        var dictionary = new Dictionary<int, T>();
        foreach (var item in source)
        {
            if (!dictionary.TryAdd(item.Id, item))
                throw new ArgumentException($"Duplicate Id {item.Id} found while building the dictionary.",
                    nameof(source));
        }
        return new ReadOnlyDictionary<int, T>(dictionary);
    }
}
