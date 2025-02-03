using System;
using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.StateMachines.Any.Core
{  
  public class StateMachine
  {
    private Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;
    
    public void SetStates(Dictionary<Type, IExitableState> states)
    {
      _states = new Dictionary<Type, IExitableState>(states);
    }

    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      Debug.Log($"{state} enter");
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();
      Debug.Log($"{_activeState} exit");

      TState state = GetState<TState>();
      _activeState = state;
      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState => 
      _states[typeof(TState)] as TState;
  }
}