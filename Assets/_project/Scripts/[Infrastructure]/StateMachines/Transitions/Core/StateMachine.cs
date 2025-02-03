using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace _project.Scripts.StateMachines.Transitions.Core
{
    public class StateMachine : ITickable
    {
        private IState _current;
        private readonly IState[] _states;
        private readonly ITransition[] _transitions;

        public StateMachine(IState[] states, ITransition[] transitions)
        {
            _current = states[0];
            _states = states;
            _transitions = transitions;

            if(_current is IEnterState enterState) 
                enterState.Enter();
        }

        public void Tick()
        {
            foreach (var transition in _transitions)
            {
                if (transition.CanTransition(_current)) 
                    Translate(transition);
            }

            if (_current is IUpdateState updateState) 
                updateState.Update();
        }

        private void Translate(ITransition transition)
        {
            if (_current is IExitState exitState)
                exitState.Exit();

            if (_states == null || !_states.Any())
                throw new InvalidOperationException("States collection is null or empty.");

            _current = _states.FirstOrDefault(x => x.GetType() == transition.To);

            if (_current == null)
                throw new InvalidOperationException($"State of type {transition.To} not found in states collection.");

            if (_current is IEnterState enterState)
                enterState.Enter();
        }
    }
}
