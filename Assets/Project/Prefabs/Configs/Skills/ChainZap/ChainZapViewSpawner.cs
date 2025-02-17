namespace Project.Prefabs.Configs.Skills.Zap
{
    public class ChainZapViewSpawner : Spawner<ChainZapView>
    {
        public ChainZapViewSpawner(ChainZapView view, int startCount)
        {
            Pool = new ChainZapViewPool(view, startCount);
        }
    }
}