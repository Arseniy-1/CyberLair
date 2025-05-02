using System;
using System.Collections;
using Project.Scripts.EnemySystem.AttackTypes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Scripts.EnemySystem
{
    public abstract class EnemyAttacker : MonoBehaviour
    {
        [SerializeField] private BaseEnemyAttackStats _stats;
        
        protected EnemyTargetProvider EnemyTargetProvider;
        private Transform _transform;

        public event Action AttackStarted;
        public event Action<bool> AttackPerforming;
        public event Action AttackPerformed;
        
        public virtual BaseEnemyAttackStats Stats => _stats;
        protected Vector2 Position => _transform.position;

        public void PerformAttack()
        {
           StartCoroutine(Performing());
        }
        
        public virtual void Initialize(EnemyTargetProvider enemyTargetProvider)
        {
            EnemyTargetProvider = enemyTargetProvider;
            _transform = transform;
        }
        
        protected abstract IEnumerator Attack();
        
        private void EndAttack()
        {
            AttackPerformed?.Invoke();
        }

        private IEnumerator Performing()
        {
            var waitDelay = new WaitForSeconds(_stats.AttackDelay);
            var waitRecovery = new WaitForSeconds(_stats.AttackRecovery);
            
            yield return waitDelay;

            for (int i = 0; i < _stats.AttackCount; i++)
            {
                AttackStarted?.Invoke();
                yield return waitRecovery;

                AttackPerforming?.Invoke(true);
                yield return Attack();

                AttackPerforming?.Invoke(false);
            }
            
            yield return waitDelay;

            EndAttack();
        }
    }
}