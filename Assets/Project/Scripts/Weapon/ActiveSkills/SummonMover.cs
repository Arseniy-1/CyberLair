using System.Collections;
using UnityEngine;

public class SummonMover : MonoBehaviour
{
    private const float MoveOffset = 0.5f;
    
    [SerializeField] private float _nominalSpeed;
    [SerializeField] private float _moveRadius = 5f;
    [SerializeField] private float _moveDelay = 2f;
    
    private Rigidbody2D _rigidbody;
    private Transform _selfTransform;
    private Vector2 _currentMovePosition;
    private Transform _targetTransform;
    private float _currentSpeed;
    
    private Vector2 SelfPosition => transform.position;
    private Vector2 TargetPosition => _targetTransform.position;
    private Vector2 RandomPointAroundTarget => TargetPosition + Random.insideUnitCircle.normalized * _moveRadius;

    public void ApplyStats(float speedMultiplier)
    {
        _currentSpeed = _nominalSpeed * speedMultiplier;
    }
    
    public void Initialize(Transform targetTransform, Rigidbody2D rigidbody)
    {
        _targetTransform = targetTransform;
        _rigidbody = rigidbody;
        
        StartCoroutine(ChangePosition());
    }
    
    public void MoveToNextPosition()
    {
        if (Vector2.Distance(_currentMovePosition, SelfPosition) <= MoveOffset)
            return;
        
        Vector2 direction = (_currentMovePosition - SelfPosition).normalized;
        _rigidbody.MovePosition(_rigidbody.position + direction * (_currentSpeed * Time.fixedDeltaTime));
    }
    
    private IEnumerator ChangePosition()
    {
        var wait = new WaitForSeconds(_moveDelay);
        
        while (isActiveAndEnabled)
        {
            _currentMovePosition = RandomPointAroundTarget;

            yield return wait;
        }
    }
}