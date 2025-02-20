using UnityEngine;

namespace Project.Scripts.MapGenerationSystem
{
    public class PerlinNoiseMapConfig : MapConfig
    {
        [field: SerializeField] public float NoiseScale { get; private set; }
        [field: SerializeField] public Vector2 NoiseOffset { get; private set; }
    }
}