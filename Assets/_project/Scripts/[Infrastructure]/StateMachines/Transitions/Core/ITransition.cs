using System;

namespace _project.Scripts.StateMachines.Transitions.Core
{
    public interface ITransition
    {
        Type To { get; }
        bool CanTransition(IState from);
    }
}
