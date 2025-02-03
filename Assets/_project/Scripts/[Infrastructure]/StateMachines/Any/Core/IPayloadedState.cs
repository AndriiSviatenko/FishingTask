namespace _project.Scripts.StateMachines.Any.Core
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}