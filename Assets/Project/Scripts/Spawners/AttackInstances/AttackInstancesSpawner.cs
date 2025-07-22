using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.Spawners.AttackInstances
{
    public class AttackInstancesSpawner<T> : Spawner<T> 
        where T : MonoBehaviour, IDestoyable<T>, IReturnable
    {
        public AttackInstancesSpawner(Pool<T> pool)
        {
            Pool = pool;
        }
    }
}