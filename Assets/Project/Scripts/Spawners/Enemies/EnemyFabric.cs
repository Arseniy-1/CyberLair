using Project.Scripts.EnemySystem;
using Project.Scripts.PlayerSystem;
using UnityEngine;

namespace Project.Scripts.Spawners.Enemies
{
    public class EnemyFabric
    {
        private readonly Player _player;

        public EnemyFabric(Player player)
        {
            _player = player;
        }
        
        public Enemy Create(Enemy enemy)
        {
            Enemy doneEnemy = Object.Instantiate(enemy);
            
            doneEnemy.Initialize(_player);

            return doneEnemy;
        }
    }
}