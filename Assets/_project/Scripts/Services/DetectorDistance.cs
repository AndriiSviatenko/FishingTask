using UnityEngine;

namespace _project.Scripts.Services
{
    public class DetectorDistance : MonoBehaviour
    {
        private Transform _from;
        private Transform _to;
        private float _reachDistance;
        private bool _isInit;
        public bool IsDistanceCatch
        {
            get
            {
                if(_isInit && _from != null && _to != null)
                    return Vector3.Distance(_from.position, _to.position) < _reachDistance;
                
                return false;
            }
        }

        public void Init(Transform from, Transform to, float reachDistance)
        {
            _from = from;
            _to = to;
            _reachDistance = reachDistance;
            _isInit = true;
        }
        
        public void ChangeTransforms(Transform from, Transform to)
        {
            _to = to;
            _from = from;
        }
    }
}