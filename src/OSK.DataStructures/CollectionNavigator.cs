using OSK.DataStructures.Events;
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

    /// <inheritdoc/>
    public event Action<CollectionNavigationEvent<T>>? Navigated;

    /// <inheritdoc/>
    public bool WrapNavigation { get; }

    /// <inheritdoc/>
    public int Count => _items.Count;

    /// <inheritdoc/>
    public T? Current => Count is 0 ? default : _items[CurrentIndex];

    /// <inheritdoc/>
    public int CurrentIndex { get; private set; }

    /// <inheritdoc/>
    public bool HasNext => WrapNavigation || CurrentIndex < _items.Count - 1;

    /// <inheritdoc/>
    public bool HasPrevious => WrapNavigation || CurrentIndex > 0;

    /// <inheritdoc/>
    public bool Next()
    {
        if (!WrapNavigation && CurrentIndex >= Count - 1)
        {
            return false;
        }

        CurrentIndex = CurrentIndex >= Count - 1
            ? WrapNavigation ? 0 : CurrentIndex
            : CurrentIndex + 1;

        TryPublishNavigationEvent();

        return true;
    }

    /// <inheritdoc/>
    public bool Previous()
    {
        if (!WrapNavigation && CurrentIndex <= 0)
        {
            return false;
        }

        CurrentIndex = CurrentIndex <= 0
            ? WrapNavigation ? Count - 1 : 0
            : CurrentIndex - 1;

        TryPublishNavigationEvent();

        return true;
    }

    /// <inheritdoc/>
    public bool TryNavigate(int index)
    {
        if (index < 0 || index >= _items.Count)
        {
            return false;
        }

        CurrentIndex = index;

        TryPublishNavigationEvent();

        return true;
    }

    #endregion

    #region Helpers

    private void TryPublishNavigationEvent()
    {
        Navigated?.Invoke(new()
        {
            Current = Current!,
            CurrentIndex = CurrentIndex,
            TotalItems = Count
        });
    }

    #endregion
}