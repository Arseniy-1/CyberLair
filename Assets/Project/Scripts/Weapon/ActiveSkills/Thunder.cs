using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Scripts.Weapon.ActiveSkills
{
    public class Thunder : ActiveWeapon
    {
        [SerializeField] private float _delay;
        [SerializeField] private LayerMask _layerMask;
        
        private int _strikesCount = 1;
        
        private Vector2 TargetPosition => TargetTransform.position;
        
        private void OnEnable()
        {
            StartCoroutine(StrikeIterating());
        }

        public void ApplyStats(float radius, float damage, float count)
        {
            ActionRadius = radius;
            Damage = (int)damage;
            _strikesCount = (int)count;
        }

        private void Strike()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(TargetPosition, ActionRadius, _layerMask);

            for (int i = 0; i < _strikesCount; i++)
            {
                Collider2D strickenCollider = colliders[Random.Range(0, colliders.Length)];

                if (strickenCollider.TryGetComponent(out Health health))
                {
                    health.TakeDamage(Damage);
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
            Gizmos.DrawWireSphere(TargetTransform.position, ActionRadius);
        }
    }
}