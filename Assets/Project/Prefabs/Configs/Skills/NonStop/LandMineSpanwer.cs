public class LandMineSpanwer : Spawner<LandMine>
{
    public LandMineSpanwer(LandMine prefab)
    {
        Prefab = prefab;
        Pool = new LandMinePool(prefab, StartAmount);
    }
}