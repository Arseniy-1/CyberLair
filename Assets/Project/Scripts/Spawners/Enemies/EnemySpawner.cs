using System.Collections.Generic;
using Project.Scripts.EnemySystem;
using Project.Scripts.PlayerSystem;
using Project.Scripts.Services.Enum;
using Project.Scripts.Stats;
using UnityEngine;

namespace Project.Scripts.Spawners.Enemies
{
    public class EnemySpawner : Spawner<Enemy>
    {
        private List<Transform> _spawnPoints;
        
        public EnemySpawner(Enemy enemyPrefab, Player player, int startCount)
        {
            StartAmount = startCount;
            var fabric = new EnemyFabric(player);

            Prefab = enemyPrefab;

            Pool = new EnemyPool(Prefab, fabric, StartAmount);
        }
        
        public EnemyTypes EnemyType => Prefab.EnemyType;

        public void ApplyModifier(StatModifier statModifier)
        {
            (Pool as EnemyPool)?.AddModifier(statModifier);
        }
    }
}