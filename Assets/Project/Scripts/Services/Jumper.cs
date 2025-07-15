using System;
using Project.Scripts.Interfaces;
using Project.Scripts.Services.Enum;
using Project.Scripts.Services.Extensions;
using UnityEngine;

namespace Project.Scripts.Services
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Jumper : MonoBehaviour
    {
        [SerializeField] private AudioID _jumpSound = AudioID.PlayerJump;
        [SerializeField] private ParticleSystem _jumpEffector;
    
        private Rigidbody2D _rigidbody;
    
        private bool _isMoving;
        private float _elapsedTime;

        private Vector3 _jumpDirection;

        public event Action JumpPerformed;

        public bool IsOnCooldown { get; private set; }
        public float CooldownTimer { get; private set; }
        public IJumpStats JumpStats { get; private set; }
        public bool CanJump => !_isMoving && !IsOnCooldown;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (_isMoving)
            {
                _elapsedTime += Time.fixedDeltaTime;

                if (_elapsedTime < JumpStats.JumpTime.CurrentValue)
                {
                    Vector3 movement = _jumpDirection * JumpStats.JumpSpeed.CurrentValue;
                    _rigidbody.velocity = movement;
                }
                else
                {
                    _isMoving = false;
                    JumpPerformed?.Invoke();
                    StartCooldown();
                }
            }

            if (IsOnCooldown == false) 
                return;
        
            CooldownTimer += Time.deltaTime;

            if (CooldownTimer < JumpStats.JumpReloadTime.CurrentValue) 
                return;
            
            IsOnCooldown = false;
            CooldownTimer = 0f;
        }

        public void Initialize(IJumpStats jumpStats)
        {
            JumpStats = jumpStats;
        }

        public void Jump(Vector3 direction)
        {
            if (CanJump == false)
                return;
        
            if (direction == Vector3.zero)
                return;

            _jumpSound.Play();
            _jumpEffector.Play();
            _jumpDirection = direction.normalized;
            _elapsedTime = 0f;
            _isMoving = true;
        }

        private void StartCooldown()
        {
            IsOnCooldown = true;
            CooldownTimer = 0f;
        }
    }
}
