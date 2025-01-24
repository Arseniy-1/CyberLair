using System;
using UnityEngine;

[Serializable]
public class AmmoSpawner : Spawner<Bullet>
{
    public AmmoSpawner(Bullet bulletPrefab)
    {
        Prefab = bulletPrefab;
        Pool = new BulletPool(Prefab, StartAmount);
    }
}