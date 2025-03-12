namespace Project.Scripts.EnemySystem.Bosses.PerimeterSentinel
{
    public class ShakeSpawner : Spawner<Shake>
    {
        public ShakeSpawner(Shake prefab)
        {
            Pool = new ShakePool(prefab, StartAmount);
        }
    }
}