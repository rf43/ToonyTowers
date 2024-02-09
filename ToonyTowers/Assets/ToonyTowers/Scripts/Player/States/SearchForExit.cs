using ToonyTowers.StateMachine.States;
using UnityEngine;

namespace ToonyTowers.Player.States
{

    public class SearchForExit : BaseState<TheThing>
    {
        private Waypoint.Waypoint[] _waypoints;
        
        public SearchForExit(TheThing owner) : base(owner) { }

        public override void OnEnter()
        {
            Debug.Log("SearchForExit::OnEnter");
            _waypoints = Object.FindObjectsByType<Waypoint.Waypoint>(FindObjectsSortMode.None);
        }

        public override void Tick()
        {
            FindExit();
        }

        public override void OnExit() { }
        
        private void FindExit()
        {
            foreach (var waypoint in _waypoints)
            {
                if (waypoint.Config.Type != Waypoint.WaypointType.Exit) continue;
                Owner.Target = waypoint;
                return;
            }
        }
    }
}