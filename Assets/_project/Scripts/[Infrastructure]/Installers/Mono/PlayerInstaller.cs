using _project.Scripts.Services;
using UnityEngine;
using Zenject;
using Config = _project.Scripts.Player.Config.Config;

namespace _project.Scripts.Installers.Mono
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private FishingRod.Config.Config fishingRodConfig;
        [SerializeField] private Config config;
        [SerializeField] private Bobber bobberPrefab;
        [SerializeField] private Transform spawnPoint;
        public override void InstallBindings()
        {
            BindPlayer();
            BindFishingRod();
            BindBobber();
            BindDetectorDistance();
            BindSpawnPoint();
            BindConfig();
            BindFishingRodConfig();
        }
        private void BindPlayer()
        {
            Container
                .BindInterfacesAndSelfTo<Player.Controller>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
        private void BindFishingRodConfig()
        {
            Container
                .Bind<FishingRod.Config.Config>()
                .FromScriptableObject(fishingRodConfig)
                .AsSingle();
        }
        private void BindConfig()
        {
            Container
                .Bind<Config>()
                .FromScriptableObject(config)
                .AsSingle();
        }
        private void BindSpawnPoint()
        {
            Container
                .Bind<Transform>()
                .FromInstance(spawnPoint)
                .AsSingle();
        }
        private void BindDetectorDistance()
        {
            Container
                .Bind<DetectorDistance>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
        private void BindBobber()
        {
            Container
                .Bind<Bobber>()
                .FromInstance(bobberPrefab)
                .AsSingle();
        }
        private void BindFishingRod()
        {
            Container
                .BindInterfacesAndSelfTo<FishingRod.FishingRod>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
    }
}