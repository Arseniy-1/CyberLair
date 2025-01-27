using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Scripts.CompositionRoot
{
    [Serializable]
    public class EdgeSpawner
    {
        [Header("Settings")]
        [SerializeField] private float _offset;
        [SerializeField] private int _horizontalObjectCount;
        [SerializeField] private int _verticalObjectCount;
        [SerializeField] private float _spawnPointPrefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private Camera _mainCamera;

        private Vector2 _cameraCenter;
        private float _width;
        private float _height;
        
        private List<Transform> _edgeObjects = new();
        
        public IReadOnlyList<Transform> EdgeObjects => _edgeObjects;
        
        public void SpawnOnEdges()
        {
            CalculateCameraBounds();

            var edges = new (Vector2 start, Vector2 end, int count)[]
            {
                (new Vector2(_cameraCenter.x - _width, _cameraCenter.y + _height), new Vector2(_cameraCenter.x + _width, _cameraCenter.y + _height), _horizontalObjectCount),
                (new Vector2(_cameraCenter.x - _width, _cameraCenter.y - _height), new Vector2(_cameraCenter.x + _width, _cameraCenter.y - _height), _horizontalObjectCount),
                
                (new Vector2(_cameraCenter.x - _width, _cameraCenter.y - _height), new Vector2(_cameraCenter.x - _width, _cameraCenter.y + _height), _verticalObjectCount),
                (new Vector2(_cameraCenter.x + _width, _cameraCenter.y - _height), new Vector2(_cameraCenter.x + _width, _cameraCenter.y + _height), _verticalObjectCount)
            };

            foreach ((Vector2 start, Vector2 end, int count) edge in edges)
            {
                SpawnLine(edge.start, edge.end, edge.count);
            }
        }

        private void SpawnLine(Vector2 start, Vector2 end, int count)
        {
            var direction = end - start;
            var step = 1f / (count - 1);

            for (int i = 0; i < count; i++)
            {
                Vector2 position = start + direction * step * i;

                var edgeObject = new GameObject($"EdgeObject {i}")
                {
                    transform =
                    {
                        parent = _parent,
                        position = position
                    }
                };
                
                _edgeObjects.Add(edgeObject.transform);
                // _edgeObjects.Add(Object.Instantiate(_spawnPointPrefab, position, Quaternion.identity, _parent).transform);
            }
        }

        private void CalculateCameraBounds()
        {
            var aspectRatio = (float)Screen.width / Screen.height;
            _cameraCenter = _mainCamera.transform.position;
            
            _width = _mainCamera.orthographicSize * aspectRatio + _offset;
            _height = _mainCamera.orthographicSize + _offset;
        }
    }
}