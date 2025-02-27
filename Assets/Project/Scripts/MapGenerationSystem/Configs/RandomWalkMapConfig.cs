using UnityEngine;

namespace Project.Scripts.MapGenerationSystem
{
    [CreateAssetMenu(fileName = "RandomWalkMapConfig", menuName = "Map/RandomWalk", order = 51)]
    public class RandomWalkMapConfig : MapConfig
    {
        [field: SerializeField] public bool InvertGrid { get; private set; }
        [field: SerializeField] public Vector2 StartingPoint { get; private set; }
        [field: SerializeField] public int MaxIterations { get; private set; }
    }
}