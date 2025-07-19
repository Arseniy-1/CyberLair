using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.EnemySystem
{
    public abstract class EnemyMover : MonoBehaviour
    {
        protected IMoverStats MoverStats;

        protected EnemyTargetProvider EnemyTargetProvider;
        protected Rigidbody2D EnemyRigidbody;
        private Enemy _enemy;
        
        protected Vector2 Direction => (EnemyTargetProvider.Player.Position - _enemy.Position).normalized;
        
        private void FixedUpdate()
        {
            Move();
        }

        private void OnDisable()
        {
            if (EnemyRigidbody)
                EnemyRigidbody.velocity = Vector2.zero;
        }

        public void Initialize(Enemy enemy, EnemyTargetProvider enemyTargetProvider, Rigidbody2D enemyRigidbody, IMoverStats moverStats)
        {
            _enemy = enemy;
            EnemyTargetProvider = enemyTargetProvider;
            EnemyRigidbody = enemyRigidbody;
            MoverStats = moverStats;
        }

        protected abstract void Move();
    }
}