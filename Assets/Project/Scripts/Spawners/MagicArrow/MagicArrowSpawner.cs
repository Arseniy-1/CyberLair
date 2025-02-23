using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Project.Prefabs.Configs.Skills.Durability;
using Project.Scripts.LevelSystem.ActiveSkills;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Project.Scripts.Weapon.ActiveSkills.MagicArrow
{
    [Serializable]
    public class MagicArrowSpawner : Spawner<MagicArrow>, ISkillInstance
    {
        private float _radius;
        private float _delay;
        private LayerMask _layerMask;

        private List<MagicArrow> _magicArrows = new();
        private bool _isActive;
        private CancellationTokenSource _cancellationToken;
        
        private Transform _transform;

        public MagicArrowSpawner(SkillData skillData, MagicArrowSkill skill)
        {
            _radius = skill.Radius;
            _delay = skill.Delay;
            _layerMask = skill.LayerMask;
            
            Prefab = skill.MagicArrowPrefab;
            Pool = new MagicArrowPool(Prefab, StartAmount);

            _transform = skillData.WeaponHolder.transform;
            
            _isActive = true;
            _cancellationToken = new CancellationTokenSource();
            SpawnIterating().Forget();
        }

        public void Disable()
        {
            Unsubscribe();
            _isActive = false;
            
            _cancellationToken.Cancel();
        }

        private Vector3 FindEnemyPosition()
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(_transform.position, _radius, _layerMask);

            if (enemies != null && enemies.Length != 0)
                return enemies[Random.Range(0, enemies.Length)].transform.position;
            
            Vector3 randomOffset = Random.insideUnitCircle;
                
            return _transform.position + randomOffset;
        }

        private Quaternion CalculateRotation(Vector3 targetPosition)
        {
            Vector2 direction = (targetPosition - _transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            return Quaternion.Euler(0, 0, angle);
        }

        private async UniTask SpawnIterating()
        {
            while (_isActive)
            {
                try
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(_delay), cancellationToken: _cancellationToken.Token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                
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
            Object.Destroy(magicArrow.gameObject);
        }
    }
}