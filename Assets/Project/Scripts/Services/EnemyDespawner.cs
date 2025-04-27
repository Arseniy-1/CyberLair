using System;
using Project.Scripts.EnemySystem;
using UnityEngine;

namespace Project.Scripts.Services
{
    public class EnemyDespawner : CollisionHandler
    {
        public event Action<Enemy> EnemyDespawn;
        
        protected override void HandleCollision(Collider2D collider)
        {
            if (collider.TryGetComponent(out Enemy enemy))
            {
                EnemyDespawn?.Invoke(enemy);
            }
        }
    }
}