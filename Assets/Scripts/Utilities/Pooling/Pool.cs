#nullable enable
using System;
using System.Collections.Generic;

namespace Deckbuilder.Utilities.Pooling
{
    /// <summary>
    /// Simple generic pool for plain objects. Intended as a minimal utility to reduce allocations.
    /// Not Unity-specific; safe to instantiate in editor or runtime.
    /// </summary>
    public class Pool<T> where T : class, new()
    {
        private readonly Stack<T> _stack = new Stack<T>();
        private readonly Action<T>? _onGet;
        private readonly Action<T>? _onRelease;

        public Pool(Action<T>? onGet = null, Action<T>? onRelease = null, int initialCapacity = 0)
        {
            _onGet = onGet;
            _onRelease = onRelease;
            for (int i = 0; i < initialCapacity; i++) _stack.Push(new T());
        }

        public T Get()
        {
            var item = _stack.Count > 0 ? _stack.Pop() : new T();
            _onGet?.Invoke(item);
            return item;
        }

        public void Release(T item)
        {
            _onRelease?.Invoke(item);
            _stack.Push(item);
        }

        public int Count => _stack.Count;
    }
}
