using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OSK.DataStructures.Common
{
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

        public T this[U key]
        {
            get => _reverseLookup[key];
            set
            {
                AddTwoWay(value, key);
            }
        }

        public U this[T key]
        {
            get => _forwardLookup[key];
            set
            {
                AddTwoWay(key, value);
            }
        }

        public void Add(T key, U value)
        {
            AddTwoWay(key, value);
        }

        public void Add(U key, T value)
        {
            AddTwoWay(value, key);
        }

        public bool Remove(T key)
        {
            if (_forwardLookup.TryGetValue(key, out var u))
            {
                _forwardLookup.Remove(key);
                _reverseLookup.Remove(u);

                return true;
            }

            return false;
        }

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

        public bool TryGetValue(T key, out U value)
            => _forwardLookup.TryGetValue(key, out value);

        public bool TryGetValue(U key, out T value)
            => _reverseLookup.TryGetValue(key, out value);

        public bool Contains(T key)
            => _forwardLookup.ContainsKey(key);

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
