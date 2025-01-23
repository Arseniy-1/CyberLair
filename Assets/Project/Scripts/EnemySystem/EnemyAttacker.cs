using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.EnemySystem
{
    public abstract class EnemyAttacker : MonoBehaviour
    {
        [SerializeField] protected int Damage;
        [SerializeField] private float _attackDelay;
        [SerializeField] private float _attackRecovery;
        [SerializeField] private int _attackCount;
        
        protected EnemyTargetProvider EnemyTargetProvider;
        protected EnemyStats Stats;
        private Transform _transform;
        
        public event Action AttackPerformed;
        
        protected Vector2 Position => _transform.position;

        public void PerformAttack()
        {
            StartCoroutine(Performing());
        }
        
        public virtual void Initialize(EnemyTargetProvider enemyTargetProvider, EnemyStats stats)
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
            WaitForSeconds waitDelay = new WaitForSeconds(_attackDelay);
            WaitForSeconds waitRecovery = new WaitForSeconds(_attackRecovery);
            
            for (int i = 0; i < _attackCount; i++)
            {
                yield return waitDelay;
                Attack();
            }

            yield return waitRecovery;
            EndAttack();
        }
    }
}