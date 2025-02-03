using UnityEngine;

namespace _project.Scripts.Services.VFX
{
    public class VFX : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particlePrefab;
        
        private ParticleSystem _instance;
        
        public void CreateVFX() => 
            _instance = Instantiate(particlePrefab);
        public void MoveTo(Vector3 position) => 
            _instance.transform.position = position;
        public void Enable() => 
            _instance.Play();
        public void Disable() => 
            _instance.Stop();
    }
}