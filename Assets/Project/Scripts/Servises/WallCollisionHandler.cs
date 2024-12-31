using UnityEngine;

public class WallCollisionHandler : CollisionHandler
{
    [SerializeField] private float _pushForce;
    [SerializeField] private float _stunTime;

    protected override void HandleCollision(Collider2D collider)
    {
        if (collider.TryGetComponent(out Rigidbody2D rigidbody2D))
        {
            if (rigidbody2D.TryGetComponent(out IStunable stunable))
            {
                stunable.TakeStun(_stunTime);
                Vector3 pushDirection = transform.up;

                rigidbody2D.AddForce(pushDirection * _pushForce, ForceMode2D.Force);
            }
        }
    }
}