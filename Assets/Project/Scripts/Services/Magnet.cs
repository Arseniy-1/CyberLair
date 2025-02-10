using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(PointEffector2D))]
public class Magnet : MonoBehaviour
{
    [SerializeField] private LayerMask _attractionLayer;

    private CircleCollider2D _collider;
    private PointEffector2D _effector;
    private Transform _player;

    public void Initialize(IMagnetStats magnetStats, Transform player)
    {
        _player = player;
        _collider = GetComponent<CircleCollider2D>();
        _effector = GetComponent<PointEffector2D>();

        _collider.isTrigger = true;
        // _collider.radius = magnetStats.MagnetRange;

        // _effector.forceMagnitude = -magnetStats.MagnetForce;
        _effector.forceVariation = 0f;
        _effector.distanceScale = 1f;
        _effector.drag = 0f;
        _effector.angularDrag = 0f;
        _effector.forceSource = EffectorSelection2D.Collider;
        _effector.forceTarget = EffectorSelection2D.Rigidbody;
        _effector.forceMode = EffectorForceMode2D.Constant;
    }

    private void FixedUpdate()
    {
        if (_player != null)
        {
            transform.position = _player.position;
        }
    }
}