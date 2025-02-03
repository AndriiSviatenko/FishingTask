using _project.Scripts.Services;
using _project.Scripts.Services.Storage;
using _project.Scripts.StateMachines.Any.Core;
using _project.Scripts.UI;
using _project.Scripts.Wallet;

namespace _project.Scripts.StateMachines.Any.Implementation.Game.States
{
    public class Start : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly Controller _uiController;
        private readonly Wallet.Wallet _wallet;
        private readonly FishSpawner _fishSpawner;
        private readonly IStorageService _storageService;
        public Start(StateMachine stateMachine, 
            Controller controller,
            Wallet.Wallet wallet,
            FishSpawner fishSpawner,
            IStorageService storageService)
        {
            _storageService = storageService;
            _stateMachine = stateMachine;
            _uiController = controller;
            _wallet = wallet;
            _fishSpawner = fishSpawner;
        }
        public void Enter()
        {
            LoadData();
            _uiController.UpdateMoney(_wallet.WalletData.Money);
            _uiController.ShowCursor();
            _fishSpawner.Init();
            _stateMachine.Enter<SetupBait>();
        }
        private void LoadData()
        {
            _storageService.Load<WalletData>("Money", value =>
            {
                _wallet.Load(value);
            });
        }
        public void Exit()
        {
        }
    }
}