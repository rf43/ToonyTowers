using System.Linq;
using ToonyTowers.StateMachine.States;
using UnityEngine;

namespace ToonyTowers.Player.States
{
    public class DetermineNextWaypoint : BaseState<TheThing>
    {
        private Waypoint.Waypoint[] _waypoints;

        public DetermineNextWaypoint(TheThing theThing) : base(theThing) { }

        public override void OnEnter()
        {
            _waypoints = Object.FindObjectsByType<Waypoint.Waypoint>(FindObjectsSortMode.None);
        }

        public override void Tick()
        {
            FindAndSetNextWaypoint();
        }

        public override void OnExit() { }

        private void FindAndSetNextWaypoint()
        {
            if (Random.Range(0, 2) == 0)
            {
                FindWanderingPoint();
            }
            else
            {
                FindStopPoint();
            }
        }

        private void FindStopPoint()
        {
            var point = _waypoints.Where(waypoint => waypoint.Config.Type == Waypoint.WaypointType.StopLocation)
                .ToList();
            var rndIndex = Random.Range(0, point.Count);
            var outPoint = point[rndIndex];
            Owner.Target = outPoint;
        }

        private void FindWanderingPoint()
        {
            var point = _waypoints.Where(waypoint => waypoint.Config.Type == Waypoint.WaypointType.Wander).ToList();
            var rndIndex = Random.Range(0, point.Count);
            var outPoint = point[rndIndex];
            Owner.Target = outPoint;
        }
    }
}