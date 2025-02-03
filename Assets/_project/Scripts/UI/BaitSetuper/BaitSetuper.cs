using System;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.UI.BaitSetuper
{
    public class BaitSetuper : BasePanel
    {
        public event Action BaitSetupedEvent;
        [SerializeField] private Slider progressBaitSetting;
        public bool BaitSetuped { get; private set; }
        
        public void Init()
        {
            progressBaitSetting.value = 0;
            progressBaitSetting.onValueChanged.AddListener(CheckProgress);
            BaitSetuped = false;
        }
        private void CheckProgress(float value)
        {
            if(BaitSetuped)
                return;
            
            if (value > 0.9)
            {
                BaitSetuped = true;
                BaitSetupedEvent?.Invoke();
                progressBaitSetting.onValueChanged.RemoveListener(CheckProgress);
            }
            else
            {
                BaitSetuped = false;
            }
        }
    }
}