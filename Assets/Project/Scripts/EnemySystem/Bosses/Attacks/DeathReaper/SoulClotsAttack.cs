using System.Collections;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.DeathReaper
{
    public class SoulClotsAttack : SpawnAttack<SoulClot>
    {
        public override void Initialize()
        {
            BossAttackAnimationTrigger = Animator.StringToHash("SoulClotsAttack");
            
            Spawner = new AttackInstancesSpawner<SoulClot>(new SoulClotsPool(Prefab, ObjectCount));
            
            Disable();
        }

        protected override IEnumerator Attack()
        {
            View.gameObject.SetActive(true);
            AttackAnimator.SetTrigger(AttackTrigger);

            for (int i = 0; i < ObjectCount; i++)
            {
                SoulClot soulClot = Spawner.Spawn();
                soulClot.transform.position = transform.position;
                
                Vector3 randomPositionInRange = Random.insideUnitCircle * Range + (Vector2)transform.position;
                soulClot.Initialize(randomPositionInRange);
                soulClot.Move();
                
                soulClot.OnDestroyed += UnsubscribeObject;
                
                SpawnedObjects.Add(soulClot);
                
                var wait = new WaitForSeconds(Random.Range(SpawnPeriodLimits.x, SpawnPeriodLimits.y));
                yield return wait;
            }
        }
    }
}