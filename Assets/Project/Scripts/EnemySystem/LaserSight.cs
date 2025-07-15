using Project.Scripts.EnemySystem.AttackTypes;
using Project.Scripts.Weapon;
using UnityEngine;

namespace Project.Scripts.EnemySystem
{
    public class LaserSight : MonoBehaviour
    {
        [SerializeField] private Transform _originPosition;
        [SerializeField] private Weapon.Weapon _weapon;
        [SerializeField] private EnemyShootAttacker _attacker;
        [SerializeField] private EnemyTargetProvider _targetProvider;
        [SerializeField] private LineRenderer _lineRenderer;

        private void OnEnable()
        {
            _attacker.AttackStarted += OnAttackStarted;
            _weapon.Shot += OnShot;
        
            _lineRenderer.enabled = false;
        }

        private void FixedUpdate()
        {
            _lineRenderer.SetPosition(0, _originPosition.position);
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _targetProvider.Player.Position);
        }

        private void OnDisable()
        {
            _attacker.AttackStarted -= OnAttackStarted;
            _weapon.Shot -= OnShot;
        }

        private void OnAttackStarted()
        {
            _lineRenderer.enabled = true;
        }

        private void OnShot(Bullet bullet)
        {
            _lineRenderer.enabled = false;
        }
    }
}