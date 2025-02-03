using System;
using System.Collections.Generic;
using _project.Scripts.Services;
using _project.Scripts.Services.Storage;
using _project.Scripts.StateMachines.Any.Core;
using _project.Scripts.UI;
using Zenject;

namespace _project.Scripts.Patterns.Factory.StateMachine.Any
{
    public class StateMachineFactory
    {
        private readonly DiContainer _container;
        
        private Controller _uiController;
        private FishSpawner _fishSpawner;
        private SetuperFish _setuperFish;
        private Wallet.Wallet _wallet;
        private Player.Controller _player;
        private IStorageService _storageService;

        public StateMachineFactory(DiContainer container) => 
            _container = container;
        public StateMachines.Any.Core.StateMachine Create()
        {
            var stateMachine = new StateMachines.Any.Core.StateMachine();
            
            ResolveDependencies();
            var states = CreateStates(stateMachine);
            stateMachine.SetStates(states);
            return stateMachine;
        }
        private Dictionary<Type, IExitableState> CreateStates(StateMachines.Any.Core.StateMachine stateMachine)
        {
            var states = new Dictionary<Type, IExitableState>
            {
                {
                    typeof(StateMachines.Any.Implementation.Game.States.BootStrap), 
                    new StateMachines.Any.Implementation.Game.States.BootStrap(stateMachine, _uiController)
                },
                {
                    typeof(StateMachines.Any.Implementation.Game.States.Start), 
                    new StateMachines.Any.Implementation.Game.States.Start(stateMachine, _uiController, _wallet, _fishSpawner, _storageService)
                },
                {
                    typeof(StateMachines.Any.Implementation.Game.States.SetupBait), 
                    new StateMachines.Any.Implementation.Game.States.SetupBait(stateMachine, _uiController)
                },
                {
                    typeof(StateMachines.Any.Implementation.Game.States.Fishing), 
                    new StateMachines.Any.Implementation.Game.States.Fishing(stateMachine, _uiController, _player, _setuperFish)
                },
                {
                    typeof(StateMachines.Any.Implementation.Game.States.End), 
                    new StateMachines.Any.Implementation.Game.States.End(stateMachine, _uiController, _wallet, _player, _fishSpawner, _setuperFish, _storageService)
                }
            };
            return states;
        }
        private void ResolveDependencies()
        {
            _uiController = _container.Resolve<Controller>();
            _wallet = _container.Resolve<Wallet.Wallet>();
            _fishSpawner = _container.Resolve<FishSpawner>();
            _player = _container.Resolve<Player.Controller>();
            _setuperFish = _container.Resolve<SetuperFish>();
            _storageService = _container.Resolve<IStorageService>();
        }
    }
}