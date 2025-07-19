using Project.Scripts.SkillSystem.SkillViews;

namespace Project.Scripts.Spawners.HellCats
{
    public class HellCatSpawner : Spawner<HellCat>
    {
        public HellCatSpawner(HellCat prefab)
        {
            Prefab = prefab;
            Pool = new HellCatPool(Prefab, StartAmount);
        }
    }
}