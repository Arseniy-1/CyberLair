using System;
using System.Collections.Generic;
using Sirenix.Utilities;
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
        
        // [Header("Clusters")]
        // [SerializeField] private float _clusterSpacing;
        // [SerializeField] private float _maxObjectsPerCluster;

        private List<Vector3> _bannedPositions = new();
        private List<Vector2> _placedObjectsPositions = new();

        public void Place()
        {
            if(_tileObjects.Length <= 0)
            {
                return;
            }
            
            List<Vector3> availablePositions = _targetTilemap.GetTileWorldPositionsWithTiles();
            
            if (_obstacleTilemaps.Length > 0)
            {
                _bannedPositions = _obstacleTilemaps.GetUsedTileWorldPositionWithTiles();
            }
            
            foreach (Vector3 position in availablePositions)
            {
                if(_bannedPositions.Contains(position) && _bannedPositions.IsNullOrEmpty() == false)
                    continue;
                
                if(Random.value > _objectFillPercentage)
                    continue;
            
                var picker = new WeightedRandomPicker(_tileObjects);
                
                PlaceIndividual(picker.Pick(), position);
            }
        }

        private void PlaceIndividual(MapEnvironment prefab, Vector3 position)
        {
            if(_placedObjectsPositions.Contains(position))
                return;
                
            Object.Instantiate(prefab, position, Quaternion.identity);
            _placedObjectsPositions.Add(position);
            
            // var objectsCount = Random.Range(1, _maxObjectsPerCluster + 1);
            //
            // for (int i = 0; i < objectsCount; i++)
            // {
            //     var currentPosition = Random.insideUnitCircle * _clusterSpacing + (Vector2)position;
            //     
            //     if(_bannedPositions.Contains(currentPosition))
            //         continue;
            //     
            //     if(_placedObjectsPositions.Contains(currentPosition))
            //         continue;
            //     
            //     Object.Instantiate(prefab, currentPosition, Quaternion.identity);
            //     _placedObjectsPositions.Add(currentPosition);
            // }
        }
    }
}