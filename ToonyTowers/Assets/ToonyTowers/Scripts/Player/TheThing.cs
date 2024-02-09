using System.Collections.Generic;
using ToonyTowers.Player.States;
using ToonyTowers.StateMachine;
using ToonyTowers.StateMachine.Predicates;
using ToonyTowers.StateMachine.States;
using UnityEngine;
using UnityEngine.AI;

namespace ToonyTowers.Player
{
    [AddComponentMenu("ToonyTowers/Player/The Thing")]
    public class TheThing : MonoBehaviour
    {
        [Header("Movement Settings")]
        [field: SerializeField] public float MinimumSpeed { get; private set; } = 0.5f;
        [field: SerializeField] public float MaximumSpeed { get; private set; } = 1.5f;

        [Header("Other Settings")]
        [SerializeField] private float _minDistance = 0.1f;
        [SerializeField] private List<GameObject> _prefabs;

        public Waypoint.Waypoint Target { get; set; }

        private Transform _transform;
        private Machine _stateMachine;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        #region Unity Lifecycle

        private void Awake()
        {
            _transform = transform;
            _stateMachine = new Machine();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            
            // Instantiate a random prefab as a child of the game object
            var randomIndex = Random.Range(0, _prefabs.Count);
            var prefab = Instantiate(_prefabs[randomIndex], _transform);
            _animator = prefab.GetComponent<Animator>();
            
            var initialState = InitState();
            _stateMachine.SetState(initialState);
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        private IState InitState()
        {
            if (_stateMachine == null)
            {
                throw new MissingReferenceException("State machine is not initialized");
            }
            
            var searchForEntrance = new SearchForEntrance(this);
            var searchForExit = new SearchForExit(this);
            var determineNextWaypoint = new DetermineNextWaypoint(this);
            var moveToSelectedWaypoint = new MoveToSelectedWaypoint(this, _navMeshAgent, _animator);
            var leaveGame = new LeaveGame(this);

            At(searchForEntrance, moveToSelectedWaypoint, HasTarget());
            At(moveToSelectedWaypoint, determineNextWaypoint, HasReachedTarget());
            At(determineNextWaypoint, moveToSelectedWaypoint, HasTarget());
            At(moveToSelectedWaypoint, searchForExit, HasReachedTarget());
            At(searchForExit, moveToSelectedWaypoint, HasTarget());
            At(moveToSelectedWaypoint, leaveGame, HasReachedTarget());

            return searchForEntrance;
            
            #region Add Transition Functions

            void At(IState to, IState from, IPredicate condition) =>
                _stateMachine.AddTransition(to, from, condition);

            void Ay(IState to, IPredicate condition) =>
                _stateMachine.AddAnyTransition(to, condition);

            #endregion

            #region Predicates

            FuncPredicate HasTarget() => new(
                predicate: () => Target != null
            );

            FuncPredicate HasReachedTarget() => new(
                predicate: () => Target != null
                                 && Vector3.Distance(_transform.position, Target.transform.position) < _minDistance
            );

            #endregion
        }

        #endregion
    }
}