using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour, IDestoyable<T>
{
    [SerializeField] protected T Prefab;
    [SerializeField] protected int StartAmount = 1;

    protected Pool<T> Pool;

    public event Action<int, int, int> CountChanged;
    

    public T Spawn()
    {
        Debug.Log($"Spawning {Prefab.name}");
        
        T spawnedObject = Pool.Get();

        spawnedObject.OnDestroyed += OnSpawnedDestroed;
        spawnedObject.gameObject.SetActive(true);

        return spawnedObject;
    }

    protected void OnSpawnedDestroed(T spawnableObject)
    {
        spawnableObject.OnDestroyed -= OnSpawnedDestroed;
        spawnableObject.gameObject.SetActive(false);
        Pool.Release(spawnableObject);
    }
}