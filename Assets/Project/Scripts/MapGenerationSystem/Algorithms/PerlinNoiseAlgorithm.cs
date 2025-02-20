using UnityEngine;

namespace Project.Scripts.MapGenerationSystem.Algorithms
{
    public class PerlinNoiseAlgorithm : IMapAlgorithm
    {
        private readonly PerlinNoiseMapConfig _mapConfig;

        public PerlinNoiseAlgorithm(PerlinNoiseMapConfig mapConfig)
        {
            _mapConfig = mapConfig;
        }

        public int[,] RandomFillMap()
        {
            var terrainMap = new int[_mapConfig.Width, _mapConfig.Height];
            var halfWidth = _mapConfig.Width / 2f;
            var halfHeight = _mapConfig.Height / 2f;
            var innerPercentage = 1f - _mapConfig.FillPercent;

            var noiseMap = new float[_mapConfig.Width, _mapConfig.Height];

            for (int x = 0; x < _mapConfig.Width; x++)
            {
                for (int y = 0; y < _mapConfig.Height; y++)
                {
                    float xCoord = (x - halfWidth) / _mapConfig.Width * _mapConfig.NoiseScale + _mapConfig.NoiseOffset.x;
                    float yCoord = (y - halfHeight) / _mapConfig.Height * _mapConfig.NoiseScale + _mapConfig.NoiseOffset.y;

                    float noiseValue = Mathf.PerlinNoise(xCoord, yCoord);
                    noiseMap[x, y] = noiseValue;
                }
            }

            for (int x = 0; x < _mapConfig.Width; x++)
            {
                for (int y = 0; y < _mapConfig.Height; y++)
                {
                    float noiseValue = noiseMap[x, y];
                    terrainMap[x, y] = GetTile(noiseValue, innerPercentage);
                }
            }

            return terrainMap;
        }

        private int GetTile(float sample, float percentage)
        {
            return percentage < sample ? MapGeneratorConstraints.Terrain : MapGeneratorConstraints.Default;
        }
    }
}