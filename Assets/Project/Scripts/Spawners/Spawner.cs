using System;
using UnityEngine;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour, IDestoyable<T>
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _startAmount = 1;

    protected Pool<T> _pool;

    public event Action<int, int, int> CounterChanged;

    public Type PrefabType => _prefab.GetType();

    protected virtual void Awake()
    {
        _pool = new Pool<T>(_prefab, transform, transform, _startAmount);
    }

    public T Spawn()
    {
        T spawnedObject = _pool.Get();

        spawnedObject.OnDestroyed += OnSpawnedDestroy;
        spawnedObject.gameObject.SetActive(true);

        return spawnedObject;
    }

    protected void OnSpawnedDestroy(T spawnableObject)
    {
        spawnableObject.OnDestroyed -= OnSpawnedDestroy;
        spawnableObject.gameObject.SetActive(false);
        _pool.Release(spawnableObject);
    }
}
