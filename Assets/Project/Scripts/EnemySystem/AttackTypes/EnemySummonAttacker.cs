using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Spawners.Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Scripts.EnemySystem.AttackTypes
{
    public class EnemySummonAttacker : EnemyAttacker
    {
        private readonly List<Enemy> _imps = new();
        
        [SerializeField] private Enemy _impPrefab;
        [SerializeField] [MinMaxSlider(0.1f, 0.5f)] private Vector2 _spawnPeriod;
        [SerializeField] private int _startCount;
        
        private EnemySpawner _impSpawner;

        private void OnDisable()
        {
            var temporaryImps = new Enemy[_imps.Count];
            
            _imps.CopyTo(temporaryImps);
            
            temporaryImps.ToList().ForEach(RemoveImp);
        }

        public override void Initialize(EnemyTargetProvider enemyTargetProvider)
        { 
            _impSpawner = new EnemySpawner(_impPrefab, enemyTargetProvider.Player, _startCount);
            
            base.Initialize(enemyTargetProvider);
        }

        protected override IEnumerator Attack()
        {
            var waitForDelay = new WaitForSeconds(Random.Range(_spawnPeriod.x, _spawnPeriod.y));
            
            yield return waitForDelay;
            
            Enemy imp = _impSpawner.Spawn();
            
            imp.OnDestroyed += RemoveImp;
            imp.transform.position = Position;
            imp.ResetState();
            
            _imps.Add(imp);
        }

        private void RemoveImp(Enemy imp)
        {
            imp.OnDestroyed -= RemoveImp;
            _imps.Remove(imp);
        }
    }
}