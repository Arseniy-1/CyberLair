using Project.Scripts.SkillSystem.SkillViews;

namespace Project.Scripts.Spawners.ChainZap
{
    public class ChainZapViewSpawner : Spawner<ChainZapView>
    {
        public ChainZapViewSpawner(ChainZapView view, int startCount)
        {
            Pool = new ChainZapViewPool(view, startCount);
        }
    }
}