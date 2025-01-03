using System;
using UnityEngine;

namespace Project.Scripts.EnemySystem
{
    public abstract class EnemyAttacker : MonoBehaviour
    {
        private Player Player;
        private Transform EnemyTransform;
        
        public event Action AttackPerformed;
        
        protected Vector2 Direction => (Player.Position - Position).normalized;
        private Vector2 Position => EnemyTransform.position;
        
        public abstract void Attack();
        
        public void Initialize(Player player)
        {
            Player = player;
            EnemyTransform = transform;
        }

        protected virtual void EndAttack()
        {
            AttackPerformed?.Invoke();
        }
    }
}