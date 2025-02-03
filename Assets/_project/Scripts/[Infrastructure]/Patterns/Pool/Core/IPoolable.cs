using System;

namespace _project.Scripts.Patterns.Pool.Core
{
    public interface IPoolable<T>
    {
        public event Action<T> ReturnInPoolEvent;
        public void Play();
        public void ReturnInPool();
        public void Stop();
    }
}