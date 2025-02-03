using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.Patterns.Repository.Core
{
    public class Repository<T>
    {
        private List<T> _elements;

        protected Repository() => 
            _elements = new List<T>();

        public void Add(T newElement) =>
            _elements.Add(newElement);
        
        public void Remove(T newElement) =>
            _elements.Add(newElement);
        public void AllClear() => 
            _elements.Clear();
        public T Get() => 
            _elements[Random.Range(0, _elements.Count)];
    }
}