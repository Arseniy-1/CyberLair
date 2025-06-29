using System.Collections;
using Project.Scripts.MessageBroker.CameraMessageBrokers;
using Project.Scripts.Services.Enum;
using Project.Scripts.Services.Extensions;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.PerimeterSentinel
{
    public class ShakeAttack : SpawnAttack<Shake>
    {
        [SerializeField] private ShakeID _shakeID = ShakeID.Medium;
        [SerializeField] private EnemyTargetProvider _targetProvider;

        private Vector2 _spawnPosition;
        
        public override void Initialize()
        {
            BossAttackAnimationTrigger = Animator.StringToHash("ShakeAttack");

            Spawner = new AttackInstancesSpawner<Shake>(new ShakePool(Prefab, ObjectCount));
            
            Disable();
        }

        protected override IEnumerator Attack()
        {
            View.gameObject.SetActive(true);
            
            _shakeID.Shake();

            for (int i = 0; i < ObjectCount; i++)
            {
                _spawnPosition = _targetProvider.Player.Position;
                
                Shake shake = Spawner.Spawn();
                shake.Initialize(Damage);
                shake.OnDestroyed += UnsubscribeObject;
                shake.transform.position = _spawnPosition;
                SpawnedObjects.Add(shake);
                
                var wait = new WaitForSeconds(Random.Range(SpawnPeriodLimits.x, SpawnPeriodLimits.y));
                yield return wait;
            }
        }
    }
}