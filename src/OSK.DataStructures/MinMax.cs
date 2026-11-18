using System;
using System.Collections.Generic;

namespace OSK.DataStructures;

public readonly struct MinMax<T> : IEquatable<MinMax<T>> 
    where T : struct, IComparable<T>
{
    #region Variables

    /// <summary>
    /// The minimum value
    /// </summary>
    public T Min { get; }

    /// <summary>
    /// The maximum value
    /// </summary>
    public T Max { get; }

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a MinMax that uses the same value for the range
    /// </summary>
    /// <param name="value">The value to use as both Min/Max</param>
    public MinMax(T value)
        : this(value, value)
    {
    }

    /// <summary>
    /// Creates a MinMax with values for both
    /// </summary>
    /// <param name="min">The minimum value</param>
    /// <param name="max">The maximum value</param>
    /// <exception cref="ArgumentException">If min is not less than or equal to max</exception>
    public MinMax(T min, T max)
    {
        if (max.CompareTo(min) < 0)
        {
            throw new ArgumentException($"Max ({max}) must be >= Min ({min}).");
        }

        Min = min;
        Max = max;
    }

    #endregion

    #region IEquatable

   public bool Equals(MinMax<T> other) 
        => EqualityComparer<T>.Default.Equals(Min, other.Min) && EqualityComparer<T>.Default.Equals(Max, other.Max);

    public override bool Equals(object obj) 
        => obj is MinMax<T> m && Equals(m);
    public override int GetHashCode() 
        => HashCode.Combine(Min, Max);

    public static bool operator ==(MinMax<T> left, MinMax<T> right) => left.Equals(right);
    public static bool operator !=(MinMax<T> left, MinMax<T> right) => !left.Equals(right);

    #endregion
}
