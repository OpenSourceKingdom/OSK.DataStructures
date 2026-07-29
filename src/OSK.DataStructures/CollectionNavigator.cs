using System.Collections.Generic;

namespace OSK.DataStructures;

public class CollectionNavigator<T>: ICollectionNavigator<T>
{
    #region Variables

    private readonly IReadOnlyList<T> _items;
    private int _currentIndex;

    #endregion

    #region Constructors

    #endregion

    #region ICollectionNavigator

    public bool WrapNavigation { get; }

    public int Count => _items.Count;

    public T Current => _items[_currentIndex];

    public int CurrentIndex => throw new System.NotImplementedException();

    public bool HasNext => throw new System.NotImplementedException();

    public bool HasPrevious => throw new System.NotImplementedException();

    public bool Next()
    {
        throw new System.NotImplementedException();
    }

    public bool Previous()
    {
        throw new System.NotImplementedException();
    }

    public bool TryNavigate(int index)
    {
        throw new System.NotImplementedException();
    }

    #endregion
}