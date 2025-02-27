using UnityEngine;

namespace Project.Scripts.MapGenerationSystem
{
    public class MapConfig : ScriptableObject
    {
        [field: SerializeField] public int Width { get; private set; }
        [field: SerializeField] public int Height { get; private set; }
        [field: SerializeField, Range(0f, 1f)] public float FillPercent { get; private set; }
    }
}