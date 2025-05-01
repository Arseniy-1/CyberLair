using System.Collections;
using System.Collections.Generic;
using Project.Prefabs.Configs.Skills.FireZone;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.FireColossus
{
    public class FireBreathingAttack : SpawnAttack<FireZone>
    {
        public override void Initialize()
        {
            BossAttackAnimationTrigger = Animator.StringToHash("FireBreathingAttack");
            
            Spawner = new AttackInstancesSpawner<FireZone>(new FireZonePool(Prefab, ObjectCount));
            
            Disable();
        }

        protected override IEnumerator Attack()
        {
            for (int i = 0; i < ObjectCount; i++)
            {
                FireZone soulClot = Spawner.Spawn();
                soulClot.transform.position = transform.position;
                
                soulClot.OnDestroyed += UnsubscribeObject;
                
                SpawnedObjects.Add(soulClot);
                
                var wait = new WaitForSeconds(Random.Range(SpawnPeriodLimits.x, SpawnPeriodLimits.y));
                yield return wait;
            }
            
            yield return null;
        }
    }
}