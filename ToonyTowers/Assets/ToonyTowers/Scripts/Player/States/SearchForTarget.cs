using ToonyTowers.StateMachine.States;
using UnityEngine;

namespace ToonyTowers.Player.States
{
    public class SearchForTarget : BaseState<TheThing>
    {
        private Waypoint.Waypoint[] _waypoints;
        
        public SearchForTarget(TheThing owner) : base(owner) { }
        
        public override void OnEnter()
        {
            _waypoints = Object.FindObjectsByType<Waypoint.Waypoint>(FindObjectsSortMode.None);
        }

        public override void Tick()
        {
            Owner.Target = FindNewTarget();
        }
        
        public override void OnExit() { }
        
        private Waypoint.Waypoint FindNewTarget()
        {
            return _waypoints[Random.Range(0, _waypoints.Length)];
        }
    }
}