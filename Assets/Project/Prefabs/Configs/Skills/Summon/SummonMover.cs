using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SummonMover : MonoBehaviour
{
    private ISummonMoveStats _summonStats;
    
    private Rigidbody2D _rigidbody;
    private Transform _selfTransform;
    private Vector2 _targetMovePosition;
    private Transform _targetTransform;
    
    private Vector2 SelfPosition => transform.position;
    private Vector2 TargetPosition => _targetTransform.position;
    private Vector2 RandomPointAroundTarget => TargetPosition + Random.insideUnitCircle.normalized * _summonStats.MoveRadius;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Transform targetTransform, SummonStats summonStats)
    {
        _targetTransform = targetTransform;
        _summonStats = summonStats;
        
        StartCoroutine(ChangePosition());
    }
    
    public void MoveToNextPosition()
    {
        var newPosition = Vector2.MoveTowards(SelfPosition, _targetMovePosition,
            _summonStats.Speed.CurrentValue * Time.fixedDeltaTime);

        _rigidbody.MovePosition(newPosition);
    }
    
    private IEnumerator ChangePosition()
    {
        var wait = new WaitForSeconds(_summonStats.MoveDelay);
        
        while (isActiveAndEnabled)
        {
            _targetMovePosition = RandomPointAroundTarget;

            yield return wait;
        }
    }
}