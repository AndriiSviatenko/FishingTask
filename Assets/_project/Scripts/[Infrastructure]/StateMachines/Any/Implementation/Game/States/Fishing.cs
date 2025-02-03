using _project.Scripts.StateMachines.Any.Core;
using _project.Scripts.UI;

namespace _project.Scripts.StateMachines.Any.Implementation.Game.States
{
    public class Fishing : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly Player.Controller _player;
        private readonly SetuperFish _setuperFish;
        private readonly Controller _controller;
        public Fishing(StateMachine stateMachine, Controller controller, Player.Controller player, SetuperFish setuperFish)
        {
            _controller = controller;
            _stateMachine = stateMachine;
            _player = player;
            _setuperFish = setuperFish;
        }
        public void Enter()
        {
            Subscribe();
            
            ResetProgress();
            _controller.ShowHUD();
            _player.StartFishing();
        }
        private void Subscribe()
        {
            _player.FishingRod.BobberReachedWaterEvent += InitSetuperFish;
            _player.FishingRod.FishBitedEvent += SetBitProgress;
            _player.FishingRod.ResetProgressEvent += ResetProgress;
            _player.FishingRod.CatchFishEvent += EnterEnd;
            _player.FishingRod.StartFishingEvent += HideCursor;
        }
        private void UnSubscribe()
        {
            _player.FishingRod.BobberReachedWaterEvent -= InitSetuperFish;
            _player.FishingRod.FishBitedEvent -= SetBitProgress;
            _player.FishingRod.ResetProgressEvent -= ResetProgress;
            _player.FishingRod.CatchFishEvent -= EnterEnd;
            _player.FishingRod.StartFishingEvent -= HideCursor;
        }

        public void Exit()
        {
            _player.StopFishing();
            
            UnSubscribe();
        }
        private void HideCursor() => 
            _controller.HideCursor();
        private void InitSetuperFish() => 
            _setuperFish.Init();
        private void ResetProgress() => 
            _controller.ResetProgress();
        private void EnterEnd() => 
            _stateMachine.Enter<End>();
        private void SetBitProgress() => 
            _controller.SetFishBitProgress();
    }
}