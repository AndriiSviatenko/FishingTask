using System.IO;
using UnityEditor;
using UnityEngine;

namespace _project.Scripts.Fish.Config
{
    [CreateAssetMenu(fileName = "FishConfig", menuName = "SO/Fish/Config", order = 0)]
    public class Config : ScriptableObject
    {
        [field:SerializeField] public Data Fish { get; private set; }
        private void OnValidate()
        {
#if UNITY_EDITOR
            LoadAndSetupName();
#endif
        }
        private void LoadAndSetupName()
        {
            var assetPath = AssetDatabase.GetAssetPath(this);
            var fileName = Path.GetFileNameWithoutExtension(assetPath);
            if (string.IsNullOrEmpty(fileName)) return;
            Fish.Name = fileName;
            EditorUtility.SetDirty(this);
        }
    }
}