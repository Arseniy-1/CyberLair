using System.Collections.Generic;
using Project.Scripts.EnemySystem;
using UnityEngine;

namespace Project.Scripts.CompositionRoot
{
    public class EnemyFabric
    {
        private Player _player;

        public EnemyFabric(Player player)
        {
            _player = player;
        }
        
        // public void Initialize(Player player)
        // {
        //     _player = player;
        // }
        
        public Enemy Create(Enemy enemy)
        {
            Enemy doneEnemy = Object.Instantiate(enemy);
            
            doneEnemy.Initialize(_player);

            return doneEnemy;
        }
    }
}