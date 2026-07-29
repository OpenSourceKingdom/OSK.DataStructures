using System;
using System.Collections.Generic;

namespace OSK.DataStructures;

public class CollectionNavigator<T>: ICollectionNavigator<T>
{
    #region Variables

    private readonly IReadOnlyList<T> _items;

    #endregion

    #region Constructors

    public CollectionNavigator(IEnumerable<T> items)
        : this(items, false)
    {
    }

    public CollectionNavigator(IEnumerable<T> items, bool wrapNavigation)
    {
        _items = items is null ? throw new ArgumentNullException(nameof(items)) : [.. items];
        WrapNavigation = wrapNavigation;
    }

    #endregion

    #region ICollectionNavigator

    public bool WrapNavigation { get; }

    public int Count => _items.Count;

    public T Current => _items[CurrentIndex];

    public int CurrentIndex { get; private set; }

    public bool HasNext => WrapNavigation || CurrentIndex < _items.Count;

    public bool HasPrevious => WrapNavigation || CurrentIndex > 0;

    public bool Next()
    {
        if (!WrapNavigation && CurrentIndex >= Count)
        {
            return false;
        }

        CurrentIndex++;
        if (CurrentIndex >= Count)
        {
            CurrentIndex = 0;
        }

        return true;
    }

    public bool Previous()
    {
        if (!WrapNavigation && CurrentIndex is 0)
        {
            return false;
        }

        CurrentIndex--;
        if (CurrentIndex < 0)
        {
            CurrentIndex = _items.Count - 1;
        }

        return true;
    }

    public bool TryNavigate(int index)
    {
        if (index < 0 || index >= _items.Count)
        {
            return false;
        }

        CurrentIndex = index;
        return true;
    }

    #endregion
}