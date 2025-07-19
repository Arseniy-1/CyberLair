using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillInstances
{
    public class FireZone : MonoBehaviour, IDestoyable<FireZone>, IReturnable
    {
        [SerializeField] private int _damagePerIteration = 2;
        [SerializeField] private float _burnInterval = 1f;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _lifeTime = 10f;

        private readonly List<IDamageable> _damageableTargets = new();
        private float _currentTime;

        private Coroutine _waitingDestroy;
        private WaitForSeconds _waitForLifetime;

        public event Action<FireZone> OnDestroyed;

        private void OnEnable()
        {
            EndWaitingDestroy();

            _waitForLifetime ??= new WaitForSeconds(_lifeTime);
            
            _waitingDestroy = StartCoroutine(WaitingDestroy());
        }
    
        private void FixedUpdate()
        {
            if (Time.time < _currentTime) 
                return;
        
            ApplyFireDamage();
            _currentTime = Time.time + _burnInterval;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamageable damagable) & _targetLayer == collision.gameObject.layer)
            {
                _damageableTargets.Add(damagable);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamageable damagable) & _targetLayer == collision.gameObject.layer)
            {
                _damageableTargets.Remove(damagable);
            }
        }

        private void OnDisable()
        {
            EndWaitingDestroy();
            ReturnToPool();
        }
        
        public void ReturnToPool()
        {
            OnDestroyed?.Invoke(this);
        }

        private void ApplyFireDamage()
        {
            foreach (var damageable in _damageableTargets.ToList())
            {
                damageable.TakeDamage(_damagePerIteration);
            }
        }

        private IEnumerator WaitingDestroy()
        {
            yield return _waitForLifetime;
            
            ReturnToPool();
        }

        private void EndWaitingDestroy()
        {
            if (_waitingDestroy == null) 
                return;
            
            StopCoroutine(_waitingDestroy);
            _waitingDestroy = null;
        }
    }
}