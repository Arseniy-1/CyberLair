using Sirenix.OdinInspector;
using UnityEngine;
using System;

public class Jumper : MonoBehaviour
{
    private Vector3 _targetPosition;
    private bool _isMoving = false;
    private float _elapsedTime = 0f;

    private IJumpStats _jumpStats;
    
    public event Action JumpPerformed;

    private void Update()
    {
        if (_isMoving)
        {
            _elapsedTime += Time.deltaTime;

            float progress = _elapsedTime / _jumpStats.JumpTime;
            
             if (progress < 1f)
             {
                 transform.position = Vector3.MoveTowards(transform.position, _targetPosition,
                     _jumpStats.JumpDistance * Time.deltaTime / _jumpStats.JumpTime);
             }
             else
             {
                 transform.position = _targetPosition;
                 _isMoving = false;
                 JumpPerformed?.Invoke();
             }
        }
    }

    public void Initialize(IJumpStats jumpStats)
     {
         _jumpStats = jumpStats;
     }

    [Button]
    public void Jump(Vector3 direction)
    {
        if (_isMoving == false)
        {
            if (direction == Vector3.zero)
                return;

            _targetPosition = transform.position + direction.normalized * _jumpStats.JumpDistance;
            _elapsedTime = 0f;
            _isMoving = true;
        }
    }
}