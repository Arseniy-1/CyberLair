using System;
using System.Collections;
using System.Collections.Generic;
using StateMashineSytem;
using StateMashineSytem.EnemyStates;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.EnemySystem
{
    public class Enemy : MonoBehaviour, ITarget, IDamagable
    {
        // [SerializeField] private CollisionHandler _collisionHandler;
        [SerializeField] protected EnemyMover _mover;
        [SerializeField] protected Rigidbody2D _enemyRigidbody;
        [SerializeField] private EnemyAttacker _attacker;
        // [SerializeField] private WeaponHolder _weaponHolder;
        [SerializeField] private Health _health;
        [SerializeField] private float _attackDistance;

        protected List<IState> States;
        private EntityStateMachine _stateMachine;
        private Player _player;
    
        public event Action<Enemy> OnDeath;
        
        public Vector2 Position => transform.position;
        public bool IsStunned { get; private set; }
        public bool HasPlayer => _player != null;
        public bool IsPlayerInRange => Vector2.Distance(Position, _player.Position) < _attackDistance;

        private void Update()
        {
            _stateMachine?.Update();
        }

        public void Initialize(Player player)
        {
            _player = player;
            
            States = new List<IState>
            {
                new EnemyIdleState(this, _enemyRigidbody),
                new EnemyMoveState(this, _mover),
                new EnemyAttackState(this, _mover, _attacker),
                new EnemyStunnedState(this, _mover)
            };
            
            _attacker.Initialize(_player);
            
            _stateMachine = new EntityStateMachine(States);

            foreach (IState state in States)
            {
                state.Initialize(_stateMachine);
            }
            
            _mover.Initialize(this, _player, _enemyRigidbody);
        }
        
        public void TakeDamage(int amount)
        {
            _health.TakeDamage(amount);
        }

        private void OnEnable()
        {
            _health.LostHealth += Die;
        }

        private void OnDisable()
        {
            _health.LostHealth += Die;
        }

        private void Die()
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
