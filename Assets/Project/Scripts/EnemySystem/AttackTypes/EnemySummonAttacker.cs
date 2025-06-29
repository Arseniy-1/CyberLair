using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Scripts.EnemySystem.AttackTypes
{
    public class EnemySummonAttacker : EnemyAttacker
    {
        [SerializeField] private Enemy _impPrefab;
        [SerializeField, MinMaxSlider(0.1f, 0.5f)] private Vector2 _spawnPeriod;
        [SerializeField] private int _startCount;
        
        private EnemySpawner _impSpawner;
        private readonly List<Enemy> _imps = new();

        private void OnDisable()
        {
            var temporaryImps = new Enemy[_imps.Count];
            _imps.CopyTo(temporaryImps);
            
            temporaryImps.ToList().ForEach(RemoveImp);
            temporaryImps.ToList().ForEach(imp => imp.Die());
        }

        public override void Initialize(EnemyTargetProvider enemyTargetProvider)
        { 
            _impSpawner = new EnemySpawner(_impPrefab, enemyTargetProvider.Player, _startCount);
            
            base.Initialize(enemyTargetProvider);
        }

        protected override IEnumerator Attack()
        {
            yield return new WaitForSeconds(Random.Range(_spawnPeriod.x, _spawnPeriod.y));
            
            Enemy imp = _impSpawner.Spawn();
            imp.OnDestroyed += RemoveImp;
            imp.transform.position = Position;
            
            _imps.Add(imp);
        }

        private void RemoveImp(Enemy imp)
        {
            imp.OnDestroyed -= RemoveImp;
            _imps.Remove(imp);
        }
    }
}