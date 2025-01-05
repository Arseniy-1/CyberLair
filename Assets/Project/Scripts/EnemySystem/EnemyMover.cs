using System;
using UnityEngine;

namespace Project.Scripts.EnemySystem
{
    public abstract class EnemyMover : MonoBehaviour
    {
        [SerializeField] protected float Speed;
        
        protected Enemy EnemyPrefab;
        protected EnemyTargetProvider EnemyTargetProvider;
        protected Rigidbody2D EnemyRigidbody;
        
        protected Vector2 Direction => (EnemyTargetProvider.Player.Position - EnemyPrefab.Position).normalized;
        
        private void FixedUpdate()
        {
            Move();
        }

        public void Initialize(Enemy enemy, EnemyTargetProvider enemyTargetProvider, Rigidbody2D enemyRigidbody)
        {
            EnemyPrefab = enemy;
            EnemyTargetProvider = enemyTargetProvider;
            EnemyRigidbody = enemyRigidbody;
        }

        protected abstract void Move();
    }
}