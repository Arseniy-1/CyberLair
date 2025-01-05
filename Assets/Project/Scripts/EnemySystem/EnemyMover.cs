using System;
using UnityEngine;

namespace Project.Scripts.EnemySystem
{
    public abstract class EnemyMover : MonoBehaviour
    {
        [SerializeField] protected float Speed;
        
        protected Enemy EnemyPrefab;
        protected Player PlayerPrefab;
        protected Rigidbody2D EnemyRigidbody;
        
        protected Vector2 Direction => (PlayerPrefab.Position - EnemyPrefab.Position).normalized;
        
        private void FixedUpdate()
        {
            Move();
        }

        public void Initialize(Enemy enemy, Player player, Rigidbody2D enemyRigidbody)
        {
            EnemyPrefab = enemy;
            PlayerPrefab = player;
            EnemyRigidbody = enemyRigidbody;
        }

        protected abstract void Move();
    }
}