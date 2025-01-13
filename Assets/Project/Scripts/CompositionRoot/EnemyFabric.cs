using System.Collections.Generic;
using Project.Scripts.EnemySystem;
using UnityEngine;

namespace Project.Scripts.CompositionRoot
{
    public class EnemyFabric
    {
        private Player _player;
        
        public void Initialize(Player player)
        {
            _player = player;
        }
        
        public Enemy Create(Enemy enemy, Transform spawnPoint)
        {
            Enemy doneEnemy = Object.Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
            
            doneEnemy.Initialize(_player);

            return doneEnemy;
        }
    }
}