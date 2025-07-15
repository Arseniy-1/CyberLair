using Project.Scripts.Interfaces;
using Project.Scripts.MapGenerationSystem.Configs;
using UnityEngine;

namespace Project.Scripts.MapGenerationSystem.Algorithms
{
    public class CellularAutomataAlgorithm : IMapAlgorithm
    {
        private readonly CellularMapConfig _mapConfig;

        public CellularAutomataAlgorithm(CellularMapConfig mapConfig)
        {
            _mapConfig = mapConfig;
        }
        
        public int[,] RandomFillMap()
        {
            var terrainMap = new int[_mapConfig.Width, _mapConfig.Height];

            for (var x = 0; x < _mapConfig.Width; x++)
            {
                for (var y = 0; y < _mapConfig.Height; y++)
                {
                    if (x == 0 || x == _mapConfig.Width - 1 || y == 0 || y == _mapConfig.Height - 1)
                        terrainMap[x, y] = MapGeneratorConstraints.Default;
                    else
                        terrainMap[x, y] = Random.value < _mapConfig.FillPercent
                            ? MapGeneratorConstraints.Terrain
                            : MapGeneratorConstraints.Default;
                }
            }
            
            for (var i = 0; i < _mapConfig.SmoothSteps; i++)
            {
                SmoothMap(ref terrainMap);
            }

            return terrainMap;
        }

        private void SmoothMap(ref int[,] terrainMap)
        {
            for (var x = 0; x < _mapConfig.Width; x++)
            {
                for (var y = 0; y < _mapConfig.Height; y++)
                {
                    var neighbourWallTiles = GetSurroundingWallCount(terrainMap, x, y);

                    if (neighbourWallTiles > _mapConfig.SmoothThreshold)
                        terrainMap[x, y] = MapGeneratorConstraints.Default;
                    else if (neighbourWallTiles < _mapConfig.SmoothThreshold)
                        terrainMap[x, y] = MapGeneratorConstraints.Terrain;
                }
            }
        }

        private int GetSurroundingWallCount(int[,] terrainMap, int gridX, int gridY)
        {
            var wallCount = 0;

            for (var neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
            {
                for (var neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
                {
                    bool isNeighborX = neighbourX >= 0 && neighbourX < _mapConfig.Width;
                    bool isNeighborY = neighbourY >= 0 && neighbourY < _mapConfig.Height;
                    
                    if (isNeighborX && isNeighborY)
                    {
                        if (neighbourX != gridX || neighbourY != gridY) 
                            wallCount += terrainMap[neighbourX, neighbourY];
                    }
                    else
                    {
                        wallCount++;
                    }
                }
            }

            return wallCount;
        }
    }
}