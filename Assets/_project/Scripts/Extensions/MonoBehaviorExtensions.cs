using UnityEngine;

namespace _project.Scripts.Extensions
{
    public static class MonoBehaviorExtensions
    {
        public static void Enable(this MonoBehaviour monoBehaviour) =>
            monoBehaviour.enabled = true;
        
        public static void Disable(this MonoBehaviour monoBehaviour) => 
            monoBehaviour.enabled = false;
    }
}