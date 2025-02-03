using System;
using UnityEngine;
using Random = System.Random;

namespace _project.Scripts.Fish.Interaction
{
    public class Interaction : MonoBehaviour
    {
        public event Action BitedEvent;
        public event Action BaitEvent;
        
        [SerializeField] private float chanceBiting;
        private Random _rnd = new ();

        public void Interact()
        {
            if (_rnd.NextDouble() < chanceBiting)
                BitedEvent?.Invoke();
            
            else
                BaitEvent?.Invoke();
        }
    }
}