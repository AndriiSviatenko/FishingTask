using System;
using _project.Scripts.Services;
using UnityEngine;
using WaterWorks.Scripts;

namespace _project.Scripts
{
    public class Bobber : MonoBehaviour
    {
        public event Action ReachedWaterEvent;
        public event Action<Fish.Fish> BitEvent;
        public event Action BaitEvent;
        
        [SerializeField] private Detector detector;
        
        public void Init()
        {
            detector.Enable();
            detector.OnEnter += CheckWater;
        }
        public void Off()
        {
            detector.Disable();
            detector.OnEnter -= CheckWater;
        }
        private void CheckWater(Collider obj)
        {
            if (obj.TryGetComponent(out WaterSettings water)) 
                ReachedWaterEvent?.Invoke();
        }
        public void Bit(Fish.Fish fish) => 
            BitEvent?.Invoke(fish);

        public void Bait() => 
            BaitEvent?.Invoke();
    }
}