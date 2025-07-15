using Project.Scripts.PlayerSystem;
using Project.Scripts.Weapon;
using UnityEngine;

namespace Project.Scripts.Services
{
    public class InvincibilityCollisionHandler : CollisionHandler
    {
        [SerializeField] private LayerMask _bulletLayer;

        protected override void HandleCollision(Collider2D collider)
        {
            if (collider.GetComponent<Player>())
                return;

            if (collider.TryGetComponent(out Bullet bullet) == false)
                return;
        
            if (bullet.gameObject.layer != _bulletLayer)
                return;
                
            Vector2 currentVelocity = bullet.Rigidbody2D.velocity;

            float currentAngle = Mathf.Atan2(currentVelocity.y, currentVelocity.x) * Mathf.Rad2Deg;

            float reflectedAngle = currentAngle + 180f;

            float randomAngle = Random.Range(-30f, 30f);
            reflectedAngle += randomAngle;

            float angleInRadians = reflectedAngle * Mathf.Deg2Rad;

            Vector2 newVelocity = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)) *
                                  currentVelocity.magnitude;

            bullet.Rigidbody2D.velocity = newVelocity;

            if (newVelocity.sqrMagnitude <= 0)
                return;
        
            float angle = Mathf.Atan2(newVelocity.y, newVelocity.x) * Mathf.Rad2Deg;
            bullet.Rigidbody2D.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}