using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Scripts.MapGenerationSystem
{
    [CreateAssetMenu(fileName = "PerlinNoiseMapConfig", menuName = "Map/PerlinNoise", order = 51)]
    public class PerlinNoiseMapConfig : MapConfig
    {
        [field: SerializeField, MinMaxSlider(5f, 15f)] public Vector2 NoiseScale { get; private set; }
        [field: SerializeField, MinMaxSlider(0f, 100f)] public Vector2 NoiseOffsetX { get; private set; }
        [field: SerializeField, MinMaxSlider(0f, 100f)] public Vector2 NoiseOffsetY { get; private set; }
    }
}