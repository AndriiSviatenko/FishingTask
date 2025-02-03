using _project.Scripts.Patterns.Factory.StateMachine.Any;
using _project.Scripts.StateMachines.Any.Core;
using UnityEngine;
using Zenject;

namespace _project.Scripts.Patterns.BootStrap
{
    public class BootStrap : MonoBehaviour, IInitializable
    {
        private StateMachine _stateMachine;
        private StateMachineFactory _stateMachineFactory;

        [Inject]
        private void Construct(StateMachineFactory stateMachineFactory) => 
            _stateMachineFactory = stateMachineFactory;
        
        public void Initialize()
        {
            _stateMachine = _stateMachineFactory.Create();
            _stateMachine.Enter<StateMachines.Any.Implementation.Game.States.BootStrap>();
        }
    }
}