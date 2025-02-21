using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Project.Scripts.MapGenerationSystem.ObjectPlacer
{
    [Serializable]
    public class MapObjectPlacer
    {
        [Header("Tilemap and Objects")]
        [SerializeField] private Tilemap _targetTilemap;
        [SerializeField] private Tilemap[] _obstacleTilemaps;
        [SerializeField] private TileObject[] _tileObjects;
        [SerializeField, Range(0f, 1f)] private float _objectFillPercentage;
        
        [Header("Clusters")]
        [SerializeField] private float _clusterSpacing;
        [SerializeField] private float _maxObjectsPerCluster;

        private List<Vector3> _bannedPositions = new();
        private List<Vector3> _placedObjectsPositions = new();

        public void PlaceObjects()
        {
            List<Vector3> availablePositions = _targetTilemap.GetTileWorldPositionsWithTiles();
            
            if(_obstacleTilemaps.Length > 0)
                _bannedPositions = _obstacleTilemaps.GetUsedTileWorldPositionWithTiles();

            foreach (Vector3 position in availablePositions)
            {
                if(_bannedPositions.Contains(position))
                    continue;
                
                if(Random.value < _objectFillPercentage)
                    continue;

                var picker = new WeightedRandomPicker(_tileObjects);
                
                PlaceCluster(picker.Pick(), position);
            }
        }

        private void PlaceCluster(MapEnvironment prefab, Vector3 position)
        {
            var objectsCount = Random.Range(1, _maxObjectsPerCluster + 1);

            for (int i = 0; i < objectsCount; i++)
            {
                var currentPosition = Random.insideUnitCircle * _clusterSpacing + (Vector2)position;
                
                if(_bannedPositions.Contains(currentPosition))
                    continue;
                
                if(_placedObjectsPositions.Contains(currentPosition))
                    continue;
                
                Object.Instantiate(prefab, position, Quaternion.identity);
                _placedObjectsPositions.Add(currentPosition);
            }
        }
    }
}