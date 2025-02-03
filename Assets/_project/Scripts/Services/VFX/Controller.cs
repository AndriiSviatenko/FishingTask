using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace _project.Scripts.Services.VFX
{
    public class Controller : MonoBehaviour
    {
        private const float DELAY_RESET_EFFECT = 1f;
        [SerializeField] private VFX waterSplash;
        private Coroutine _resetEffect;
        private Player.Controller _player;

        [Inject]
        private void Construct(Player.Controller player) => 
            _player = player;
        
        private void Start() => 
            _player.FishingRod.BobberReachedWaterEvent += ShowBobberReachedWater;
        private void OnDestroy() => 
            _player.FishingRod.BobberReachedWaterEvent -= ShowBobberReachedWater;
        private void ShowBobberReachedWater()
        {
            waterSplash.CreateVFX();
            waterSplash.MoveTo(_player.FishingRod.Bobber.transform.position);
            waterSplash.Enable();
            
            _resetEffect ??= StartCoroutine(ResetEffect());
        }
        private IEnumerator ResetEffect()
        {
            yield return new WaitForSeconds(DELAY_RESET_EFFECT);
            waterSplash.Disable();
            _resetEffect = null;
        }
    }
}