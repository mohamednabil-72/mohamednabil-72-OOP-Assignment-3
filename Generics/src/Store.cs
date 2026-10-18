namespace GenericsApp;

public class Store<T> where T : IHasId
{
    // Keyed by Id, so GetById is a hash lookup instead of a loop.
    private readonly Dictionary<int, T> _items = new();

    public void Add(T item)
    {
        ArgumentNullException.ThrowIfNull(item);

        if (_items.ContainsKey(item.Id))
            throw new ArgumentException(
                $"An item with Id {item.Id} already exists in the store.", nameof(item));

        _items.Add(item.Id, item);
    }

    public T? GetById(int id)
    {
        return _items.TryGetValue(id, out var item) ? item : default;
    }

    // IReadOnlyCollection<T> has no Add/Remove, so callers cannot change the store through it.
    public IReadOnlyCollection<T> GetAll()
    {
        return _items.Values;
    }

    public void Remove(int id)
    {
        _items.Remove(id);
    }
}
