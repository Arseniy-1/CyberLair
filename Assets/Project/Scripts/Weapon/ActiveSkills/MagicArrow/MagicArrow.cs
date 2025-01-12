using System;
using System.Collections;
using UnityEngine;

namespace Project.Scripts.Weapon.ActiveSkills.MagicArrow
{
    public class MagicArrow : MonoBehaviour, IDestoyable<MagicArrow>
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;
        [SerializeField] private float _timeToDespawn;
        
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
            Vector2 newPosition = _rigidbody.position + Forward * (_speed * Time.fixedDeltaTime);
            
            _rigidbody.MovePosition(newPosition);
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