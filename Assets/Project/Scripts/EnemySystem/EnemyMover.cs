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
        
        private void FixedUpdate()
        {
            Move();
        }

        public void Initialize(Enemy enemy, Player player, Rigidbody2D rigidbody)
        {
            EnemyPrefab = enemy;
            PlayerPrefab = player;
            EnemyRigidbody = rigidbody;
        }

        protected abstract void Move();
    }
}