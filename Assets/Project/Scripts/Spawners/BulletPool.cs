using System.Collections.Generic;
using UnityEngine;

public class BulletPool : Pool<Bullet>
{
    private readonly List<Bullet> _templates = new();
    
    public BulletPool(Bullet prefab) : base(prefab)
    {
        CreateStartCount();
    }

    protected override Bullet Create()
    {
        Bullet template = Object.Instantiate(Prefab);
        template.gameObject.SetActive(false);
        _templates.Add(template);
        Stack.Push(template);

        return template;
    }
}