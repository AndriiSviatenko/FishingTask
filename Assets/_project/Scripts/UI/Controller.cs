using System;
using _project.Scripts.Fish.Config;
using UnityEngine;

namespace _project.Scripts.UI
{
    public class Controller : MonoBehaviour
    {
        public event Action StartGameEvent;
        public event Action BaitSetupedEvent;
        public event Action SellClickEvent;
        public event Action ReleaseClickEvent;

        [SerializeField] private Main_Menu.MainMenu mainMenu;
        [SerializeField] private Fish_Offer.FishOffer fishOffer;
        [SerializeField] private HUD.HUD hud;
        [SerializeField] private BaitSetuper.BaitSetuper baitSetuper;
        private RechangerPanel _rechangerPanel = new ();

        public void Init()
        {
            mainMenu.PlayClickEvent += StartGame;
            fishOffer.SellCLickEvent += SellFish;
            fishOffer.ReleaseCLickEvent += ReleaseFish;
            
            ShowCursor();
            _rechangerPanel.ShowPanel(mainMenu);
        }
        
        private void OnDestroy() => 
            mainMenu.PlayClickEvent -= StartGame;
        
        public void ShowCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        public void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        private void BaitSetuped()
        {
            BaitSetupedEvent?.Invoke(); 
            baitSetuper.BaitSetupedEvent -= BaitSetuped;
        }
        private void SellFish(Data wallet) => 
            SellClickEvent?.Invoke();
        private void ReleaseFish() => 
            ReleaseClickEvent?.Invoke();
        public void ShowOffer(Data fishData)
        {
            fishOffer.Init(fishData);
            _rechangerPanel.ShowPanel(fishOffer);
        }
        public void SetFishBitProgress() => 
            hud.SetFishBitProgress();  
        
        public void ResetProgress() => 
            hud.ResetProgress();

        public void ShowHUD() => 
            _rechangerPanel.ShowPanel(hud);

        public void SetHUDProgress(float min, float max) => 
            hud.SetProgress(min,max);
        
        public void HideHud() => 
            hud.Hide();

        public void UpdateMoney(int value) => 
            hud.UpdateMoney(value);
        
        private void StartGame()
        {
            Debug.Log($"Start Game");
            StartGameEvent?.Invoke();
        }

        public void ShowBaitSetuper()
        {
            baitSetuper.Init();
            _rechangerPanel.ShowPanel(baitSetuper);
            baitSetuper.BaitSetupedEvent += BaitSetuped;
        }
    }
}
