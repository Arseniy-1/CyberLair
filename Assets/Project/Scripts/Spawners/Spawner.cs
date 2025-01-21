using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Spawner<T> where T : MonoBehaviour, IDestoyable<T>
{
    protected T Prefab;
    [SerializeField] protected int StartAmount = 5;

    [SerializeField] protected Pool<T> Pool;

    public event Action<int, int, int> CountChanged;

    public T Spawn()
    {
        T spawnedObject = Pool.Get();

        spawnedObject.OnDestroyed += OnSpawnedDestroyed;

        return spawnedObject;
    }

    protected void OnSpawnedDestroyed(T spawnableObject)
    {
        spawnableObject.OnDestroyed -= OnSpawnedDestroyed;
        spawnableObject.gameObject.SetActive(false);
        Pool.Release(spawnableObject);
    }
}