using _project.Scripts.Services;
using _project.Scripts.Services.Storage;
using _project.Scripts.StateMachines.Any.Core;
using _project.Scripts.UI;

namespace _project.Scripts.StateMachines.Any.Implementation.Game.States
{
    public class End : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly Controller _controller;
        private readonly Wallet.Wallet _wallet;
        private readonly Player.Controller _player;
        private readonly FishSpawner _fishSpawner;
        private readonly SetuperFish _setuperFish;
        private readonly IStorageService _storageService;

        public End(StateMachine stateMachine, Controller controller, Wallet.Wallet wallet, 
            Player.Controller player, FishSpawner fishSpawner, SetuperFish setuperFish, IStorageService storageService)
        {
            _stateMachine = stateMachine;
            _controller = controller;
            _wallet = wallet;
            _player = player;
            _fishSpawner = fishSpawner;
            _setuperFish = setuperFish;
            _storageService = storageService;
        }
        public void Enter()
        {
            if (_player.FishingRod.CurrentFish != null)
            {
                _controller.ShowCursor();
                _controller.ShowOffer(_player.FishingRod.CurrentFish.Config.Fish);
                
                _controller.SellClickEvent += Sell;
                _controller.ReleaseClickEvent += EnterStart;
            }
            else
            {
                EnterStart();
            }
        }
        public void Exit()
        {
            _fishSpawner.Off();
            _setuperFish.Off();
            
            _controller.SellClickEvent -= Sell;
            _controller.ReleaseClickEvent -= EnterStart;
        }
        private void Sell()
        {
            _wallet.Add(_player.FishingRod.CurrentFish.Config.Fish.Price);
            _storageService.Save("Money", _wallet.WalletData);
            _controller.UpdateMoney(_wallet.WalletData.Money);
            EnterStart();
        }
        private void EnterStart() => 
            _stateMachine.Enter<Start>();
    }
}