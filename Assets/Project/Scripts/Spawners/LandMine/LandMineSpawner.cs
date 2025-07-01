public class LandMineSpawner : Spawner<LandMine>
{
    public LandMineSpawner(LandMine prefab)
    {
        Prefab = prefab;
        Pool = new LandMinePool(prefab, StartAmount);
    }
}