using Sirenix.OdinInspector;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private SoundPlayer _jumpSoundPlayer;
    
    private Rigidbody2D _rigidbody;
    
    private bool _isMoving = false;
    private bool _isOnCooldown = false;
    private float _elapsedTime = 0f;
    private float _cooldownTimer = 0f;

    private Vector3 _jumpDirection;

    private IJumpStats _jumpStats;

    public event Action JumpPerformed;

    public bool IsOnCooldown => _isOnCooldown;
    public float CooldownTimer => _cooldownTimer;
    public IJumpStats JumpStats => _jumpStats;

    public bool CanJump => !_isMoving && !_isOnCooldown;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            _elapsedTime += Time.fixedDeltaTime;

            if (_elapsedTime < _jumpStats.JumpTime.CurrentValue)
            {
                Vector3 movement = _jumpDirection * _jumpStats.JumpSpeed.CurrentValue * Time.fixedDeltaTime;
                _rigidbody.MovePosition(_rigidbody.position + (Vector2)movement);
            }
            else
            {
                _isMoving = false;
                JumpPerformed?.Invoke();
                StartCooldown();
            }
        }

        if (_isOnCooldown)
        {
            _cooldownTimer += Time.deltaTime;

            if (_cooldownTimer >= _jumpStats.JumpReloadTime.CurrentValue)
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

            _jumpSoundPlayer.Play();
            _jumpDirection = direction.normalized;
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
