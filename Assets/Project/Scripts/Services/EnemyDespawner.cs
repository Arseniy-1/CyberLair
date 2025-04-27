using System;
using Project.Scripts.EnemySystem;
using UnityEngine;

namespace Project.Scripts.Services
{
    public class EnemyDespawner : MonoBehaviour
    {
        public event Action<Enemy> EnemyDespawn;

        private void OnTriggerExit2D(Collider2D other)
        {
            HandleCollision(other);
        }
        
        private void HandleCollision(Collider2D collider)
        {
            Debug.Log(collider.gameObject.name);
            
            if (collider.TryGetComponent(out Enemy enemy))
            {
                EnemyDespawn?.Invoke(enemy);
            }
        }
    }
}