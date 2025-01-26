using System.Collections.Generic;
using StateMashineSytem;
using StateMashineSytem.EnemyStates;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace Project.Scripts.EnemySystem
{
    public class Enemy : MonoBehaviour, ITarget, IDamageable, IDieable, IDestoyable<Enemy>
    {
        [SerializeField] private EnemyCollisionHandler _collisionHandler;
        [SerializeField] protected EnemyMover _mover;
        [SerializeField] protected Rigidbody2D _enemyRigidbody;
        [SerializeField] private EnemyAttacker _attacker;
        [SerializeField] private Destroyer _destroyer;
        [SerializeField] private EnemyTargetProvider _enemyTargetProvider;
        [SerializeField] private float _attackDistance;

        private EntityStateMachine _stateMachine;
        
        public event Action<Enemy> OnDestroyed;
        public static event Action<Enemy> OnDeath;

        [field: SerializeField] public Health Health {get; private set; }
        [field: SerializeField] public EnemyTypes EnemyType { get; private set; }
        [field: SerializeField] public EnemyStats EnemyStats { get; private set; }
        
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
            _attacker.Initialize(_enemyTargetProvider, EnemyStats);
            _destroyer.Initialize(Health, this);
            
            _stateMachine = new EntityStateMachine(states);

            foreach (IState state in states)
            {
                state.Initialize(_stateMachine);
            }
            
            _mover.Initialize(this, _enemyTargetProvider, _enemyRigidbody, EnemyStats);
        }
        
        public void TakeDamage(int amount)
        {
            Health.TakeDamage(amount);
        }

        public void ResetEnemy()
        {
            Health.ResetHealth();
            _stateMachine.SwitchState<EnemyIdleState>();
        }

        [Button]
        public void Die()
        {
            OnDestroyed?.Invoke(this);
            OnDeath?.Invoke(this);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackDistance);
        }
    }
}