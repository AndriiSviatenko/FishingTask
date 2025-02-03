using _project.Scripts.UI;
using Zenject;

namespace _project.Scripts.Installers.Mono
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindUIController();
        }
        private void BindUIController()
        {
            Container
                .BindInterfacesAndSelfTo<Controller>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
    }
}