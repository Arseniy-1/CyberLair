using System.Collections.Generic;
using Project.Scripts.EnemySystem;
using UnityEngine;

namespace Project.Scripts.CompositionRoot
{
    public class EnemyFabric : MonoBehaviour
    {
        private Player _player;
        private List<Transform> _spawnPoints;
        
        public void Initialize(Player player, List<Transform> spawnPoints)
        {
            _player = player;
            _spawnPoints = spawnPoints;
        }
        
        public Enemy Create(Enemy enemy)
        {
            Enemy doneEnemy = Instantiate(enemy, _spawnPoints[Random.Range(0, _spawnPoints.Count)]);
            
            doneEnemy.Initialize(_player);

            return doneEnemy;
        }
    }
}