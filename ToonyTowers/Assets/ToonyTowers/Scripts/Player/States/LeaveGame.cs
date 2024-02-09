using ToonyTowers.StateMachine.States;
using UnityEngine;

namespace ToonyTowers.Player.States
{
    public class LeaveGame : BaseState<TheThing>
    {
        private Waypoint.Waypoint[] _waypoints;

        public LeaveGame(TheThing owner) : base(owner) { }

        public override void OnEnter()
        {
            Debug.Log("LeaveGame::OnEnter");
            _waypoints = Object.FindObjectsByType<Waypoint.Waypoint>(FindObjectsSortMode.None);
        }

        public override void Tick()
        {
            FindLeaveArea();
        }

        public override void OnExit()
        {
            Owner.gameObject.SetActive(false);
        }
        
        private void FindLeaveArea()
        {
            foreach (var waypoint in _waypoints)
            {
                if (waypoint.Config.Type != Waypoint.WaypointType.LeaveGame) continue;
                Owner.Target = waypoint;
                return;
            }
        }
    }
}