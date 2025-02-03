using UnityEngine;

namespace _project.Scripts.Fish.Movement
{
    public class Movement
    {
        private readonly Transform _transform;
        private readonly Rigidbody _rb;
        private readonly float _speed;
        private Vector3 _target;

        public bool IsActive { get; private set; }

        public Movement(Transform transform ,Rigidbody rb, float speed, Vector3 target)
        {
            _transform = transform;
            _rb = rb;
            _speed = speed;
            _target = target;
        }
        public void SetTarget(Vector3 target) =>
            _target = target;
        public void StartMove()
        {
            IsActive = true;
            _rb.isKinematic = false;
        }

        public void StopMove()
        {
            IsActive = false;
            _rb.velocity = Vector3.zero;
            _rb.isKinematic = true;
        }

        public void Move()
        {
            if (IsActive)
            {
                var newDirection = _target - _transform.position;
                _rb.velocity = newDirection.normalized * _speed;
                _transform.forward = newDirection;
            }
        }
    }
}
