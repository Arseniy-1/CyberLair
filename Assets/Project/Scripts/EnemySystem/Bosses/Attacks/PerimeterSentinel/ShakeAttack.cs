using System.Collections;
using Project.Scripts.MessageBroker.CameraMessageBrokers;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.PerimeterSentinel
{
    public class ShakeAttack : SpawnAttack<Shake>
    {
        [SerializeField] private CameraShakeSettings _cameraShakeSettings;
        
        public override void Initialize()
        {
            BossAttackAnimationTrigger = Animator.StringToHash("ShakeAttack");

            Spawner = new AttackInstancesSpawner<Shake>(new ShakePool(Prefab, ObjectCount));
            
            Disable();
        }

        protected override IEnumerator Attack()
        {
            View.gameObject.SetActive(true);
            
            MessageBrokerHolder.Camera.Publish(new M_CameraShake(_cameraShakeSettings));

            for (int i = 0; i < ObjectCount; i++)
            {
                Shake shake = Spawner.Spawn();
                shake.Initialize(Damage);
                shake.OnDestroyed += UnsubscribeObject;
                shake.transform.position = Random.insideUnitCircle * Range + (Vector2)transform.position;
                SpawnedObjects.Add(shake);
                
                var wait = new WaitForSeconds(Random.Range(SpawnPeriodLimits.x, SpawnPeriodLimits.y));
                yield return wait;
            }
        }
    }
}