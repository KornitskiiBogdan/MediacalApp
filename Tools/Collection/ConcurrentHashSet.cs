using System.Collections;
using System.Collections.Concurrent;

namespace Tools.Collection
{
    public sealed class ConcurrentHashSet<T> : IEnumerable<T>, ICollection<T> where T : notnull
    {
        public ConcurrentHashSet(IEqualityComparer<T> comparer)
        {
            _dict = new(comparer);
        }

        public ConcurrentHashSet()
        {
            _dict = new();
        }

        public ConcurrentHashSet(IEnumerable<T> source, IEqualityComparer<T> comparer)
        {
            _dict = new(source.Select(el => KeyValuePair.Create(el, default(ValueTuple))), comparer);
        }

        private readonly ConcurrentDictionary<T, ValueTuple> _dict;

        public int Count => _dict.Count;

        public bool IsEmpty => _dict.IsEmpty;

        public bool Add(T item) => _dict.TryAdd(item, default);


        public void Clear() => _dict.Clear();

        public bool Contains(T item) => _dict.ContainsKey(item);

        public void CopyTo(T[] array, int arrayIndex) => _dict.Keys.CopyTo(array, arrayIndex);

        public bool Remove(T item) => _dict.TryRemove(item, out _);

        public IEnumerator<T> GetEnumerator() => _dict.Keys.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        bool ICollection<T>.IsReadOnly => false;

        void ICollection<T>.Add(T item) => Add(item);
    }
}
