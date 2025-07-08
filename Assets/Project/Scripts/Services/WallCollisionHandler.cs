using UnityEngine;

public class WallCollisionHandler : MonoBehaviour
{
    [SerializeField] private float _pushForce;
    [SerializeField] private float _stunTime;

    private void OnCollisionStay2D(Collision2D other)
    {
        HandleCollision(other.collider);
    }

    private void HandleCollision(Collider2D collider)
    {
        if (collider.TryGetComponent(out IStunable stunable) == false)
            return;

        stunable.TakeStun(_stunTime);
        Vector3 pushDirection = transform.up.normalized;

        stunable.Rigidbody2D.AddForce(pushDirection * _pushForce, ForceMode2D.Force);
    }
}