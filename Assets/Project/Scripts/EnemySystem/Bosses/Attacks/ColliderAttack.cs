using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.MessageBroker.CameraMessageBrokers;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses
{
    public abstract class ColliderAttack : BossAttack
    {
        [SerializeField] private Vector2 _offset;
        [SerializeField] private Vector2 _size;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private CameraShakeSettings _cameraShakeSettings;
        
        [SerializeField] private Transform _bossViewScale;

        public override void Disable()
        {
            View.gameObject.SetActive(false);
        }

        protected override IEnumerator Attack()
        {
            List<Collider2D> affectedColliders = Physics2D
                .OverlapBoxAll((Vector2)transform.position + _offset * _bossViewScale.localScale.x, _size, _layerMask).ToList();
            
            MessageBrokerHolder.Camera.Publish(new M_CameraShake(_cameraShakeSettings));

            foreach (Collider2D collider in affectedColliders)
            {
                if (collider.TryGetComponent(out IDamageable damageable))
                    damageable.TakeDamage(Damage);
            }
            
            Disable();
            
            yield return null;
        }

        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            
            Gizmos.DrawWireCube((Vector2)transform.position + _offset * _bossViewScale.localScale.x, _size);
        }
    }
}