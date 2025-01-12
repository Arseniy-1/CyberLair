using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Weapon.ActiveSkills
{
    public class Thunder : MonoBehaviour
    {
        [SerializeField] private float _delay;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _actionRadius;
        [SerializeField] private int _damage;
        
        private int _strikesCount = 1;
        
        private Vector2 TargetPosition => transform.position;
        
        private void OnEnable()
        {
            StartCoroutine(StrikeIterating());
        }

        public void ApplyStats(float radiusMultiplier, float damageMultiplier, float countMultiplier)
        {
            _actionRadius *= radiusMultiplier;
            _damage *= (int)damageMultiplier;
            _strikesCount *= (int)countMultiplier;
        }

        private void Strike()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(TargetPosition, _actionRadius, _layerMask);
            Debug.Log(colliders.Length);
            
            if(colliders.Length == 0)
                return;

            for (int i = 0; i < _strikesCount; i++)
            {
                Collider2D strickenCollider = colliders[Random.Range(0, colliders.Length)];
                Debug.Log(strickenCollider.name);

                if (strickenCollider.TryGetComponent(out Health health))
                {
                    health.TakeDamage(_damage);
                }
            }
        }

        private IEnumerator StrikeIterating()
        {
            WaitForSeconds wait = new(_delay);
            
            while (isActiveAndEnabled)
            {
                yield return wait;
                
                Strike();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(TargetPosition, _actionRadius);
        }
    }
}