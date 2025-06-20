using System;
using UnityEngine;

[Serializable]
public class Spawner<T> where T : MonoBehaviour, IDestoyable<T>
{
    protected T Prefab;
    [SerializeField] protected int StartAmount = 5;

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