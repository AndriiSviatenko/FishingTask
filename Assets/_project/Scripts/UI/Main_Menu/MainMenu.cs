using System;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.UI.Main_Menu
{
    public class MainMenu : BasePanel
    {
        public event Action PlayClickEvent;
        [SerializeField] private Button playBtn;

        private void Awake() => 
            playBtn.onClick.AddListener(OnPlayClick);
        private void OnDestroy() => 
            playBtn.onClick.RemoveListener(OnPlayClick);
        private void OnPlayClick() => 
            PlayClickEvent?.Invoke();
    }
}