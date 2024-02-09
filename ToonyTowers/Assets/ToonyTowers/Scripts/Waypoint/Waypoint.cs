using UnityEngine;

namespace ToonyTowers.Waypoint
{
    [AddComponentMenu("ToonyTowers/Waypoint/Waypoint")]
    public class Waypoint : MonoBehaviour
    {
        public MeshRenderer Renderer { get; private set; }

        private void Awake()
        {
            Renderer = GetComponent<MeshRenderer>();
        }
    }
}