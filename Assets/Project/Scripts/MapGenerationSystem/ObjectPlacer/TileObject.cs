using System;
using UnityEngine;

namespace Project.Scripts.MapGenerationSystem.ObjectPlacer
{
    [Serializable]
    public struct TileObject
    {
        [SerializeField] private MapEnvironment _prefab;
        [SerializeField] private float _weight;
        
        public MapEnvironment Prefab => _prefab;
        public float Weight => _weight;
    }
}