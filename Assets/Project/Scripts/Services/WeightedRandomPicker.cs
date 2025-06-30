using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Servises
{
    public class WeightedRandomPicker<T> where T : class
    {
        private readonly List<T> _prefabs;
        private readonly List<float> _weights;
        private readonly float _totalWeight;

        public WeightedRandomPicker(List<T> prefabs, List<float> weights)
        {
            _prefabs = prefabs;
            _weights = weights;
            
            foreach (float weight in _weights)
            {
                _totalWeight += weight;
            }
        }

        public T Pick()
        {
            float pickedWeight = Random.value * _totalWeight;
            float partialWeight = 0f;

            for (int i = 0; i < _weights.Count; i++)
            {
                partialWeight += _weights[i];
                
                if(partialWeight > pickedWeight)
                    return _prefabs[i];
            }
            
            return _prefabs[0];
        }
    }
}