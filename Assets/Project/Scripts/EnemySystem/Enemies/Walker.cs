using System.Collections.Generic;
using Project.Scripts.EnemySystem.AttackTypes;
using StateMashineSytem;
using StateMashineSytem.EnemyStates;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Enemies
{
    public class Walker : Enemy
    {
        [SerializeField] private EnemyJumpAttacker _attacker;
        
        public override void Initialize(Player player)
        {
            States = new List<IState>
            {
                new EnemyIdleState(this, EnemyRigidbody),
                new EnemyMoveState(this, Mover),
                new EnemyAttackState(this, Mover, _attacker),
                new EnemyStunnedState(this, Mover)
            };
            
            _attacker.Initialize(player);

            base.Initialize(player);
        }
    }
}