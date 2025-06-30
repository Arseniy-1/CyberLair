using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Project.Scripts.MapGenerationSystem
{
    [Serializable]
    public class MapLayer 
    {
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private TileBase _terrainTile;
        [SerializeField] private TileBase _defaultTile;

        [field: SerializeField] public MapConfig Config { get; private set; }

        public void Render(int[,] tiles)
        {
            for (int x = 0; x < Config.Width; x++)
            {
                for (int y = 0; y < Config.Height; y++)
                {
                    var position = new Vector3Int(-x + Config.Width / 2, -y + Config.Height / 2, 0);

                    if (_tilemap.HasTile(position))
                        return;
                    
                    if (tiles[x, y] == MapGeneratorConstraints.Terrain)
                        _tilemap.SetTile(position, _terrainTile);

                    if (tiles[x, y] == MapGeneratorConstraints.Default && _defaultTile)
                        _tilemap.SetTile(position, _defaultTile);
                }
            }
        }
    }
}