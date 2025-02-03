using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.Services
{
    public class WayPointSystem
    {
        private readonly List<Vector3> _points;
        private Vector3 _prevPoint;
        
        public WayPointSystem(IEnumerable<Vector3> points) => 
            _points = new (points);
        
        public Vector3 Get()
        {
            var newPoint = _points[Random.Range(0, _points.Count)];
            
            while (_prevPoint == newPoint)
            {
                newPoint = _points[Random.Range(0, _points.Count)];
            }
            
            _prevPoint = newPoint;
            return newPoint;
        }
    }
}