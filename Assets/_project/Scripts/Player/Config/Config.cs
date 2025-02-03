using UnityEngine;

namespace _project.Scripts.Player.Config
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Player", order = 0)]
    public class Config : ScriptableObject
    {
        [field:SerializeField] public float Sensitivity { get; private set; }
        [field:SerializeField, Range(-90f, 90f)] public float MinXRotation { get; private set; }
        [field:SerializeField, Range(-90f, 90f)] public float MaxXRotation { get; private set; }
        private void OnValidate()
        {
            if (MinXRotation > MaxXRotation) 
                MaxXRotation = MinXRotation;
        }
    }
}