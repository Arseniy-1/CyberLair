using Project.Scripts.EnemySystem;
using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.StateMashine.EnemyStates
{
    public class EnemyAttackState : IState
    {
        private readonly EnemyMover _mover;
        private readonly EnemyAttacker _attacker;
        private readonly EnemyAttackCooldown _cooldown;

        private readonly int _attackAnimation = Animator.StringToHash("IsAttacking");
        private readonly int _moveAnimation = Animator.StringToHash("IsMoving");
        private readonly int _attackTrigger = Animator.StringToHash("StartAttack");
        
        private IStateSwitcher _stateSwitcher;
        private Animator _animator;
        
        public EnemyAttackState(EnemyMover mover, EnemyAttacker attacker, EnemyAttackCooldown cooldown)
        {
            _mover = mover;
            _attacker = attacker;
            _cooldown = cooldown;
        }

        public void Enter()
        {
            _mover.enabled = false;
            
            _attacker.AttackStarted += OnAttackStarted;
            _attacker.AttackPerforming += OnAttackPerforming;
            _attacker.AttackPerformed += OnAttackPerformed;
            
            _animator.SetBool(_moveAnimation, _mover.enabled);
            
            _attacker.PerformAttack();
        }

        public void Update() { }

        public void Exit()
        {
            _attacker.AttackStarted -= OnAttackStarted;
            _attacker.AttackPerforming -= OnAttackPerforming;
            _attacker.AttackPerformed -= OnAttackPerformed;
            
            _animator.ResetTrigger(_attackTrigger);
            
            _mover.enabled = true;
        }

        public void Initialize(IStateSwitcher stateSwitcher, Animator animator)
        {
            _stateSwitcher = stateSwitcher;
            _animator = animator;
        }

        private void OnAttackStarted()
        {
            _animator.SetTrigger(_attackTrigger);
        }

        private void OnAttackPerforming(bool isPerforming)
        {
            _animator.SetBool(_attackAnimation, isPerforming);
        }

        private void OnAttackPerformed()
        {
            _cooldown.StartCooldown(_attacker.Stats.Cooldown);
            
            _stateSwitcher.SwitchState<EnemyIdleState>();
        }
    }
}