using System.Collections.Generic;
using UnityEngine;

public class BulletPool : Pool<Bullet>
{
    public BulletPool(Bullet prefab, int startAmount) : base(prefab, startAmount)
    {
    }

    protected override Bullet Create()
    {
        Bullet template = Object.Instantiate(Prefab);
        template.gameObject.SetActive(false);

        return template;
    }
}