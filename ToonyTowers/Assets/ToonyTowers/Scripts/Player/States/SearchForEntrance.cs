using ToonyTowers.StateMachine.States;
using UnityEngine;

namespace ToonyTowers.Player.States
{
    public class SearchForEntrance : BaseState<TheThing>
    {
        private Waypoint.Waypoint[] _waypoints;

        public SearchForEntrance(TheThing owner) : base(owner) { }

        public override void OnEnter()
        {
            _waypoints = Object.FindObjectsByType<Waypoint.Waypoint>(FindObjectsSortMode.None);
        }

        public override void Tick()
        {
            FindEntrance();
        }

        public override void OnExit() { }

        private void FindEntrance()
        {
            foreach (var waypoint in _waypoints)
            {
                if (waypoint.Config.Type != Waypoint.WaypointType.Entrance) continue;
                Owner.Target = waypoint;
                return;
            }
        }
    }
}