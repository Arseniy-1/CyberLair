using System.Collections.Generic;
using UnityEngine;

public abstract class Pool<T> where T : MonoBehaviour
{
    private readonly List<T> _templates = new();
    protected T Prefab;
    private readonly Transform _container;
    private readonly Transform _spawnPoint;
    private readonly int _startAmount;
    private Stack<T> _stack = new();
    
    

    private int _entitiesCount = 0;

    public int EntitiesCount => _entitiesCount;
    private int PoolCount => _templates.Count;

    public Pool(T prefab, Transform container, int startAmount)
    {
        Prefab = prefab;
        _container = container;
        _spawnPoint = container;
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
            _stack.Push(Create());
            template = _stack.Pop();
        }

        return template;
    }

    protected abstract T Create();
}