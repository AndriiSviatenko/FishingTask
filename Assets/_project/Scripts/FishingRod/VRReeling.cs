using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _project.Scripts.FishingRod
{
    public class VRReeling
    {
        public event Action ReelReadyEvent;
        public event Action ReelCancelEvent;
        
        private readonly XRGrabInteractable _reelObject;
        private readonly float _speed;
        
        private Transform _bobber;

        public VRReeling(XRGrabInteractable reelObject, float speed)
        {
            _speed = speed;
            _reelObject = reelObject;
            _reelObject.selectEntered.AddListener(OnReelGrabbed);
            _reelObject.selectExited.AddListener(OnReelReleased);
        }
        private void OnReelReleased(SelectExitEventArgs arg0) => 
            ReelCancelEvent?.Invoke();
        private void OnReelGrabbed(SelectEnterEventArgs arg0) => 
            ReelReadyEvent?.Invoke();

        public void SetBobber(Transform bobber) => 
            _bobber = bobber;
        
        public void Reeling(Transform value)
        {
            var direction = value.position - _bobber.transform.position;
            _bobber.transform.Translate(direction * (_speed * Time.deltaTime));
        }
    }
}