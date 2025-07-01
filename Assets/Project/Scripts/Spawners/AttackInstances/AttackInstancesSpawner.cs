using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses
{
    public class AttackInstancesSpawner<T> : Spawner<T> where T : MonoBehaviour, IDestoyable<T>, IReturnable
    {
        public AttackInstancesSpawner(Pool<T> pool)
        {
            Pool = pool;
        }
    }
}