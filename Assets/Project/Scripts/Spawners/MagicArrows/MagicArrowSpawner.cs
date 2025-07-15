using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Prefabs.Configs.Skills.MagicArrow;
using Project.Scripts.Interfaces;
using Project.Scripts.Skill;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Project.Scripts.Spawners.MagicArrows
{
    [Serializable]
    public class MagicArrowSpawner : Spawner<MagicArrow>, ISkillInstance
    {
        private const int MaxHits = 8;
        
        private readonly Collider2D[] _results = new Collider2D[MaxHits];
        
        private float _radius;
        private float _delay;
        private LayerMask _layerMask;

        private List<MagicArrow> _magicArrows = new();
        private CancellationTokenSource _cancellationToken;
        
        private Transform _transform;

        public MagicArrowSpawner(SkillData skillData, IMagicArrowStats skill)
        {
            _radius = skill.Radius;
            _delay = skill.Delay;
            _layerMask = skill.LayerMask;
            
            Prefab = skill.MagicArrowPrefab;
            Pool = new MagicArrowPool(Prefab, StartAmount);

            _transform = skillData.WeaponHolder.transform;
            
            _cancellationToken = new CancellationTokenSource();
            
            SpawnIterating(_cancellationToken.Token).Forget();
        }

        public void Disable()
        {
            Unsubscribe();
            
            _cancellationToken?.Cancel();
        }

        private Vector3 FindEnemyPosition()
        {
            int hitCount = Physics2D.OverlapCircleNonAlloc(_transform.position, _radius, _results, _layerMask);

            if (_results != null && hitCount != 0)
                return _results[Random.Range(0, _results.Length)].transform.position;
            
            Vector3 randomOffset = Random.insideUnitCircle;
                
            return _transform.position + randomOffset;
        }

        private Quaternion CalculateRotation(Vector3 targetPosition)
        {
            Vector2 direction = (targetPosition - _transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            return Quaternion.Euler(0, 0, angle);
        }

        private async UniTask SpawnIterating(CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_delay), cancellationToken: token);
                
                var enemyPosition = FindEnemyPosition();
                var rotation = CalculateRotation(enemyPosition);
                var magicArrow = Spawn();

                if (_magicArrows.Contains(magicArrow) == false)
                    _magicArrows.Add(magicArrow);

                magicArrow.transform.position = _transform.position;
                magicArrow.transform.rotation = rotation;
            }
        }

        private void Unsubscribe()
        {
            foreach (MagicArrow arrow in _magicArrows)
            {
                arrow.OnDestroyed -= OnSpawnedDestroyed;

                arrow.OnDestroyed += DestroyArrow;
            }
        }

        private void DestroyArrow(MagicArrow magicArrow)
        {
            magicArrow.OnDestroyed -= DestroyArrow;
            
            Object.Destroy(magicArrow.gameObject);
        }
    }
}