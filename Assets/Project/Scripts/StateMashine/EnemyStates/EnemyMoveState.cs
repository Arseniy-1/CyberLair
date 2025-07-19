using Project.Scripts.EnemySystem;
using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.StateMashine.EnemyStates
{
    public class EnemyMoveState : IState
    {
        private readonly EnemyMover _mover;
        private readonly EnemyTargetProvider _enemyTargetProvider;
        private readonly EnemyAttackCooldown _cooldown;
        
        private readonly int _moveAnimation = Animator.StringToHash("IsMoving");
        
        private IStateSwitcher _stateSwitcher;
        private  Animator _animator;

        public EnemyMoveState(EnemyMover mover, EnemyTargetProvider enemyTargetProvider, EnemyAttackCooldown cooldown)
        {
            _mover = mover;
            _enemyTargetProvider = enemyTargetProvider;
            _cooldown = cooldown;
        }
        
        public void Enter()
        {
            _mover.enabled = true;
            
            _animator.SetBool(_moveAnimation, _mover.enabled);
        }

        public void Update()
        {
            if (_enemyTargetProvider.HasPlayer == false)
                _stateSwitcher.SwitchState<EnemyIdleState>();
            
            if (_enemyTargetProvider.IsPlayerInRange && _cooldown.IsOnCooldown == false)
                _stateSwitcher.SwitchState<EnemyAttackState>();
        }

        public void Exit()
        {
            _mover.enabled = false;
        }

        public void Initialize(IStateSwitcher stateSwitcher, Animator animator)
        {
            _stateSwitcher = stateSwitcher;
            _animator = animator;
        }
    }
}