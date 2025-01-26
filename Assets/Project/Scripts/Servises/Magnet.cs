using UnityEngine;
using UnityEngine.Serialization;

public class Magnet : MonoBehaviour
{
    [SerializeField] private float _attractionRadius = 5f;
    [SerializeField] private float _attractionForce = 10f;
    [SerializeField] private LayerMask _attractionLayer;

    private void FixedUpdate()
    {
        Collider2D[] attractables =
            Physics2D.OverlapCircleAll(transform.position, _attractionRadius, _attractionLayer);

        foreach (Collider2D attractable in attractables)
        {
            if (attractable.TryGetComponent(out IMoveable attractableComponent))
            {
                Vector2 direction = (transform.position - attractable.transform.position).normalized;

                attractableComponent.Rigidbody2D.AddForce(direction * _attractionForce * Time.fixedDeltaTime,
                    ForceMode2D.Force);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Рисуем радиус притяжения для наглядности
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _attractionRadius);
    }
}