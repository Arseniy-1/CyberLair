using System.Collections;
using UnityEngine;

namespace Project.Scripts.Weapon.ActiveSkills
{
    public class Thunder : MonoBehaviour
    {
        [SerializeField] private float _nominalDelay;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _nominalActionRadius;
        [SerializeField] private int _nominalDamage;
        
        private float _currentDelay;
        private int _strikesCount = 1;
        private float _currentActionRadius;
        private int _currentDamage;
        private WaitForSeconds _waitDelay;
        
        private Vector2 TargetPosition => transform.position;
        
        private void OnEnable()
        {
            StartCoroutine(StrikeIterating());
        }

        public void ApplyStats(float delayMultiplier, float radiusMultiplier, float damageMultiplier, float countMultiplier)
        {
            _currentDelay = _nominalDelay * delayMultiplier;
            _currentActionRadius = _nominalActionRadius * radiusMultiplier;
            _currentDamage = (int)(_nominalDamage * damageMultiplier);
            _strikesCount *= (int)countMultiplier;
            
            _waitDelay = new WaitForSeconds(_currentDelay);
        }

        private void Strike()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(TargetPosition, _currentActionRadius, _layerMask);
            
            if(colliders.Length == 0)
                return;

            for (int i = 0; i < _strikesCount; i++)
            {
                Collider2D strickenCollider = colliders[Random.Range(0, colliders.Length)];

                if (strickenCollider.TryGetComponent(out Health health))
                {
                    health.TakeDamage(_currentDamage);
                }
            }
        }

        private IEnumerator StrikeIterating()
        {
           while (isActiveAndEnabled)
           {
               yield return _waitDelay;
                
               Strike();
           }
        }
    }
}