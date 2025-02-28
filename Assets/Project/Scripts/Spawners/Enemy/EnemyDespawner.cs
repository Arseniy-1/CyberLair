using System;
using Project.Scripts.EnemySystem;
using UnityEngine;

namespace Project.Scripts.CompositionRoot
{
    public class EnemyDespawner : MonoBehaviour
    {
        public event Action<Enemy> EnemyDespawnNeeded;

        private void OnTriggerExit2D(Collider2D other)
        {
            HandleCollision(other);
        }
        
        private void HandleCollision(Collider2D collider)
        { 
            if (collider.TryGetComponent(out Enemy enemy))
            {
                EnemyDespawnNeeded?.Invoke(enemy);
            }
        }
    }
}