using System;
using System.Collections;
using Project.Scripts.Interfaces;
using Project.Scripts.Services;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.MagicArrow
{
    public class MagicArrow : MonoBehaviour, IDestoyable<MagicArrow>
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private float _timeToDespawn;
        [SerializeField] private SkillCollisionHandler _collisionHandler;
        
        private Transform _transform;
        private Coroutine _lifetimeCoroutine;
        
        public event Action<MagicArrow> OnDestroyed;
        
        private Vector2 Forward => _transform.right;

        private void OnEnable()
        {
            _collisionHandler.ContactLimitExpired += Return;
            
            _transform = transform;
            
            EndLifetime();
            
            _lifetimeCoroutine = StartCoroutine(Lifetime());
        }
        
        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + Forward * (_speed * Time.fixedDeltaTime));
        }

        private void OnDisable()
        {
            _collisionHandler.ContactLimitExpired -= Return;
            
            EndLifetime();
        }

        private IEnumerator Lifetime()
        {
            var wait = new WaitForSeconds(_timeToDespawn);
            
            yield return wait;

            Return();
        }
        
        private void Return()
        {
            OnDestroyed?.Invoke(this);
        }

        private void EndLifetime()
        {
            if(_lifetimeCoroutine != null)
                StopCoroutine(_lifetimeCoroutine);
        }
    }
}