using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.FireZone
{
    public class FireZone : MonoBehaviour, IDestoyable<FireZone>
    {
        [SerializeField] private int _damagePerIteration = 2;
        [SerializeField] private float _burnInterval = 1f;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _lifeTime = 10f;

        private List<IDamageable> _damageableTargets = new();
        private float _currentTime = 0f;

        private Coroutine _waitingDestroy;

        public event Action<FireZone> OnDestroyed;

        private void OnEnable()
        {
            _waitingDestroy = StartCoroutine(WaitingDestroy());
        }
    
        private void FixedUpdate()
        {
            if (!(Time.time >= _currentTime)) return;
        
            ApplyFireDamage();
            _currentTime = Time.time + _burnInterval;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamageable damagable) & (_targetLayer << collision.gameObject.layer) != 0)
            {
                _damageableTargets.Add(damagable);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamageable damagable) & (_targetLayer << collision.gameObject.layer) != 0)
            {
                _damageableTargets.Remove(damagable);
            }
        }

        private void OnDisable()
        {
            StopCoroutine(_waitingDestroy);
        }

        private void ApplyFireDamage()
        {
            foreach (var damageable in _damageableTargets)
            {
                damageable.TakeDamage(_damagePerIteration);
            }
        }

        private IEnumerator WaitingDestroy()
        {
            yield return new WaitForSeconds(_lifeTime);
            OnDestroyed?.Invoke(this);
        }
    }
}