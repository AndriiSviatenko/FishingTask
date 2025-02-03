using TMPro;
using UnityEngine;
namespace _project.Scripts.UI.HUD
{
   public class HUD : BasePanel
   {
      [SerializeField] private TextMeshProUGUI money;
      [SerializeField] private ProgressSlider progressFishing;

      public override void Show()
      {
         base.Show();
         Debug.Log("SHOW HUD");
      }
      public void UpdateMoney(int value) => 
         money.text = $"Money: {value}";
      public void SetProgress(float min, float max) => 
         progressFishing.Init(min,max);
      public void ResetProgress() =>
         progressFishing.ResetProgress();
      public void SetFishBitProgress() => 
         progressFishing.Add(1);
   }
}
