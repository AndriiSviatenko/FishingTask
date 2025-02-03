using _project.Scripts.Patterns.Factory.StateMachine.Any;
using Zenject;

namespace _project.Scripts.Installers.Mono
{
    public class StateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactory();
        }
        private void BindFactory()
        {
            Container
                .Bind<StateMachineFactory>()
                .AsSingle();
        }
    }
}