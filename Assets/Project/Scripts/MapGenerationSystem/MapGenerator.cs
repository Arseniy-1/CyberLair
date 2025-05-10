using System.Collections.Generic;
using Project.Scripts.MapGenerationSystem.Algorithms;
using Project.Scripts.MapGenerationSystem.ObjectPlacer;
using UnityEngine;

namespace Project.Scripts.MapGenerationSystem
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private List<MapLayer> _mapLayers;
        [SerializeField] private MapObjectPlacer _mapObjectPlacer;

        
        public void Initialize()
        {
            foreach (MapLayer mapLayer in _mapLayers)
            {
                mapLayer.Render(CreateAlgorithm(mapLayer)?.RandomFillMap());
            }
            
            _mapObjectPlacer.Place();
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