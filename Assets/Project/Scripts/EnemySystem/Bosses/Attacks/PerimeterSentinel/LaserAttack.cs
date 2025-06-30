using System.Collections;
using Project.Scripts.Services.Enum;
using Project.Scripts.Services.Extensions;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.PerimeterSentinel
{
    public class LaserAttack : BossAttack
    {
        [SerializeField, Header("Laser Attack Settings")] private EnemyCollisionHandler _laser;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private ShakeID _shakeID = ShakeID.LongLight;
        [SerializeField] private EnemyTargetProvider _targetProvider;
        
        [SerializeField, Header("Spring Settings")] private float _stiffness = 2f;
        [SerializeField] private float _damping = 0.3f;

        [SerializeField, Header("Rotation Settings")] private float _maxRotationSpeed = 360f;
    
        private Transform _laserOrigin;
        private float _currentAngularVelocity;

        public override void Initialize()
        {
            BossAttackAnimationTrigger = Animator.StringToHash("LaserAttack");
            
            Disable();

            _laserOrigin = transform;
            _laser.Initialize(Damage);
        }

        protected override IEnumerator Attack()
        {   
            _collider.enabled = true;
            View.gameObject.SetActive(true);
            
            _shakeID.Shake();
            
            while (IsAttacking)
            {
                Vector2 direction = _targetProvider.Player.Position - (Vector2)_laserOrigin.position;
                float targetAngle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
                
                float currentAngle = _laserOrigin.eulerAngles.z;
                float angleDelta = Mathf.DeltaAngle(currentAngle, targetAngle);
                
                float springForce = angleDelta * _stiffness;
                float dampingForce = -_currentAngularVelocity * _damping;
                float torque = springForce + dampingForce;
                
                _currentAngularVelocity += torque * Time.deltaTime;
                _currentAngularVelocity = Mathf.Clamp(_currentAngularVelocity, -_maxRotationSpeed, _maxRotationSpeed);

                float newAngle = currentAngle + _currentAngularVelocity * Time.deltaTime;
                _laserOrigin.rotation = Quaternion.Euler(0f, 0f, newAngle);
                
                yield return null;
            }
            
            Disable();
        }

        public override void Disable()
        {
            _collider.enabled = false;
            View.gameObject.SetActive(false);
        }
    }
}