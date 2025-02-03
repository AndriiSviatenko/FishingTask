namespace _project.Scripts.StateMachines.Any.Core
{  
    public interface IState : IExitableState
    {
        void Enter();
    }
}