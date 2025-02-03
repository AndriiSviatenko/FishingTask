using System;
using _project.Scripts.Extensions;
using _project.Scripts.Patterns.Factory.Implementation.Bobber;
using _project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _project.Scripts.FishingRod
{
    public class FishingRod : MonoBehaviour, ITickable
    {
        private const float DISTANCE_TO_CATCH_FISH = 5f;
        
        public event Action StartFishingEvent;
        public event Action ReelingEvent;
        public event Action ReelUpEvent;
        public event Action FishBitedEvent;
        public event Action CatchFishEvent;
        public event Action BobberReachedWaterEvent;
        public event Action ResetProgressEvent;
        
        private Config.Config _config;
        private DetectorDistance _detectorDistance;
        private BobberFactory _bobberFactory;
        private Bobber _bobberPrefab;
        private VRReeling _vrReeling;
        private VRSwipe _swipe;
        private Transform _spawnPoint;
        private bool _getSwipe;
        private bool _isReelingAllow;
        private bool _isVrReelingReady;

        public Bobber Bobber => _bobber;
        private Bobber _bobber;
        public Fish.Fish CurrentFish => _currentFish;
        private Fish.Fish _currentFish;

        private bool IsReelingReady =>
            _isVrReelingReady && _bobber != null;
        public void Init(Bobber bobberPrefab, 
            DetectorDistance detectorDistance, 
            Transform spawnPoint, 
            Config.Config config,
            VRSwipe swipe, 
            VRReeling vrReeling)
        {
            _config = config;
            _spawnPoint = spawnPoint;
            _bobberPrefab = bobberPrefab;
            _bobberFactory = new BobberFactory(_bobberPrefab);
            _detectorDistance = detectorDistance;
            _detectorDistance.Disable();
            _swipe = swipe;
            
            _vrReeling = vrReeling;
            _vrReeling.ReelReadyEvent += () => _isVrReelingReady = true;
            _vrReeling.ReelCancelEvent += () => _isVrReelingReady = false;
        }
        public void EnableGetSwipe() => 
            _getSwipe = true;
        
        public void Tick()
        {
            CheckResetProgress();

            if (IsReelingReady) 
                Reeling();
            
            else if (!_isVrReelingReady)
                ReelUpEvent?.Invoke();
            
            if (_getSwipe)
            {
                var _swipeValue = _swipe.GetSwipe();
                
                if (_swipeValue != Vector3.zero) 
                    StartFishing(_swipeValue);
            }
            if (_currentFish != null)
            {
                if (_currentFish.IsFishBited) 
                    CheckDistanceToCatch(_detectorDistance.IsDistanceCatch);
            }
        }
        private void Bit(Fish.Fish fish)
        {
            _currentFish = fish;
            FishBitedEvent?.Invoke();
        }
        private void CheckDistanceToCatch(bool value)
        {
            if (value) 
                CatchFish();
        }
        private void StartFishing(Vector3 _swipeValue)
        {
            CreateBobber(_swipeValue);
            InitServices();
            _getSwipe = false;
            _detectorDistance.Enable();
            StartFishingEvent?.Invoke();
        }
        private void Reeling()
        {
            _vrReeling.Reeling(transform);
            ReelingEvent?.Invoke();
        }
        private void CatchFish()
        {
            CatchFishEvent?.Invoke();
            _currentFish?.Stop();
        }
        private void InitServices()
        {
            _detectorDistance.Init(transform, _bobber.transform, DISTANCE_TO_CATCH_FISH); 
            _vrReeling.SetBobber(_bobber.transform);
        }
        private void CreateBobber(Vector3 _swipeValue)
        {
            var instance = _bobberFactory.GetNewInstance(new Vector3(_spawnPoint.position.x, _spawnPoint.position.y, _config.BaseForce  * _swipeValue.magnitude), Quaternion.identity, null);
            _bobber = instance;
            _bobber.Init();
            SubcribeBobber();
        }
        private void SubcribeBobber()
        {
            _bobber.ReachedWaterEvent += () => BobberReachedWaterEvent?.Invoke();
            _bobber.BitEvent += Bit;
        }
        private void CheckResetProgress()
        {
            if (_currentFish != null)
                if (!_currentFish.IsFishBited)
                    ResetProgressEvent?.Invoke();
        }

        public void StopFishing()
        {
            _bobber.BitEvent -= Bit;
            Destroy(_bobber.gameObject);
            _getSwipe = false;
            ResetProgressEvent?.Invoke();
            ReelUpEvent?.Invoke();
        }

    }
}