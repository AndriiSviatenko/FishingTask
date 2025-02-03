using System;
using System.Collections;
using System.Collections.Generic;
using _project.Scripts.Services;
using _project.Scripts.StateMachines.Transitions.Core;
using _project.Scripts.StateMachines.Transitions.Implementantion.Fish.States;
using UnityEngine;

namespace _project.Scripts.Fish
{
    [RequireComponent(typeof(Rigidbody))]
    public class Fish : MonoBehaviour, Patterns.Pool.Core.IPoolable<Fish>
    {
        private const float DISTANCE_TO_CHANGE_TARGET = 0.4f;
        public event Action ReturnInWaterEvent;
        public event Action<Fish> ReturnInPoolEvent;

        [SerializeField] private Config.Config config;
        [SerializeField] private Detector detector;
        [SerializeField] private Interaction.Interaction interaction;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float timeToGetOut;
        private StateMachine _stateMachine;
        private WayPointSystem _wayPointSystem;
        private Movement.Movement _movement;
        private Coroutine _delayCoroutine;
        private Vector3 _currentPoint;
        private bool _isInit;
        private bool _isCollisionBobber;
        private bool _isSetBobber;
        private bool _isMoveActive;
        public Config.Config Config => config;
        public bool IsFishBited { get; private set; }
        private bool IsDistanceReach => 
            Vector3.Distance(transform.position, _currentPoint) < DISTANCE_TO_CHANGE_TARGET;

        public void SetBobber(Vector3 target)
        {
            SetTarget(target);
            _isSetBobber = true;
            detector.Enable();
            StartCoroutine(GoOutBobber());
        }
        public void Init(IEnumerable<Vector3> points)
        {
            _wayPointSystem = new WayPointSystem(points);
            _movement = new Movement.Movement(transform, rb, config.Fish.Speed,_currentPoint);
            ChangeTarget();
            _isInit = true;
            InitStateMachine();
        }
        private void InitStateMachine()
        {
            _stateMachine = new StateMachine(new IState[]
            {
                new Idle(),
                new Move(_movement)
            }, new ITransition[]
            {
                new Transition<Idle, Move>(() => _isMoveActive), 
                new Transition<Move, Idle>(() => !_isMoveActive || IsFishBited),
            });
        }
        
        public void Play()
        {
            if (_isInit == false)
                throw new InvalidOperationException(nameof(_isInit));

            detector.Disable();
            detector.OnEnter += CheckCollider;
            gameObject.SetActive(true);
            _isMoveActive = true;
        }
        
        private void Update()
        {
            if (IsDistanceReach) 
                ChangeTarget();
            
            _stateMachine.Tick();
        }
        private bool IsBobber(Collider other, out Bobber bobber) => 
            other.TryGetComponent(out bobber);
        private void CheckCollider(Collider other)
        {
            Debug.Log("Collision");
            if (IsBobber(other, out Bobber bobber))
            {
                Debug.Log("Bobber Collision");
                _isCollisionBobber = true;
                interaction.BaitEvent += () => InteractionOnBaitEvent(bobber);
                interaction.BitedEvent += () => InteractionOnBitedEvent(bobber);
                interaction.Interact();
                detector.Disable();
            }
        }
        private void InteractionOnBitedEvent(Bobber bobber)
        {
            Debug.Log("Bit Logic");
            IsFishBited = true;
            bobber.Bit(this);
            transform.SetParent(bobber.transform);

            if (_delayCoroutine == null) 
                _delayCoroutine = StartCoroutine(ResetMoveState(true));
        }
        private void InteractionOnBaitEvent(Bobber bobber)
        {
            Debug.Log("Bait logic");
            bobber.Bait();
            if (_delayCoroutine == null) 
                _delayCoroutine = StartCoroutine(ResetMoveState(false));
        }
        private void SetTarget(Vector3 target) => 
            _movement.SetTarget(target);

        private void ChangeTarget()
        {
            _currentPoint = _wayPointSystem.Get();
            _movement.SetTarget(_currentPoint);
        }
        private IEnumerator ResetMoveState(bool useDelay)
        {
            if (useDelay)
                yield return StartCoroutine(DelayRoutine(timeToGetOut));
            else
                yield return null;
            
            IsFishBited = false;
            ChangeTarget();
            _isMoveActive = true;
            _delayCoroutine = null;
            transform.SetParent(null);
            _isCollisionBobber = false;
            ReturnInWaterEvent?.Invoke();
        }
        
        private IEnumerator DelayRoutine(float time)
        {
            yield return new WaitForSeconds(time);
        }
        private IEnumerator GoOutBobber()
        {
            yield return StartCoroutine(DelayRoutine(2f));

            if (_isCollisionBobber == false) 
                StartCoroutine(ResetMoveState(false));
        }
        
        public void ReturnInPool() => 
            ReturnInPoolEvent?.Invoke(this);
        
        public void Stop()
        {
            gameObject.SetActive(false);
            _isMoveActive = false;
            ReturnInPool();
        }
    }
}