using UnityEngine;

namespace Project.Scripts.CompositionRoot
{
    public class EnemyFabric
    {
        public Enemy Create(Enemy enemy)
        {
            return enemy;
        }
    }
}