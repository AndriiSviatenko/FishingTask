using UnityEngine;

namespace _project.Scripts.Patterns.Factory.Core
{
    public class GenericFactory<T> where T : MonoBehaviour
    {
        private T Prefab { get; set; }

        protected GenericFactory(T prefab) => 
            Prefab = prefab;
        public void ChangePrefab(T prefab) => 
            Prefab = prefab;

        public T GetNewInstance() =>
            Object.Instantiate(Prefab);
        
        public T GetNewInstance(Vector3 position, Quaternion rotation, Transform parent) =>
            Object.Instantiate(Prefab, position,rotation,parent);
    }
}
