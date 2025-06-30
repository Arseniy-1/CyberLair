using UnityEngine;

public class PlayerSight : MonoBehaviour
{
    [SerializeField] private TargetScanner _targetScanner;
    [SerializeField] private SpriteRenderer _view;

    private Transform _transform;
    
    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        if (_targetScanner.HasTarget)
        {
            _transform.position = _targetScanner.ClosestTarget.Position;
            _view.enabled = true;
        }
        else
        {
            _view.enabled = false;
        }
    }
}