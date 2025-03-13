using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.PerimeterSentinel
{
    public class ShakeAttack : BossAttack
    {
        [SerializeField] private Shake _shakePrefab;
        [SerializeField, MinMaxSlider(5, 20)] private Vector2Int _spawnLimits;
        [SerializeField, MinMaxSlider(0.1f, 0.5f)] private Vector2 _spawnPeriodLimits;
        
        private readonly List<Shake> _shakes = new();
        private ShakeSpawner _spawner;

        public override void Initialize()
        {
            BossAttackAnimationTrigger = Animator.StringToHash("ShakeAttack");

            _spawner = new ShakeSpawner(_shakePrefab);
            Disable();
        }

        protected override void Disable()
        {
            View.gameObject.SetActive(false);
            
            foreach (Shake shake in _shakes)
            {
                shake.OnDestroyed -= UnsubscribeShake;
                shake.ReturnToPool();
            }
            
            _shakes.Clear();
        }

        protected override IEnumerator Attack()
        {
            int shakeCount = Random.Range(_spawnLimits.x, _spawnLimits.y);
            View.gameObject.SetActive(true);
            AttackAnimator.SetTrigger(AttackTrigger);

            for (int i = 0; i < shakeCount; i++)
            {
                Shake shake = _spawner.Spawn();
                shake.Initialize(Damage);
                shake.OnDestroyed += UnsubscribeShake;
                shake.transform.position = Random.insideUnitCircle * Range + (Vector2)transform.position;
                _shakes.Add(shake);
                
                var wait = new WaitForSeconds(Random.Range(_spawnPeriodLimits.x, _spawnPeriodLimits.y));
                yield return wait;
            }
        }

        private void UnsubscribeShake(Shake shake)
        {
            shake.OnDestroyed -= UnsubscribeShake;
            _shakes.Remove(shake);
        }
    }
}