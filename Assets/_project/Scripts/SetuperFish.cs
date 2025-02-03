using _project.Scripts.Player;
using _project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _project.Scripts
{
    public class SetuperFish : MonoBehaviour, ITickable
    {
        private const float CHANGE_BIT_FISH = 0.7f;
        private Controller _player;
        private FishSpawner _fishSpawner;
        private bool _isActive;

        [Inject]
        private void Construct(FishSpawner fishSpawner, Controller player)
        {
            _fishSpawner = fishSpawner;
            _player = player;
        }

        public void Init() => 
            _isActive = true;

        public void Off() => 
            _isActive = false;

        public void Tick()
        {
            if(_isActive == false) return;
            
            if (Random.value > CHANGE_BIT_FISH)
            {
                var fish = _fishSpawner.FishRepository.Get();
                fish.SetBobber(_player.FishingRod.Bobber.transform.position);
                _isActive = false;
                fish.ReturnInWaterEvent += () => _isActive = true;
            }
        }
    }
}