using System;
using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.Spawners
{
    [Serializable]
    public class Spawner<T> 
        where T : MonoBehaviour, IDestoyable<T>
    {
        [SerializeField] protected int StartAmount = 5;

        protected T Prefab;
        protected Pool<T> Pool;

        public T Spawn()
        {
            T spawnedObject = Pool.Get();
        
            spawnedObject.OnDestroyed += OnSpawnedDestroyed;

            return spawnedObject;
        }

        protected void OnSpawnedDestroyed(T spawnableObject)
        {
            spawnableObject.OnDestroyed -= OnSpawnedDestroyed;
        
            Pool.Release(spawnableObject);
        }
    }
}