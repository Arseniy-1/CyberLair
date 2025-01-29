using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private LayerMask _attractionLayer;

    private IMagnetStats _magnetStats;
    
    private void FixedUpdate()
    {
        Collider2D[] attractables =
            Physics2D.OverlapCircleAll(transform.position, _magnetStats.MagnetRange, _attractionLayer);

        foreach (Collider2D attractable in attractables)
        {
            if (attractable.TryGetComponent(out IMoveable attractableComponent))
            {
                Vector2 direction = (transform.position - attractable.transform.position).normalized;

                attractableComponent.Rigidbody2D.AddForce(direction * _magnetStats.MagnetRange * Time.fixedDeltaTime,
                    ForceMode2D.Force);
            }
        }
    }

    public void Initialize(IMagnetStats magnetStats)
    {
        _magnetStats = magnetStats;
    }
}