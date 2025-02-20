using UnityEngine;

namespace Project.Scripts.MapGenerationSystem
{
    public class RandomWalkMapConfig : MapConfig
    {
        [field: SerializeField] public bool InvertGrid { get; private set; }
        [field: SerializeField] public Vector2 StartingPoint { get; private set; }
        [field: SerializeField] public int MaxIterations { get; private set; }
    }
}