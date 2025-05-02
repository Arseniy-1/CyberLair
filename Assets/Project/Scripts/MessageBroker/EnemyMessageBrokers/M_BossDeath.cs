using Project.Scripts.EnemySystem;

namespace Project.Scripts.MessageBroker.EnemyMessageBrokers
{
    public struct M_BossDeath
    {
        public M_BossDeath(Enemy enemy)
        {
            Boss = enemy;
        }
    
        public Enemy Boss { get; private set; }
    }
}