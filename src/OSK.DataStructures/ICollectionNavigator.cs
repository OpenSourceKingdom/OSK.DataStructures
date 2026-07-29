namespace OSK.DataStructures;

/// <summary>
/// A navigator over a collection of items
/// </summary>
/// <typeparam name="T">The data type the collection contains</typeparam>
public interface ICollectionNavigator<T>
{
    /// <summary>
    /// Describes that navigator behavior if an end of the collection has been reached (i.e. start or finish). If set, the navigator will wrap around to the other end of the collection
    /// </summary>
    bool WrapNavigation { get; }
    
    /// <summary>
    /// The total number of items in the collection
    /// </summary>
    int Count { get; }

    /// <summary>
    /// The current item
    /// </summary>
    T? Current { get; }

    /// <summary>
    /// The current index for the item
    /// </summary>
    int CurrentIndex { get; }

    /// <summary>
    /// Describes if the current item has a next item to navigate to. If false, the collection has reached the end of navigation.
    /// </summary>
    public bool HasNext { get; }

    /// <summary>
    /// Describes if the current has a previous item to navigate to. If false, the collection has reached the start of navigation
    /// </summary>
    public bool HasPrevious { get; }

    /// <summary>
    /// Moves the navigation to the next item in the collection
    /// </summary>
    /// <remarks>
    /// 💡Notes:
    /// <list type="bullet">
    /// <item>Hitting the end of the collection should not throw an error, it's intended that unless wrapping is enabled that hitting next at the end of navigation will do nothing</item>
    /// </list>
    /// </remarks>
    /// <returns>Whether navigation was successfully completed</returns>
    bool Next();

    /// <summary>
    /// Moves the navigation to the previous item in the collection
    /// </summary>
    /// <remarks>
    /// 💡Notes:
    /// <list type="bullet">
    /// <item>Hitting the start of the collection should not throw an error, it's intended that unless wrapping is enabled that hitting previous at the start of navigation will do nothing</item>
    /// </list>
    /// </remarks>
    /// <returns>Whether navigation was successfully completed</returns>>
    bool Previous();

    /// <summary>
    /// Attempts to move the navigation to a specific index in the collection
    /// </summary>
    /// <remarks>
    /// 💡Notes:
    /// <list type="bullet">
    /// <item>Navigating to an invalid index should not cause an error, if navigation can not be performed then it will not be moved</item>
    /// </list>
    /// </remarks>
    /// <param name="index">The index to navigate to</param>
    /// <returns>Whether navigation was successfully completed</returns>
    bool TryNavigate(int index);
}
