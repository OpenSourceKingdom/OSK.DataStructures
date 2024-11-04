using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OSK.DataStructures.Common
{
    /// <summary>
    /// A bidirectional mapping object that can be used similar to a dictionary
    /// </summary>
    /// <typeparam name="T">The object for the first parameter type</typeparam>
    /// <typeparam name="U">The object for the second parameter type</typeparam>
    public class TwoWayMap<T, U>: IEnumerable<KeyValuePair<T, U>>
    {
        #region Variables

        private readonly Dictionary<T, U> _forwardLookup;
        private readonly Dictionary<U, T> _reverseLookup;

        #endregion

        #region Constructors

        public TwoWayMap()
        {
            _forwardLookup = new Dictionary<T, U>();
            _reverseLookup = new Dictionary<U, T>();
        }

        public TwoWayMap(IEnumerable<KeyValuePair<T, U>> pairs)
        {
            _forwardLookup = new Dictionary<T, U>();
            _reverseLookup = new Dictionary<U, T>();

            AddRangeTwoWay(pairs);
        }

        public TwoWayMap(IEnumerable<KeyValuePair<U, T>> pairs)
        {
            _forwardLookup = new Dictionary<T, U>();
            _reverseLookup = new Dictionary<U, T>();

            AddRangeTwoWay(pairs.Select(kvp => new KeyValuePair<T, U>(kvp.Value, kvp.Key)));
        }

        public TwoWayMap(IDictionary<T, U> original)
        {
            _forwardLookup = new Dictionary<T, U>(original);
            _reverseLookup = new Dictionary<U, T>(original.Select(kvp => new KeyValuePair<U, T>(kvp.Value, kvp.Key)));
        }

        public TwoWayMap(IDictionary<U, T> original)
        {
            _forwardLookup = new Dictionary<T, U>(original.Select(kvp => new KeyValuePair<T, U>(kvp.Value, kvp.Key)));
            _reverseLookup = new Dictionary<U, T>(original);
        }

        #endregion

        #region Map

        /// <summary>
        /// Use the value object to lookup the key.
        /// </summary>
        /// <param name="key">The value object for looking up the key.</param>
        /// <returns>The key object.</returns>
        public T this[U key]
        {
            get => _reverseLookup[key];
            set
            {
                AddTwoWay(value, key);
            }
        }

        /// <summary>
        /// Use the key object to lookup the value.
        /// </summary>
        /// <param name="key">The key object for looking up the value.</param>
        /// <returns>The value object.</returns>
        public U this[T key]
        {
            get => _forwardLookup[key];
            set
            {
                AddTwoWay(key, value);
            }
        }

        /// <summary>
        /// Adds a lookup entry using the key-value method.
        /// Note: this is identical to using the <see cref="Add(U, T)"/> method.
        /// </summary>
        /// <param name="key">The key object/</param>
        /// <param name="value">The value object/</param>
        public void Add(T key, U value)
        {
            AddTwoWay(key, value);
        }

        /// <summary>
        /// Adds a lookup entry using the value-key method.
        /// Note: this is identical to using the <see cref="Add(T, U)"/> method.
        /// </summary>
        /// <param name="key">The value object/</param>
        /// <param name="value">The key object/</param>
        public void Add(U key, T value)
        {
            AddTwoWay(value, key);
        }

        /// <summary>
        /// Removes a two way lookup entry using the key object.
        /// Note: This is identical to using the <see cref="Remove(U)"/> method.
        /// </summary>
        /// <param name="key">The key object used to remove the lookup entry.</param>
        /// <returns>Whether the two map was removed. If the entry is not found, this method will return <see langword="false"/>.</returns>
        public bool Remove(T key)
        {
            if (_forwardLookup.TryGetValue(key, out var u))
            {
                var removed = _forwardLookup.Remove(key);
                removed = _reverseLookup.Remove(u);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes a two way lookup entry using the value object.
        /// Note: This is identical to using the <see cref="Remove(T)"/> method.
        /// </summary>
        /// <param name="key">The value object used to remove the lookup entry</param>
        /// <returns>Whether the two map was removed. If the entry is not found, this method will return <see langword="false"/>.</returns>
        public bool Remove(U key)
        {
            if (_reverseLookup.TryGetValue(key, out var t))
            {
                _reverseLookup.Remove(key);
                _forwardLookup.Remove(t);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Attempts to get the value object using the key object.
        /// </summary>
        /// <param name="key">The key object to get the value.</param>
        /// <param name="value">The value object.</param>
        /// <returns>Returns <see langword="true"/> if the key finds a lookup entry, <see langword="false"/> otherwise.</returns>
        public bool TryGetValue(T key, out U value)
            => _forwardLookup.TryGetValue(key, out value);

        /// <summary>
        /// Attempts to get the key object using the value object.
        /// </summary>
        /// <param name="key">The value object to get the key.</param>
        /// <param name="value">The key object.</param>
        /// <returns>Returns <see langword="true"/> if the value finds a lookup entry, <see langword="false"/> otherwise.</returns>
        public bool TryGetValue(U key, out T value)
            => _reverseLookup.TryGetValue(key, out value);

        /// <summary>
        /// Checks to see if the key object has a lookup entry.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <returns>Returns <see langword="true"/> if the key has a lookup entry.</returns>
        public bool Contains(T key)
            => _forwardLookup.ContainsKey(key);

        /// <summary>
        /// Checks to see if the value object has a lookup entry.
        /// </summary>
        /// <param name="key">The value object.</param>
        /// <returns>Returns <see langword="true"/> if the value has a lookup entry.</returns>
        public bool Contains(U key)
            => _reverseLookup.ContainsKey(key);

        #endregion

        #region IEnumerable

        public IEnumerator<KeyValuePair<T, U>> GetEnumerator()
        {
            return _forwardLookup.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Helpers
             
        private void AddTwoWay(T item1, U item2)
        {
            _forwardLookup.Add(item1, item2);
            _reverseLookup.Add(item2, item1);
        }

        private void AddRangeTwoWay(IEnumerable<KeyValuePair<T, U>> items)
        {
            foreach (var itemPair in items)
            {
                _forwardLookup[itemPair.Key] = itemPair.Value;
                _reverseLookup[itemPair.Value] = itemPair.Key;
            }
        }

        #endregion
    }
}
