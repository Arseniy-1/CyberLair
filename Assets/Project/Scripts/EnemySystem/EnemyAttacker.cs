using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.EnemySystem
{
    public abstract class EnemyAttacker : MonoBehaviour
    {
        protected IAttackerStats Stats;
        
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
            Stats = stats;
            _transform = transform;
        }
        
        protected abstract void Attack();
        
        protected virtual void EndAttack()
        {
            AttackPerformed?.Invoke();
        }

        private IEnumerator Performing()
        {
            WaitForSeconds waitDelay = new WaitForSeconds(Stats.AttackDelay);
            WaitForSeconds waitRecovery = new WaitForSeconds(Stats.AttackRecovery);
            
            for (int i = 0; i < Stats.AttackCount; i++)
            {
                yield return waitDelay;
                Attack();
            }

            yield return waitRecovery;
            EndAttack();
        }
    }
}