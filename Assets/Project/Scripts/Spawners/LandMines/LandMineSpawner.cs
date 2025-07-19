using Project.Scripts.SkillSystem.SkillViews;

namespace Project.Scripts.Spawners.LandMines
{
    public class LandMineSpawner : Spawner<LandMine>
    {
        public LandMineSpawner(LandMine prefab)
        {
            Prefab = prefab;
            Pool = new LandMinePool(prefab, StartAmount);
        }
    }
}