public class HellCatSpawner : Spawner<HellCat>
{
    public HellCatSpawner(HellCat prefab)
    {
        Prefab = prefab;
        Pool = new HellCatPool(Prefab, StartAmount);
    }
}