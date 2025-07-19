using System;
using Project.Scripts.Weapon;

namespace Project.Scripts.Spawners.Ammo
{
    [Serializable]
    public class AmmoSpawner : Spawner<Bullet>
    {
        public AmmoSpawner(Bullet bulletPrefab)
        {
            Prefab = bulletPrefab;
            Pool = new BulletPool(Prefab, StartAmount);
        }
    }
}