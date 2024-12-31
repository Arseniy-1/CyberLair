using Sirenix.OdinInspector;
using UnityEngine;
using System;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _distance = 1f;
    [SerializeField] private float _jumpTime = 1f;

    private Vector3 _targetPosition;
    private bool _isMoving = false;
    private float _elapsedTime = 0f;

    public event Action JumpPerformed;

    private void Update()
    {
        if (_isMoving)
        {
            _elapsedTime += Time.deltaTime;

            float progress = _elapsedTime / _jumpTime;

            if (progress < 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _distance * Time.deltaTime / _jumpTime);
            }
            else
            {
                transform.position = _targetPosition;
                _isMoving = false;
                JumpPerformed?.Invoke();
            }
        }
    }

    [Button]
    public void Jump(Vector3 direction)
    {
        Debug.Log("J");
        if (_isMoving == false)
        {
            if (direction == Vector3.zero)
                return;

            _targetPosition = transform.position + direction.normalized * _distance;
            _elapsedTime = 0f;
            _isMoving = true;
        }
    }
}
