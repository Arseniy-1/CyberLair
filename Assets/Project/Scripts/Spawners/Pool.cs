using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    private readonly List<T> _templates = new();
    private readonly T _prefab;
    private readonly Transform _container;
    private readonly Transform _spawnPoint;
    private readonly int _startAmount;
    private Stack<T> _stack = new();

    private int _entitiesCount = 0;

    public int EntitiesCount => _entitiesCount;
    private int PoolCount => _templates.Count;

    public Pool(T prefab, Transform container, Transform spawnPoint, int startAmount)
    {
        _prefab = prefab;
        _container = container;
        _spawnPoint = spawnPoint;
        _startAmount = startAmount;

        for (int i = 0; i < _startAmount; i++)
        {
            Create();
        }
    }

    public void Release(T template)
    {
        _stack.Push(template);
    }

    public T Get()
    {
        if (_stack.TryPop(out T template) == false)
        {
            Create();
            template = _stack.Pop();
        }

        return template;
    }

    private T Create()
    {
        T template = UnityEngine.Object.Instantiate(_prefab, _container, _spawnPoint);
        _entitiesCount++;
        template.gameObject.SetActive(false);
        _templates.Add(template);
        _stack.Push(template);

        return template;
    }
}
