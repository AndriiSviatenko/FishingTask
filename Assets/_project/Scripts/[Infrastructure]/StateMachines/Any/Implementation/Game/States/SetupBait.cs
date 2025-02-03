
using _project.Scripts.StateMachines.Any.Core;
using _project.Scripts.UI;

namespace _project.Scripts.StateMachines.Any.Implementation.Game.States
{
    public class SetupBait : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly Controller _controller;
        public SetupBait(StateMachine stateMachine, Controller controller)
        {
            _stateMachine = stateMachine;
            _controller = controller;
        }
        public void Enter()
        {
            _controller.ShowBaitSetuper();
            _controller.BaitSetupedEvent += OnBaitSetuped;
        }
        public void Exit() => 
            _controller.BaitSetupedEvent -= OnBaitSetuped;
        private void OnBaitSetuped() => 
            _stateMachine.Enter<Fishing>();
    }
}