using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _project.Scripts.Patterns.Factory.Implementation.Fish;
using _project.Scripts.Patterns.Pool.Implementation;
using _project.Scripts.Patterns.Repository.Implementation;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _project.Scripts.Services
{
    public class FishSpawner : MonoBehaviour
    {
        [SerializeField] private List<Fish.Fish> prefabs;
        [SerializeField] private List<Transform> movementPoints = new ();
        [SerializeField] private float delaySpawn;
        [SerializeField] private int maxCountFish;
        private readonly FishRepository _fishRepository= new ();
        private readonly List<Fish.Fish> _fishes = new ();
        private FishFactory _fishFactory;
        private FishPool _fishPool;
        private Coroutine _spawnCoroutine;
        private int _currentCount;
        public FishRepository FishRepository => _fishRepository;
        
        public void Init()
        {
            _fishFactory = new(prefabs[Random.Range(0, prefabs.Count)]);
            _fishPool = new FishPool();
            _fishPool.GetNewSpawnedEvent += () => SpawnInstance();
            _spawnCoroutine = StartCoroutine(SpawnRoutine());
        }
        public void Off()
        {
            _currentCount = 0;
            
            foreach (var fish in _fishes)
                if (fish != null)
                    Destroy(fish.gameObject);
            
            _fishes.Clear();
            _fishRepository.AllClear();
        }
        private void OnDestroy()
        {
            if(_spawnCoroutine != null)
                StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }

        private IEnumerator SpawnRoutine()
        {
            while (_currentCount < maxCountFish)
            {
                yield return new WaitForSeconds(delaySpawn);
                SpawnInstance();
            }
            yield return null;
        }
        private void SpawnInstance()
        {
            _currentCount++;
            
            var instance = _fishFactory.GetNewInstance();
            instance.Init(movementPoints.Select(point => point.position));
            instance.Play();
            
            _fishes.Add(instance);
            _fishPool.AddInstance(instance);
            _fishRepository.Add(instance);
            _fishFactory.ChangePrefab(prefabs[Random.Range(0, prefabs.Count)]);
        }
    }
}