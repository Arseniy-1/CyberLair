using UnityEngine;

public class WallCollisionHandler : CollisionHandler
{
    [SerializeField] private float _pushForce;
    protected override void HandleCollision(Collider2D collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            if (player.TryGetComponent(out Rigidbody2D rigidbody2D))
            {
                Vector3 pushDirection = (player.transform.position - transform.position).normalized;

                rigidbody2D.AddForce(pushDirection * _pushForce, ForceMode2D.Impulse);
                Debug.Log(pushDirection * _pushForce);
            }
        }
    }
}