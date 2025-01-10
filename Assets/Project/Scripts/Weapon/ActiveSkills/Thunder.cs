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
            StartCoroutine(ThunderTimer());
        }

        [Button]
        public void IncreaseStrikesCount(int count)
        {
            _strikesCount += count;
        }

        private void Strike()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(TargetPosition, ActionRadius, _layerMask);
            
            Debug.Log(colliders.Length);

            for (int i = 0; i < _strikesCount; i++)
            {
                Debug.Log(Random.Range(0, colliders.Length));
                
                Collider2D strickenCollider = colliders[Random.Range(0, colliders.Length)];

                if (strickenCollider.TryGetComponent(out Health health))
                {
                    health.TakeDamage(Damage);
                }
            }
        }

        private IEnumerator ThunderTimer()
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