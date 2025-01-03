using System;
using System.Collections;
using System.Collections.Generic;
using StateMashineSytem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.EnemySystem
{
    public class Enemy : MonoBehaviour, ITarget, IDamagable
    {
        // [SerializeField] private CollisionHandler _collisionHandler;
        [SerializeField] protected EnemyMover Mover;
        [SerializeField] protected Rigidbody2D EnemyRigidbody;
        // [SerializeField] private WeaponHolder _weaponHolder;
        [SerializeField] private Health _health;
        [SerializeField] private float _attackDistance;

        protected List<IState> States;
        private EntityStateMachine _stateMachine;
        protected Player Player;
    
        public event Action<Enemy> OnDeath;
        
        public Vector2 Position => transform.position;
        public bool IsStunned { get; private set; }
        public bool HasPlayer => Player != null;
        public bool IsPlayerInRange
        {
            get
            {
                Debug.Log($"{Vector2.Distance(Position, Player.Position)} is in range");
                
                return Vector2.Distance(Position, Player.Position) < _attackDistance;
            }
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        public virtual void Initialize(Player player)
        {
            Player = player;
            
            _stateMachine = new EntityStateMachine(States);

            foreach (IState state in States)
            {
                state.Initialize(_stateMachine);
            }
            
            Mover.Initialize(this, Player, EnemyRigidbody);
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
