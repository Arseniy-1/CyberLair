using System.Collections.Generic;
using UnityEngine;

public class BulletPool : Pool<Bullet>
{
    public BulletPool(Bullet prefab) : base(prefab)
    {
        CreateStartCount();
    }

    protected override Bullet Create()
    {
        Bullet template = Object.Instantiate(Prefab);
        template.gameObject.SetActive(false);
        Stack.Push(template);

        return template;
    }
}