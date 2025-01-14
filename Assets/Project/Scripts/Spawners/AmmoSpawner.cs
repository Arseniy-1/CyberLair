public class AmmoSpawner : Spawner<Bullet>
{
    private void Awake()
    {
        Pool = new BulletPool(Prefab);
    }
}