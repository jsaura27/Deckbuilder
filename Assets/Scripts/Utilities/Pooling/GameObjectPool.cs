using System.Collections.Generic;
#if UNITY_ENGINE || UNITY_2020_1_OR_NEWER
using UnityEngine;
#endif

namespace Deckbuilder.Utilities.Pooling
{
#if UNITY_ENGINE || UNITY_2020_1_OR_NEWER
    public class GameObjectPool
    {
        private readonly GameObject _prefab;
        private readonly Stack<GameObject> _pool = new Stack<GameObject>();

        public GameObjectPool(GameObject prefab, int initial = 0)
        {
            _prefab = prefab;
            for (int i = 0; i < initial; i++)
            {
                var go = Object.Instantiate(prefab);
                go.SetActive(false);
                _pool.Push(go);
            }
        }

        public GameObject Get()
        {
            if (_pool.Count > 0)
            {
                var go = _pool.Pop();
                go.SetActive(true);
                return go;
            }

            return Object.Instantiate(_prefab);
        }

        public void Release(GameObject go)
        {
            go.SetActive(false);
            _pool.Push(go);
        }
    }
#else
    // Non-Unity fallback stub to keep compilation outside Unity simple.
    public class GameObjectPool { }
#endif
}
