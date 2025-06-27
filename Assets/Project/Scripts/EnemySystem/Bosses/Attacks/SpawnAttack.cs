using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses
{
    public abstract class SpawnAttack<T> : BossAttack where T : MonoBehaviour, IDestoyable<T>, IReturnable
    {
        [SerializeField] protected T Prefab;
        [SerializeField] protected int ObjectCount;
        [SerializeField, MinMaxSlider(0.1f, 0.5f)] protected Vector2 SpawnPeriodLimits;
        
        protected AttackInstancesSpawner<T> Spawner;
        protected readonly List<T> SpawnedObjects = new();

        public override void Disable()
        {
            View.gameObject.SetActive(false);
            
            if(SpawnedObjects.IsNullOrEmpty())
                return;
            
            foreach (T spawnedObject in SpawnedObjects.ToList())
            {
                spawnedObject.gameObject.SetActive(false);
                UnsubscribeObject(spawnedObject);
            }
        }
        
        protected virtual void UnsubscribeObject(T spawnedObject)
        {
            spawnedObject.OnDestroyed -= UnsubscribeObject;
            // spawnedObject.ReturnToPool();
            SpawnedObjects.Remove(spawnedObject);
        }
    }
}