using UnityEngine;

namespace Project.Scripts.MapGenerationSystem.ObjectPlacer
{
    public class WeightedRandomPicker
    {
        private readonly TileObject[] _tileObjects;
        private readonly float _totalWeight = 0;

        public WeightedRandomPicker(TileObject[] tileObjects)
        {
            _tileObjects = tileObjects;
            
            foreach (TileObject tileObject in tileObjects)
            {
                _totalWeight += tileObject.Weight;
            }
        }

        public MapEnvironment Pick()
        {
            float pickedWeight = Random.value * _totalWeight;
            float partialWeight = 0f;

            foreach (TileObject tileObject in _tileObjects)
            {
                partialWeight += tileObject.Weight;
                
                if(partialWeight > pickedWeight)
                    return tileObject.Prefab;
            }
            
            return _tileObjects[0].Prefab;
        }
    }
}