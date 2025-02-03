using _project.Scripts.StateMachines.Any.Core;
using _project.Scripts.UI;

namespace _project.Scripts.StateMachines.Any.Implementation.Game.States
{
    public class BootStrap : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly Controller _uiController;
        public BootStrap(StateMachine stateMachine, Controller uiController)
        {
            _stateMachine = stateMachine;
            _uiController = uiController;
        }
        public void Enter()
        {
            _uiController.StartGameEvent += () => _stateMachine.Enter<Start>();
            _uiController.Init();
        }
        public void Exit()
        {
            
        }
        private void EnterStart() => 
            _stateMachine.Enter<Start>();
    }
}