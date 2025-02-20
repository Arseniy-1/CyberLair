using System.Collections.Generic;
using Project.Scripts.MapGenerationSystem.Algorithms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.MapGenerationSystem
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private int _seed;
        [SerializeField] private List<MapLayer> _mapLayers;

        public void Initialize()
        {
            Random.InitState(_seed);
        }

        private IMapAlgorithm CreateAlgorithm(MapLayer mapLayer)
        {
            return mapLayer.Config switch
            {
                CellularMapConfig cellular => new CellularAutomataAlgorithm(cellular),
                PerlinNoiseMapConfig perlinNoise => new PerlinNoiseAlgorithm(perlinNoise),
                RandomWalkMapConfig randomWalk => new RandomWalkAlgorithm(randomWalk),
                _ => null
            };
        }
    }
}