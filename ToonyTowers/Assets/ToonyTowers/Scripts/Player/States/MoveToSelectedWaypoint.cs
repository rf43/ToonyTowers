using ToonyTowers.StateMachine.States;
using UnityEngine;
using UnityEngine.AI;

namespace ToonyTowers.Player.States
{
    public class MoveToSelectedWaypoint : BaseState<TheThing>
    {
        private const float WalkAnimationEnabled = 1.0f;
        private const float WalkAnimationDisabled = 0.0f;
        
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        
        private readonly int _walkHash = Animator.StringToHash("Walk");

        public MoveToSelectedWaypoint(TheThing owner, NavMeshAgent navMeshAgent, Animator animator) : base(owner)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
        }

        public override void OnEnter()
        {
            _navMeshAgent.enabled = true;
            _navMeshAgent.speed = Random.Range(Owner.MinimumSpeed, Owner.MaximumSpeed);
            _animator.SetFloat(_walkHash, WalkAnimationEnabled);
        }

        public override void Tick()
        {
            MoveToTarget();
        }

        public override void OnExit()
        {
            _navMeshAgent.enabled = false;
            _animator.SetFloat(_walkHash, WalkAnimationDisabled);
        }

        private void MoveToTarget()
        {
            _navMeshAgent.SetDestination(Owner.Target.transform.position);
        }
    }
}