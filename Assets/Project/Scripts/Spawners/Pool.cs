using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Pool<T> where T : MonoBehaviour
{
    private int _startAmount;

    protected T Prefab;
    protected Stack<T> Stack = new();

    public Pool(T prefab, int startAmount)
    {
        Prefab = prefab;
        _startAmount = startAmount;
    }

    public void Release(T template)
    {
        template.gameObject.SetActive(false); 
        Stack.Push(template);
    }

    public T Get()
    {
        if (Stack.TryPop(out T template) == false)
        {
            Stack.Push(Create());
            template = Stack.Pop();
        }
        
        template.gameObject.SetActive(true);

        return template;
    }
    
    protected void CreateStartCount()
    {
        for (int i = 0; i < _startAmount; i++)
        {
            Create();
        }
    }

    protected abstract T Create();
}