using Project.Scripts.EnemySystem;
using UnityEngine;

namespace StateMashineSytem.EnemyStates
{
    public class EnemyMoveState : IState
    {
        private IStateSwitcher _stateSwitcher;
        private readonly EnemyMover _mover;
        private readonly Enemy _enemy;
        private readonly EnemyTargetProvider _enemyTargetProvider;
        private readonly EnemyAttackCooldown _cooldown;
        
        private readonly Animator _animator;
        private readonly int _moveAnimation = Animator.StringToHash("IsMoving");

        public EnemyMoveState(Enemy enemy, EnemyMover mover,
            EnemyTargetProvider enemyTargetProvider, EnemyAttackCooldown cooldown, Animator animator)
        {
            _enemy = enemy;
            _mover = mover;
            _enemyTargetProvider = enemyTargetProvider;
            _cooldown = cooldown;
            _animator = animator;
        }
        
        public void Enter()
        {
            _mover.enabled = true;
            
            _animator.SetBool(_moveAnimation, _mover.enabled);
        }

        public void Update()
        {
            if(_enemyTargetProvider.HasPlayer == false)
                _stateSwitcher.SwitchState<EnemyIdleState>();
            
            if(_enemyTargetProvider.IsPlayerInRange && _cooldown.IsOnCooldown == false)
                _stateSwitcher.SwitchState<EnemyAttackState>();
        }

        public void Exit()
        {
            _mover.enabled = false;
        }

        public void Initialize(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }
    }
}