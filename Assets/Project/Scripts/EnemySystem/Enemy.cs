using System.Collections.Generic;
using StateMashineSytem;
using StateMashineSytem.EnemyStates;
using UnityEngine;
using System;

namespace Project.Scripts.EnemySystem
{
    public class Enemy : MonoBehaviour, ITarget, IDamagable, IDieable, IDestoyable<Enemy>
    {
        [SerializeField] private EnemyCollisionHandler _collisionHandler;
        [SerializeField] protected EnemyMover _mover;
        [SerializeField] protected Rigidbody2D _enemyRigidbody;
        [SerializeField] private EnemyAttacker _attacker;
        [SerializeField] private Destroyer _destroyer;
        [SerializeField] private EnemyTargetProvider _enemyTargetProvider;
        
        [SerializeField] private Health _health;
        [SerializeField] private float _attackDistance;

        private EntityStateMachine _stateMachine;
        
        public event Action<Enemy> OnDestroyed;
        public event Action<Enemy> OnDeath;

        [field: SerializeField] public EnemyTypes EnemyType { get; private set; }
        
        public Vector2 Position => transform.position;
        public bool IsStunned { get; private set; }

        private void Update()
        {
            _stateMachine?.Update();
        }

        public void Initialize(Player player)
        {
            var states = new List<IState>
            {
                new EnemyIdleState(this, _enemyRigidbody, _enemyTargetProvider),
                new EnemyMoveState(this, _mover, _enemyTargetProvider),
                new EnemyAttackState(this, _mover, _attacker),
                new EnemyStunnedState(this, _mover)
            };
            
            _enemyTargetProvider.Initialize(player, _attackDistance);
            _attacker.Initialize(_enemyTargetProvider);
            _destroyer.Initialize(_health, this);
            
            _stateMachine = new EntityStateMachine(states);

            foreach (IState state in states)
            {
                state.Initialize(_stateMachine);
            }
            
            _mover.Initialize(this, _enemyTargetProvider, _enemyRigidbody);
        }
        
        public void TakeDamage(int amount)
        {
            _health.TakeDamage(amount);
        }

        public void Die()
        {
            OnDeath?.Invoke(this);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackDistance);
        }
    }
}