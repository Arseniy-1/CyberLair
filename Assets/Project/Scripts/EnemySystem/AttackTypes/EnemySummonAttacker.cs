using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.EnemySystem.AttackTypes
{
    public class EnemySummonAttacker : EnemyAttacker
    {
        [SerializeField] private Enemy _impPrefab;
        [SerializeField] private int _startCount;
        
        private EnemySpawner _impSpawner;
        private List<Enemy> _imps = new();

        private void OnDisable()
        {
            var temporaryImps = new Enemy[_imps.Count];
            _imps.CopyTo(temporaryImps);
            
            temporaryImps.ToList().ForEach(RemoveImp);
            temporaryImps.ToList().ForEach(imp => Destroy(imp.gameObject));
        }

        public override void Initialize(EnemyTargetProvider enemyTargetProvider)
        { 
            _impSpawner = new EnemySpawner(_impPrefab, enemyTargetProvider.Player, _startCount);
            
            base.Initialize(enemyTargetProvider);
        }

        protected override IEnumerator Attack()
        {
            Enemy imp = _impSpawner.Spawn();
            imp.OnDestroyed += RemoveImp;
            imp.transform.position = Position;
            
            _imps.Add(imp);
            yield return null;
        }

        private void RemoveImp(Enemy imp)
        {
            imp.OnDestroyed -= RemoveImp;
            _imps.Remove(imp);
        }
    }
}