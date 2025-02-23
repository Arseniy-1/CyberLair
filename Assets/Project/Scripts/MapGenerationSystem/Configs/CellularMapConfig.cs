using UnityEngine;

namespace Project.Scripts.MapGenerationSystem
{
    [CreateAssetMenu(fileName = "CellularMapConfig", menuName = "Map/CellularAutomata", order = 51)]
    public class CellularMapConfig : MapConfig
    {
        [field: SerializeField, Range(0f, 10f)] public float SmoothSteps { get; private set; }
        [field: SerializeField, Range(0f, 10f)] public float SmoothThreshold { get; private set; }
    }
}