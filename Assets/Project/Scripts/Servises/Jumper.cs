using Sirenix.OdinInspector;
using UnityEngine;
using System;

public class Jumper : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    
    private Vector3 _targetPosition;
    private bool _isMoving = false;
    private bool _isOnCooldown = false;
    private float _elapsedTime = 0f;
    private float _cooldownTimer = 0f;

    private IJumpStats _jumpStats;

    public event Action JumpPerformed;
    
    public bool IsOnCooldown => _isOnCooldown;
    public float CooldownTimer => _cooldownTimer;
    public IJumpStats JumpStats => _jumpStats;

    public bool CanJump => !_isMoving && !_isOnCooldown;

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            _elapsedTime += Time.fixedDeltaTime;

            float progress = _elapsedTime / _jumpStats.JumpTime;

            if (progress < 1f)
            {
                // var newPosition = Vector3.MoveTowards(transform.position, _targetPosition,
                //     _jumpStats.JumpDistance * Time.fixedDeltaTime / _jumpStats.JumpTime);
                //
                // _rigidbody.MovePosition(newPosition);
                
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition,
                    _jumpStats.JumpDistance * Time.fixedDeltaTime / _jumpStats.JumpTime);
            }
            else
            {
                transform.position = _targetPosition;
                _isMoving = false;
                JumpPerformed?.Invoke();
                StartCooldown();
            }
        }

        if (_isOnCooldown)
        {
            _cooldownTimer += Time.deltaTime;

            if (_cooldownTimer >= _jumpStats.JumpReloadTime)
            {
                _isOnCooldown = false;
                _cooldownTimer = 0f;
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
        if (CanJump)
        {
            if (direction == Vector3.zero)
                return;

            _targetPosition = transform.position + direction.normalized * _jumpStats.JumpDistance;
            _elapsedTime = 0f;
            _isMoving = true;
        }
    }

    private void StartCooldown()
    {
        _isOnCooldown = true;
        _cooldownTimer = 0f;
    }
}
