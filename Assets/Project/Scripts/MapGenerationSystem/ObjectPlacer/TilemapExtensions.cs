using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Project.Scripts.MapGenerationSystem.ObjectPlacer
{
    public static class TilemapExtensions
    {
        public static List<Vector3> GetTileWorldPositionsWithTiles(this Tilemap tilemap)
        {
            var tilesWorldPositions = new List<Vector3>();
            
            BoundsInt.PositionEnumerator tilesLocalPositions = tilemap.cellBounds.allPositionsWithin;

            foreach (Vector3Int localPosition in tilesLocalPositions)
            {
                if (tilemap.HasTile(localPosition))
                    tilesWorldPositions.Add(tilemap.CellToWorld(localPosition));
            }

            return tilesWorldPositions;
        }

        public static List<Vector3> GetUsedTileWorldPositionWithTiles(this Tilemap[] tilemaps)
        {
            var tilesWorldPositions = new List<Vector3>();

            foreach (Tilemap tilemap in tilemaps)
            {
                tilesWorldPositions.AddRange(tilemap.GetTileWorldPositionsWithTiles());
            }

            return tilesWorldPositions;
        }
    }
}