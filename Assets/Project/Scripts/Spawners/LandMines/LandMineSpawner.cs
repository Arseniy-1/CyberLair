using Project.Prefabs.Configs.Skills.NonStop;

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