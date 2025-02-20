using UnityEngine;

namespace Project.Scripts.MapGenerationSystem
{
    public class CellularMapConfig : MapConfig
    {
        [field: SerializeField, Range(0f, 10f)] public float SmoothSteps { get; private set; }
        [field: SerializeField, Range(0f, 10f)] public float SmoothThreshold { get; private set; }
    }
}