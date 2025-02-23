using UnityEngine;

namespace Project.Scripts.MapGenerationSystem.Algorithms
{
    public class RandomWalkAlgorithm : IMapAlgorithm
    {
        private readonly RandomWalkMapConfig _mapConfig;

        public RandomWalkAlgorithm(RandomWalkMapConfig mapConfig)
        {
            _mapConfig = mapConfig;
        }

        public int[,] RandomFillMap()
        {
            var terrainMap = FullDefaultMap();
            
            var requiredFillQuantity = (int)(_mapConfig.Width * _mapConfig.Height * _mapConfig.FillPercent);
            var fillCounter = 0;

            var currentX = (int)_mapConfig.StartingPoint.x;
            var currentY = (int)_mapConfig.StartingPoint.y;
            terrainMap[currentX, currentY] = 0;
            fillCounter++;
            var iterationsCounter = 0;
            
            while (fillCounter < requiredFillQuantity && iterationsCounter < _mapConfig.MaxIterations)
            { 
                int direction = Random.Range(0, 4); 

                switch (direction)
                {
                    case 0: 
                        if ((currentY + 1) < _mapConfig.Height) 
                        {
                            currentY++;
                            terrainMap = Carve(terrainMap, currentX, currentY, ref fillCounter);
                        }
                        break;
                    case 1: 
                        if ((currentY - 1) > 1)
                        { 
                            currentY--;
                            terrainMap = Carve(terrainMap, currentX, currentY, ref fillCounter);
                        }
                        break;
                    case 2: 
                        if ((currentX - 1) > 1)
                        {
                            currentX--;
                            terrainMap = Carve(terrainMap, currentX, currentY, ref fillCounter);
                        }
                        break;
                    case 3: 
                        if ((currentX + 1) < _mapConfig.Width)
                        {
                            currentX++;
                            terrainMap = Carve(terrainMap, currentX, currentY, ref fillCounter);
                        }
                        break;
                }

                iterationsCounter++;
            }

            return terrainMap;
        }
        
        private int[,] FullDefaultMap()
        {
            var terrainMap = new int[_mapConfig.Width, _mapConfig.Height];

            for (int x = 0; x < _mapConfig.Width; x++)
            {
                for (int y = 0; y < _mapConfig.Height; y++)
                {
                    terrainMap[x, y] = _mapConfig.InvertGrid? MapGeneratorConstraints.Default : MapGeneratorConstraints.Terrain;
                }
            }

            return terrainMap;
        }
        
        private int[,] Carve(int[,] terrainMap, int x, int y, ref int fillCounter)
        {
            var tile = _mapConfig.InvertGrid ? MapGeneratorConstraints.Terrain : MapGeneratorConstraints.Default;
            var checkTile = _mapConfig.InvertGrid ? MapGeneratorConstraints.Default : MapGeneratorConstraints.Terrain;
            
            if (terrainMap[x, y] != checkTile) return terrainMap;
            
            terrainMap[x, y] = tile;
            fillCounter++;
            return terrainMap;
        }
    }
}