using UnityEngine;

namespace _project.Scripts.FishingRod.Config
{
    [CreateAssetMenu(fileName = "FishRodConfig", menuName = "Configs/FishRod", order = 0)]
    public class Config : ScriptableObject
    {
        [field:SerializeField] public float BaseForce { get; private set; } 
    }
}