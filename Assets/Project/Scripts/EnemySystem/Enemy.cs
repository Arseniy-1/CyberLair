using System.Collections.Generic;
using StateMashineSytem;
using StateMashineSytem.EnemyStates;
using UnityEngine;
using System;
using System.Collections;
using Sirenix.OdinInspector;

namespace Project.Scripts.EnemySystem
{
    public class Enemy : MonoBehaviour, ITarget, IDamageable, IDieable, IDestoyable<Enemy>, IStunable
    {
        [SerializeField] private EnemyCollisionHandler _collisionHandler;
        [SerializeField] protected EnemyMover _mover;
        [SerializeField] protected Rigidbody2D _rigidbody;
        [SerializeField] private EnemyAttacker _attacker;
        [SerializeField] private Destroyer _destroyer;
        [SerializeField] private EnemyTargetProvider _enemyTargetProvider;
        [SerializeField] private float _attackDistance;
        [SerializeField] private EnemyView _view;
        [SerializeField] private AudioID _damageSound = AudioID.EnemyTakeDamage;
        
        private EntityStateMachine _stateMachine;
        private EnemyAttackCooldown _cooldown;

        public event Action OnDeath;
        public event Action<Enemy> OnDestroyed;

        [field: SerializeField] public EnemyTypes EnemyType { get; private set; }
        [field: SerializeField] public EnemyStats EnemyStats { get; private set; }

        public Rigidbody2D Rigidbody2D => _rigidbody;

        public Vector2 Position => transform.position;
        
        private void Update()
        {
            _stateMachine?.Update();
            EnemyStats.Update();
        }

        private void OnDisable()
        {
            _cooldown?.EndCooldown();
            _view?.EndBlink();
        }

        public void Initialize(Player player)
        {
            _cooldown = new EnemyAttackCooldown();
            
            _view.Initialize();
            
            var states = new List<IState>
            {
                new EnemyIdleState(this, _mover, _enemyTargetProvider),
                new EnemyMoveState(this, _mover, _enemyTargetProvider, _cooldown),
                new EnemyAttackState(_mover, _attacker, _cooldown),
                new EnemyStunnedState(this, _mover)
            };
            
            _stateMachine = new EntityStateMachine(states);
            
            foreach (IState state in states)
            {
                state.Initialize(_stateMachine, _view.Animator);
            }
            
            _stateMachine.Initialize();
            EnemyStats.Initialize();
            _enemyTargetProvider.Initialize(player, _attackDistance);
            _attacker.Initialize(_enemyTargetProvider);
            _destroyer.Initialize(EnemyStats.Health, this);
            
            _mover.Initialize(this, _enemyTargetProvider, _rigidbody, EnemyStats);
            _collisionHandler.Initialize(EnemyStats.CollisionDamage);
        }
        
        public void TakeDamage(float amount)
        {
            _damageSound.Play();
            _view.StartBlink();
            EnemyStats.Health.TakeDamage(amount);
        }
        
        public void TakeStun(float time)
        {
            if(isActiveAndEnabled)
                StartCoroutine(TakingStun(time));
        }

        public void ResetState()
        {
            _stateMachine.SwitchState<EnemyIdleState>();
        }

        [Button]
        public void Die()
        {
            MessageBrokerHolder.Enemy.Publish(new M_EnemyDeath(transform.position));
            OnDestroyed?.Invoke(this);
        }
        
        private IEnumerator TakingStun(float time)
        {
            var waitForStunTime = new WaitForSeconds(time);
            
            _stateMachine.SwitchState<EnemyStunnedState>();
        
            yield return waitForStunTime;
        
            _stateMachine.SwitchState<EnemyIdleState>();
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackDistance);
        }
    }
    
}