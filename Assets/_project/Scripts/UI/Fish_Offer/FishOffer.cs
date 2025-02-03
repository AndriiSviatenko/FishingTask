using System;
using _project.Scripts.Fish.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.UI.Fish_Offer
{
    public class FishOffer : BasePanel
    {
        public event Action<Data> SellCLickEvent;
        public event Action ReleaseCLickEvent;
        [SerializeField] private TextMeshProUGUI nameFish;
        [SerializeField] private TextMeshProUGUI typeFish;
        [SerializeField] private Image iconFish;
        [SerializeField] private Button sellBtn;
        [SerializeField] private Button releaseBtn;
        private Data _fishData;

        public void Init(Data fishData)
        {
            _fishData = fishData;
            nameFish.text = fishData.Name;
            typeFish.text = fishData.Type.ToString();
            iconFish.sprite = fishData.Icon;
        }
        private void Start()
        {
            sellBtn.onClick.AddListener(OnSellCLick);
            releaseBtn.onClick.AddListener(OnReleaseCLick);
        }
        private void OnDestroy()
        {
            sellBtn.onClick.RemoveListener(OnSellCLick);
            releaseBtn.onClick.RemoveListener(OnReleaseCLick);
        }
        private void OnReleaseCLick()
        {
            ReleaseCLickEvent?.Invoke();
            Hide();
        }
        private void OnSellCLick()
        {
            SellCLickEvent?.Invoke(_fishData);
            Hide();
        }
    }
}