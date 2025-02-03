using System;
using System.Collections.Generic;

namespace _project.Scripts.Patterns.Pool.Core
{
    public abstract class Pool<T> where T : IPoolable<T>
    {
        public event Action GetNewSpawnedEvent;
        private float _currentCount;
        private Queue<T> _instances = new();

        public void AddInstance(T instance)
        {
            _instances.Enqueue(instance);
            _currentCount++;
        }

        public T Get()
        {
            if (_instances.Count > 0)
            {
                var instance = _instances.Dequeue();
                instance.ReturnInPoolEvent += ReturnInPool;
                _currentCount--;
                return instance;
            }
            GetNewSpawnedEvent?.Invoke();
            throw new InvalidOperationException(nameof(T));
        }

        private void ReturnInPool(T instance)
        {
            instance.Stop();
            instance.ReturnInPoolEvent -= ReturnInPool;
            _instances.Enqueue(instance);
            _currentCount++;
        }
    }
}