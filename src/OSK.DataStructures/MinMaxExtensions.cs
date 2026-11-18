using System;

namespace OSK.DataStructures;

public static class MinMaxExtensions
{
    extension<T>(MinMax<T> minMax) 
        where T : struct, IComparable<T>
    {
        /// <summary>
        /// Clamps the value to be within the min max
        /// </summary>
        /// <param name="value">The value to clamp</param>
        /// <returns>The clamped value</returns>
        public T Clamp(T value) =>
            value.CompareTo(minMax.Min) < 0 ? minMax.Min
            : value.CompareTo(minMax.Max) > 0 ? minMax.Max
            : value;

        /// <summary>
        /// Checks if the value is within Min/Max 
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <returns>Whether the value is in the Min/Max</returns>
        public bool Contains(T value) =>
            value.CompareTo(minMax.Min) >= 0 && value.CompareTo(minMax.Max) <= 0;
    }
}
