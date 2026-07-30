namespace OSK.DataStructures.Events;

/// <summary>
/// An that is raised when collection navigation is performed
/// </summary>
/// <typeparam name="T"></typeparam>
public class CollectionNavigationEvent<T>
{
    /// <summary>
    /// The current value the collection is at after navigation
    /// </summary>
    public required T Current { get; init; }

    /// <summary>
    /// The current index after navigation
    /// </summary>
    public required int CurrentIndex { get; init; }

    /// <summary>
    /// The total items in the navigated collection
    /// </summary>
    public required int TotalItems { get; init; }
}
