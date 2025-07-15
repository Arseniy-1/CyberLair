using Project.Prefabs.Configs.Skills.ChainZap;

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