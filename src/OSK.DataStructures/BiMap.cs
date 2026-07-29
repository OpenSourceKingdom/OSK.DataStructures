using System.Collections;
using System.Collections.Generic;

namespace OSK.DataStructures;

/// <inheritdoc/>
public class BiMap<TLeft, TRight>: IBiMap<TLeft, TRight>
{
    #region Variables

    private readonly Dictionary<TLeft, TRight> _forwardLookup = [];
    private readonly Dictionary<TRight, TLeft> _reverseLookup = [];

    #endregion

    #region Constructors

    /// <summary>
    /// Creates an empty <see cref="BiMap{TLeft, TRight}"/>
    /// </summary>
    public BiMap()
    {
    }

    /// <summary>
    /// Creates a <see cref="BiMap{TLeft, TRight}"/> using the map entries provided
    /// </summary>
    /// <param name="entries">The map entries to include in this map</param>
    public BiMap(IEnumerable<MapEntry<TLeft, TRight>> entries)
    {
        AddRangeBiMap(entries);
    }

    /// <summary>
    /// Creates a <see cref="BiMap{TLeft, TRight}"/> using the map entries provided
    /// </summary>
    /// <param name="entries">The map entries to include in this map</param>
    public BiMap(IEnumerable<MapEntry<TRight, TLeft>> entries)
    {
        AddRangeBiMap(entries);
    }

    #endregion

    #region Map

    /// <inheritdoc/>
    public int Count => _forwardLookup.Count;

    /// <inheritdoc/>
    public IEnumerable<TLeft> Left => _forwardLookup.Keys;

    /// <inheritdoc/>
    public IEnumerable<TRight> Right => _forwardLookup.Values;

    /// <inheritdoc/>
    public TLeft this[TRight key]
    {
        get => _reverseLookup[key];
        set
        {
            AddMapEntry(value, key);
        }
    }

    /// <inheritdoc/>
    public TRight this[TLeft key]
    {
        get => _forwardLookup[key];
        set
        {
            AddMapEntry(key, value);
        }
    }

    /// <inheritdoc/>
    public void Add(TLeft key, TRight value)
    {
        AddMapEntry(key, value);
    }

    /// <inheritdoc/>
    public void Add(TRight key, TLeft value)
    {
        AddMapEntry(value, key);
    }

    /// <inheritdoc/>
    public bool Remove(TLeft key)
    {
        if (_forwardLookup.TryGetValue(key, out var u))
        {
            var removed = _forwardLookup.Remove(key);
            removed = _reverseLookup.Remove(u);

            return true;
        }

        return false;
    }

    /// <inheritdoc/>
    public bool Remove(TRight key)
    {
        if (_reverseLookup.TryGetValue(key, out var t))
        {
            _reverseLookup.Remove(key);
            _forwardLookup.Remove(t);

            return true;
        }

        return false;
    }

    /// <inheritdoc/>
    public bool TryGetEntry(TLeft left, out TRight? right)
        => _forwardLookup.TryGetValue(left, out right);

    /// <inheritdoc/>
    public bool TryGetValue(TRight right, out TLeft? left)
        => _reverseLookup.TryGetValue(right, out left);

    /// <inheritdoc/>
    public bool Contains(TLeft key)
        => _forwardLookup.ContainsKey(key);

    /// <inheritdoc/>
    public bool Contains(TRight key)
        => _reverseLookup.ContainsKey(key);

    /// <inheritdoc/>
    public void Clear()
    {
        _forwardLookup.Clear();
        _reverseLookup.Clear();
    }

    #endregion

    #region IEnumerable

    public IEnumerator<MapEntry<TLeft, TRight>> GetEnumerator()
    {
        foreach (var pair in _forwardLookup)
        {
            yield return new(pair.Key, pair.Value);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    #endregion

    #region Helpers

    private void AddMapEntry(TLeft item1, TRight item2)
    {
        _forwardLookup.Add(item1, item2);
        _reverseLookup.Add(item2, item1);
    }

    private void AddRangeBiMap(IEnumerable<MapEntry<TLeft, TRight>> maps)
    {
        foreach (var map in maps)
        {
            AddMapEntry(map.Left, map.Right);
        }
    }

    private void AddRangeBiMap(IEnumerable<MapEntry<TRight, TLeft>> maps)
    {
        foreach (var map in maps)
        {
            AddMapEntry(map.Right, map.Left);
        }
    }

    #endregion
}
