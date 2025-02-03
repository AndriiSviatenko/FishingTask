using _project.Scripts.FishingRod;
using _project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _project.Scripts.Player
{
    public class Controller : MonoBehaviour
    {
        private FishingRod.FishingRod _fishingRod;
        public FishingRod.FishingRod FishingRod => _fishingRod;

        [Inject]
        private void Construct(FishingRod.FishingRod fishingRod, 
            Bobber bobber, 
            DetectorDistance detectorDistance, 
            Transform spawnPoint,
            Config.Config config,
            FishingRod.Config.Config fishingRodConfig, 
            VRSwipe swipe, 
            VRReeling vrReeling)
        {
            _fishingRod = fishingRod;
            fishingRod.Init(bobber, detectorDistance, spawnPoint, fishingRodConfig, swipe, vrReeling);
        }

        public void StartFishing() => 
            _fishingRod.EnableGetSwipe();

        public void StopFishing() => 
            _fishingRod.StopFishing();
    }
}