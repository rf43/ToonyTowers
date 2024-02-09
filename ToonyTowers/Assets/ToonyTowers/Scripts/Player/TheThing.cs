using ToonyTowers.Player.States;
using ToonyTowers.StateMachine;
using ToonyTowers.StateMachine.Predicates;
using ToonyTowers.StateMachine.States;
using UnityEngine;

namespace ToonyTowers.Player
{
    [AddComponentMenu("ToonyTowers/Player/The Thing")]
    public class TheThing : MonoBehaviour
    {
        [field: SerializeField] public float Speed { get; private set; } = 5.0f;
        [SerializeField] private float _minDistance = 0.1f;

        public Waypoint.Waypoint Target { get; set; }
        public Transform Transform { get; private set; }

        private Machine _stateMachine;

        #region Unity Lifecycle

        private void Awake()
        {
            Transform = transform;
            _stateMachine = new Machine();

            var searchForTarget = new SearchForTarget(this);
            var moveToSelectedWaypoint = new MoveToSelectedWaypoint(this);

            At(searchForTarget, moveToSelectedWaypoint, HasTarget());
            At(moveToSelectedWaypoint, searchForTarget, HasReachedTarget());

            _stateMachine.SetState(searchForTarget);
            
            #region Add Transition Functions
            
            void At(IState to, IState from, IPredicate condition) =>
                _stateMachine.AddTransition(to, from, condition);

            void Ay(IState to, IPredicate condition) =>
                _stateMachine.AddAnyTransition(to, condition);

            #endregion
            
            #region Predicates
            
            ActionPredicate HasTarget() => new(
                predicate: () => Target != null,
                action: () => Target.Renderer.material.color = Color.green
            );

            ActionPredicate HasReachedTarget() => new(
                predicate: () => Target != null
                                 && Vector3.Distance(Transform.position, Target.transform.position) < _minDistance,
                action: () => Target.Renderer.material.color = Color.red
            );

            #endregion
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        #endregion
    }
}