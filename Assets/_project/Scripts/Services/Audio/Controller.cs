using UnityEngine;
using Zenject;

namespace _project.Scripts.Services.Audio
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip startFishing;
        [SerializeField] private AudioClip reeling;
        private FishingRod.FishingRod _fishingRod;

        [Inject]
        private void Construct(FishingRod.FishingRod fishingRod)
        {
            _fishingRod = fishingRod;
            
            _fishingRod.StartFishingEvent += PlayFishing;
            _fishingRod.ReelingEvent += PlayReeling;
            _fishingRod.ReelUpEvent += OffReeling;
            _fishingRod.BobberReachedWaterEvent += OffFishing;
        }

        private void UnSubscribe()
        {
            _fishingRod.StartFishingEvent -= PlayFishing;
            _fishingRod.ReelingEvent -= PlayReeling;
            _fishingRod.ReelUpEvent -= OffReeling;
            _fishingRod.BobberReachedWaterEvent -= OffFishing;
        }
        private void OnDestroy() => 
            UnSubscribe();
        private void PlayReeling() => 
            Change(reeling, false);
        
        private void PlayFishing() => 
            Change(startFishing, true);
        
        private void Change(AudioClip audioClip, bool interruption)
        {
            if(audioSource.clip == audioClip) 
                return;
            
            audioSource.clip = audioClip;
            if (interruption)
            {
                audioSource.Play();
            }
            else
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
        }
        private void OffReeling()
        {
            if (audioSource.clip == reeling)
            {
                audioSource.clip = null;
                audioSource.Stop();    
            }
        }
        private void OffFishing()
        {
            if (audioSource.clip == startFishing)
            {
                audioSource.clip = null;
                audioSource.Stop();    
            }
        }
    }
}