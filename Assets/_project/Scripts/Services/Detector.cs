using System;
using UnityEngine;

namespace _project.Scripts.Services
{
    public class Detector : MonoBehaviour
    {
        public event Action<Collider> OnEnter;
        public event Action<Collider> OnStay;
        public event Action<Collider> OnExit;
        private void OnTriggerEnter(Collider other)
        {
            OnEnter?.Invoke(other);
        }
        private void OnTriggerStay(Collider other)
        {
            OnStay?.Invoke(other);
        }
        private void OnTriggerExit(Collider other)
        {
            OnStay?.Invoke(other);
        }
        public void Enable()
        {
            gameObject.SetActive(true);
        }
        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}