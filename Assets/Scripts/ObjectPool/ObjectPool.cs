using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        private readonly List<T> _activePool = new();
        private readonly Queue<T> _passivePool = new();
        [SerializeField] protected T objectPrefab;
        [SerializeField] private Transform objectContainer;

        private int _defaultCapacity;
        
        public List<T> ActivePool => _activePool;

        protected void SetPool(T objectPrefab, int defaultCapacity)
        {
            this.objectPrefab = objectPrefab;
            _defaultCapacity = defaultCapacity;

            for(int i = 0; i < _defaultCapacity; i++)
                    CreateAnInstance();
        }
        
        private void CreateAnInstance()
        {
            T instance = Instantiate(objectPrefab, objectContainer);
            instance.gameObject.SetActive(false);
            _passivePool.Enqueue(instance);
        }

        protected T GetItem()
        {
            if (_passivePool.Count == 0)
                CreateAnInstance();
            
            T item = _passivePool.Dequeue();

            _activePool.Add(item);
            
            return item;
        }

        public void ReleaseItem(T item)
        {
            _activePool.Remove(item);
            _passivePool.Enqueue(item);
        }
    }
}