using ToonyTowers.StateMachine.States;
using UnityEngine;

namespace ToonyTowers.Player.States
{
    public class MoveToSelectedWaypoint : BaseState<TheThing>
    {
        public MoveToSelectedWaypoint(TheThing owner) : base(owner) { }

        public override void OnEnter() { }

        public override void Tick()
        {
            MoveToTarget();
        }

        public override void OnExit() { }

        private void MoveToTarget()
        {
            var targetPosition = Owner.Target.transform.position;
            var direction = targetPosition - Owner.transform.position;
            var movement = direction.normalized * (Owner.Speed * Time.deltaTime);
            var newPosition = Owner.Transform.position + movement;
            Owner.Transform.position = newPosition;
        }
    }
}