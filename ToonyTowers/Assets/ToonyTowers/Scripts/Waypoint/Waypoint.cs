using ToonyTowers.Waypoint.Configs;
using UnityEngine;

namespace ToonyTowers.Waypoint
{
    [AddComponentMenu("ToonyTowers/Waypoint/Waypoint")]
    public class Waypoint : MonoBehaviour
    {
        [field: SerializeField] public WaypointScriptableObject Config { get; private set; }
        public MeshRenderer Renderer { get; private set; }

        private void Awake()
        {
            Renderer = GetComponent<MeshRenderer>();
        }
    }
}