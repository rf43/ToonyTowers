using UnityEngine;

namespace ToonyTowers.Waypoint.Configs
{
    [CreateAssetMenu(fileName = "NewWaypointConfig", menuName = "ToonyTowers/Waypoint", order = 0)]
    public class WaypointScriptableObject : ScriptableObject
    {
        [field: SerializeField] public WaypointType Type { get; private set; }
    }
}