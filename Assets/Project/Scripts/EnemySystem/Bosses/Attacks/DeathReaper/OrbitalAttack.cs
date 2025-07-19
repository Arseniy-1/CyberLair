using System.Collections;
using Project.Scripts.Services;
using Project.Scripts.Spawners.AttackInstances;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.Attacks.DeathReaper
{
    public class OrbitalAttack : SpawnAttack<SoulOrbital>
    {
        private readonly OrbitalHandler _orbitalHandler = new();
        
        private Transform _transform;
        
        public override void Initialize()
        {
            BossAttackAnimationTrigger = Animator.StringToHash("OrbitalAttack");
            
            Spawner = new AttackInstancesSpawner<SoulOrbital>(new SoulOrbitalPool(Prefab, ObjectCount));
            _transform = transform;
            
            Disable();
        }

        protected override IEnumerator Attack()
        {
            for (int i = 0; i < ObjectCount; i++)
            {
                SoulOrbital orbital = Spawner.Spawn();
                orbital.OnDestroyed += UnsubscribeObject;
                orbital.Initialize(_transform);
                
                SpawnedObjects.Add(orbital);
                _orbitalHandler.AddOrbital(orbital, _transform);
                
                var wait = new WaitForSeconds(Random.Range(SpawnPeriodLimits.x, SpawnPeriodLimits.y));
                
                yield return wait;
            }
            
            View.gameObject.SetActive(false);
        }

        protected override void UnsubscribeObject(SoulOrbital spawnedObject)
        {
            base.UnsubscribeObject(spawnedObject);
            
            _orbitalHandler.RemoveOrbital(spawnedObject);
        }
    }
}