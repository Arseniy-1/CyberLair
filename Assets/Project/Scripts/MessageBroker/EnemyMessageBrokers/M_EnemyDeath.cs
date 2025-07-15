using UnityEngine;

namespace Project.Scripts.MessageBroker.EnemyMessageBrokers
{
    public struct M_EnemyDeath
    {
        public M_EnemyDeath(Vector2 position)
        {
            Position = position;
        }
    
        public Vector2 Position { get; private set; }
    }
}