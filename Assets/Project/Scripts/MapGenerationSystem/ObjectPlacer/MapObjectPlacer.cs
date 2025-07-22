using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Services;
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
        [SerializeField] private List<ObjectWeightPair<MapEnvironment>> _tileObjects;
        [SerializeField] [Range(0f, 1f)] private float _objectFillPercentage;

        private List<Vector3> _bannedPositions = new ();
        private List<Vector2> _placedObjectsPositions = new ();

        public void Place()
        {
            if (_tileObjects.Count <= 0)
                return;
            
            List<Vector3> availablePositions = _targetTilemap.GetTileWorldPositionsWithTiles();
            
            if (_obstacleTilemaps.Length > 0)
            {
                _bannedPositions = _obstacleTilemaps.GetUsedTileWorldPositionWithTiles();
            }
            
            foreach (Vector3 position in availablePositions)
            {
                if (_bannedPositions.Contains(position) && _bannedPositions.IsNullOrEmpty() == false)
                    continue;
                
                if (Random.value > _objectFillPercentage)
                    continue;
            
                var picker = new WeightedRandomPicker<MapEnvironment>(
                    _tileObjects.Select(pair => pair.Prefab).ToList(),
                    _tileObjects.Select(pair => pair.Weight).ToList());
                
                PlaceIndividual(picker.Pick(), position);
            }
        }

        private void PlaceIndividual(MapEnvironment prefab, Vector3 position)
        {
            if (_placedObjectsPositions.Contains(position))
                return;
                
            Object.Instantiate(prefab, position, Quaternion.identity);
            
            _placedObjectsPositions.Add(position);
        }
    }
}