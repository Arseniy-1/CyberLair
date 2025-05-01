using System.Collections;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.FireColossus
{
    public class LavaWaveAttack : SpawnAttack<Bullet>
    {
        public override void Initialize()
        {
            BossAttackAnimationTrigger = Animator.StringToHash("LavaWaveAttack");
            
            Spawner = new AttackInstancesSpawner<Bullet>(new BulletPool(Prefab, ObjectCount));
            
            Disable();
        }

        protected override IEnumerator Attack()
        {
            for (int i = 0; i < ObjectCount; i++)
            {
                float angle = i * Mathf.PI * 2 / ObjectCount;
                Quaternion rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
                
                Bullet lavaWave = Spawner.Spawn();
                lavaWave.Init(transform.position, rotation, Damage);
                lavaWave.Activate();
                
                lavaWave.OnDestroyed += UnsubscribeObject;
                
                SpawnedObjects.Add(lavaWave);
                
                var wait = new WaitForSeconds(Random.Range(SpawnPeriodLimits.x, SpawnPeriodLimits.y));
                yield return wait;
            }
        }
    }
}