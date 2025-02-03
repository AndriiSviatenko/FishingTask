using _project.Scripts.Fish.Movement;
using _project.Scripts.StateMachines.Transitions.Core;

namespace _project.Scripts.StateMachines.Transitions.Implementantion.Fish.States
{
    public class Move : IEnterState, IUpdateState, IExitState
    {
        private readonly Movement _movement;
        
        public Move(Movement movement) => 
            _movement = movement;

        public void Enter() => 
            _movement.StartMove();
        public void Update() => 
            _movement.Move();
        public void Exit() => 
            _movement.StopMove();
    }
}