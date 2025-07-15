using Project.Scripts.EnemySystem;

namespace Project.Scripts.MessageBroker.EnemyMessageBrokers
{
    public struct M_BossSpawned
    {
        public M_BossSpawned(Enemy enemy)
        {
            Boss = enemy;
        }
    
        public Enemy Boss { get; private set; }
    }
}