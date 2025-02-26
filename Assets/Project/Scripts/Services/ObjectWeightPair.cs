using System;
using UnityEngine;

namespace Project.Scripts.Servises
{
    [Serializable]
    public class ObjectWeightPair<T> where T : class
    {
        [SerializeField] private T _prefab;
        [SerializeField, Range(0f, 1f)] private float _weight;
        
        public T Prefab => _prefab;
        public float Weight => _weight;
    }
}