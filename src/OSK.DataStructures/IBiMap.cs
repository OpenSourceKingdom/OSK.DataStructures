using System.Collections.Generic;

namespace OSK.DataStructures;

/// <summary>
/// Represents a data structure that is able to do forward and reverse looking of data
/// </summary>
/// <typeparam name="TLeft">The type of entries on the left of the map</typeparam>
/// <typeparam name="TRight">The type of entries on the right of the map</typeparam>
public interface IBiMap<TLeft, TRight>: IEnumerable<MapEntry<TLeft, TRight>>
{
    /// <summary>
    /// The total number of entries in the map
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Gets the left entries in the map
    /// </summary>
    IEnumerable<TLeft> Left { get; }

    /// <summary>
    /// Gets the right entries in the map
    /// </summary>
    IEnumerable<TRight> Right { get; }

    /// <summary>
    /// Indexes an entry with the right
    /// </summary>
    /// <param name="right">The right for looking up the left.</param>
    /// <returns>The left entry</returns>
    TLeft this[TRight right] { get; set; }

    /// <summary>
    /// Indexes an entry with the left
    /// </summary>
    /// <param name="left">The left for looking up the right.</param>
    /// <returns>The right entry</returns>
    TRight this[TLeft left] { get; set; }

    /// <summary>
    /// Adds an entry using the left first
    /// Note: this is identical to using the <see cref="Add(TRight, TLeft)"/> method.
    /// </summary>
    /// <param name="left">The left entry</param>
    /// <param name="right">The right entry</param>
    void Add(TLeft left, TRight right);

    /// <summary>
    /// Adds an entry using the right first
    /// Note: this is identical to using the <see cref="Add(TLeft, TRight)"/> method.
    /// </summary>
    /// <param name="right">The right entry</param>
    /// <param name="left">The left enetry</param>
    void Add(TRight right, TLeft left);

    /// <summary>
    /// Attempts to get the right using the left entry.
    /// </summary>
    /// <param name="left">The left to get the right.</param>
    /// <param name="right">The right.</param>
    /// <returns>Returns <see langword="true"/> if the key finds a lookup entry, <see langword="false"/> otherwise.</returns>
    public bool TryGetEntry(TLeft left, out TRight right);

    /// <summary>
    /// Attempts to get the left using the right entry.
    /// </summary>
    /// <param name="left">The right to get the left.</param>
    /// <param name="right">The left.</param>
    /// <returns>Returns <see langword="true"/> if the value finds a lookup entry, <see langword="false"/> otherwise.</returns>
    public bool TryGetValue(TRight right, out TLeft left);

    /// <summary>
    /// Removes an entry using the left.
    /// Note: This is identical to using the <see cref="Remove(TRight)"/> method.
    /// </summary>
    /// <param name="left">The left entry.</param>
    /// <returns>Whether the entry was removed. If the entry is not found, this method will return <see langword="false"/>.</returns>
    bool Remove(TLeft left);
    /// <summary>
    /// Removes an entry using the right.
    /// Note: This is identical to using the <see cref="Remove(TLeft)"/> method.
    /// </summary>
    /// <param name="right">The left entry.</param>
    /// <returns>Whether the two map was removed. If the entry is not found, this method will return <see langword="false"/>.</returns>
    bool Remove(TRight right);

    /// <summary>
    /// Checks to see if the left has a lookup entry.
    /// </summary>
    /// <param name="left">The left entry.</param>
    /// <returns>Returns <see langword="true"/> if the left has a lookup entry.</returns>
    bool Contains(TLeft left);
    /// <summary>
    /// Checks to see if the right has a lookup entry.
    /// </summary>
    /// <param name="right">The right entry.</param>
    /// <returns>Returns <see langword="true"/> if the right has a lookup entry.</returns>
    bool Contains(TRight right);

    /// <summary>
    /// Clears the map from all entries
    /// </summary>
    void Clear();
}
