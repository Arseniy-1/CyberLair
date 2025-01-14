using System;
using System.Collections;
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
        
        private float _currentSpeed;
        private int _currentDamage;
        
        private Transform _transform;
        
        public event Action<MagicArrow> OnDestroyed;
        
        private Vector2 Forward => _transform.right;

        private void OnEnable()
        {
            _transform = transform;
            
            StartCoroutine(Lifetime());
        }

        private void FixedUpdate()
        {
            Vector2 newPosition = _rigidbody.position + Forward * (_nominalSpeed * Time.fixedDeltaTime);
            
            _rigidbody.MovePosition(newPosition);
        }

        public void ApplyStats(float speedMultiplier, int damageMultiplier)
        {
            _currentSpeed = _nominalSpeed * speedMultiplier;
            _currentDamage = _nominalDamage * damageMultiplier;
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