using UnityEngine;
namespace _project.Scripts
{
    public class WaterBuoyancy : MonoBehaviour
    {
        [SerializeField] private float sideMovementStrength;
        [SerializeField] private float sideMovementSpeed;
        [SerializeField] private float returnSpeed;

        private Vector3 _initialPosition;
        private float _sideOffset;

        private void Start() => 
            _initialPosition = transform.position;

        private void Update()
        {
            _sideOffset = Mathf.Sin(Time.time * sideMovementSpeed) * sideMovementStrength;
            
            Vector3 targetPosition = _initialPosition + new Vector3(_sideOffset, 0f, 0f);
            
            transform.position = Vector3.Lerp(transform.position, targetPosition, 
                Time.deltaTime * returnSpeed);
        }
    }
}