using _project.Scripts.Patterns.BootStrap;
using _project.Scripts.Services;
using _project.Scripts.Services.Storage;
using Zenject;
using Controller = _project.Scripts.Services.VFX.Controller;

namespace _project.Scripts.Installers.Mono
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindBootStrap();
            BindStorageService();
            BindWallet();
            BindVFX();
            BindAudio();
            BindFishSpawner();
            BindSetuperFish();
        }
        private void BindBootStrap()
        {
            Container
                .BindInterfacesTo<BootStrap>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
        private void BindStorageService()
        {
            Container
                .Bind<IStorageService>()
                .To<JsonToFileStorageService>()
                .AsSingle();
        }
        private void BindWallet()
        {
            Container
                .Bind<Wallet.Wallet>()
                .AsSingle();
        }
        private void BindVFX()
        {
            Container
                .BindInterfacesAndSelfTo<Controller>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
        private void BindAudio()
        {
            Container
                .BindInterfacesAndSelfTo<Services.Audio.Controller>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
        private void BindFishSpawner()
        {
            Container
                .Bind<FishSpawner>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
        private void BindSetuperFish()
        {
            Container
                .BindInterfacesAndSelfTo<SetuperFish>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
    }
}