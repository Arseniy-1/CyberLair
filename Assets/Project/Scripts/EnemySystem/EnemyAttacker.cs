using System;
using System.Collections;
using UnityEngine;

namespace Project.Scripts.EnemySystem
{
    public abstract class EnemyAttacker : MonoBehaviour
    {
        private IAttackerStats _stats;
        
        protected EnemyTargetProvider EnemyTargetProvider;
        private Transform _transform;
        
        public event Action AttackPerformed;
        
        protected Vector2 Position => _transform.position;

        public void PerformAttack()
        {
            StartCoroutine(Performing());
        }
        
        public virtual void Initialize(EnemyTargetProvider enemyTargetProvider, IAttackerStats stats)
        {
            EnemyTargetProvider = enemyTargetProvider;
            _stats = stats;
            _transform = transform;
        }
        
        protected abstract void Attack();
        
        private void EndAttack()
        {
            AttackPerformed?.Invoke();
        }

        private IEnumerator Performing()
        {
            WaitForSeconds waitDelay = new WaitForSeconds(_stats.AttackDelay);
            WaitForSeconds waitRecovery = new WaitForSeconds(_stats.AttackRecovery);
            
            for (int i = 0; i < _stats.AttackCount; i++)
            {
                yield return waitDelay;
                Attack();
            }

            yield return waitRecovery;
            EndAttack();
        }
    }
}