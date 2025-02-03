using System;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts
{
    public class ProgressSlider : MonoBehaviour
    {
        public event Action CompletedEvent;
        [SerializeField] private Slider slider;

        public void Init(float minValue, float maxValue)
        {
            slider.minValue = minValue;
            slider.maxValue = maxValue;
            slider.value = minValue;
        }
        
        public void Add(float value)
        {
            slider.value += value;
            
            if (slider.value == slider.maxValue) 
                CompletedEvent?.Invoke();
        }
        public void ResetProgress() => 
            slider.value = slider.minValue;
    }
}