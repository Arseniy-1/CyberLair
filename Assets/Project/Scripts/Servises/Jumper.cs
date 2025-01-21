using Sirenix.OdinInspector;
using UnityEngine;
using System;

public class Jumper : MonoBehaviour
{
    private Vector3 _targetPosition;
    private bool _isMoving = false;
    private float _elapsedTime = 0f;

    public event Action JumpPerformed;

    private void Update()
    {
        if (_isMoving)
        {
            _elapsedTime += Time.deltaTime;

            // float progress = _elapsedTime / _jumpTime;
            //
            // if (progress < 1f)
            // {
            //     transform.position = Vector3.MoveTowards(transform.position, _targetPosition,
            //         _jumpDistance * Time.deltaTime / _jumpTime);
            // }
            // else
            // {
            //     transform.position = _targetPosition;
            //     _isMoving = false;
            //     JumpPerformed?.Invoke();
            // }
        }
    }

    public void Initialize(PlayerStats)
     {
    //     _jumpDistance = playerConfig.JumpDistance;
    //     _jumpTime = playerConfig.JumpTime;
     }

    [Button]
    public void Jump(Vector3 direction)
    {
        if (_isMoving == false)
        {
            if (direction == Vector3.zero)
                return;

            // _targetPosition = transform.position + direction.normalized * _jumpDistance;
            _elapsedTime = 0f;
            _isMoving = true;
        }
    }
}