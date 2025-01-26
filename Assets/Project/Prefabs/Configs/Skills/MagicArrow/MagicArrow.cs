using System;
using System.Collections;
using Project.Scripts.Servises;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Weapon.ActiveSkills.MagicArrow
{
    public class MagicArrow : MonoBehaviour, IDestoyable<MagicArrow>
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _nominalSpeed;
        [SerializeField] private int _nominalDamage;
        [SerializeField] private float _timeToDespawn;
        [SerializeField] private SkillCollisionHandler _collisionHandler;
        
        private float _currentSpeed;
        private int _currentDamage;
        
        private Transform _transform;
        
        public event Action<MagicArrow> OnDestroyed;
        
        private Vector2 Forward => _transform.right;

        private void OnEnable()
        {
            _collisionHandler.ContactLimitExpired += Return;
            
            _transform = transform;
            
            StartCoroutine(Lifetime());
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + Forward * (_currentSpeed * Time.fixedDeltaTime));
        }

        private void OnDisable()
        {
            _collisionHandler.ContactLimitExpired -= Return;
        }

        public void ApplyStats(float speedMultiplier, int damageMultiplier)
        {
            _currentSpeed = _nominalSpeed * speedMultiplier;
            _currentDamage = _nominalDamage * damageMultiplier;
            
            _collisionHandler.ApplyStats(_currentDamage);
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
    }
}