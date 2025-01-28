using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Project.Scripts.Weapon.ActiveSkills.MagicArrow
{
    [Serializable]
    public class MagicArrowSpawner : Spawner<MagicArrow>
    {
        [SerializeField] private float _radius;
        
        private float _delay;
        private LayerMask _layerMask;

        private float _speedMultiplier;
        private int _damageMultiplier;

        private List<MagicArrow> _magicArrows = new();

        private Transform _transform;

        public MagicArrowSpawner(MagicArrow magicArrowPrefab, Transform transform, float delay, float radius, LayerMask layerMask)
        {
            Prefab = magicArrowPrefab;
            Pool = new MagicArrowPool(Prefab, StartAmount);

            _transform = transform;
            _delay = delay;
            _radius = radius;
            _layerMask = layerMask;
            
            SpawnIterating().Forget();
        }

        public void ChangeArrowPrefab(MagicArrow magicArrow)
        {
            Unsubscribe();

            Pool = new MagicArrowPool(magicArrow, StartAmount);
        }

        public void ApplyStats(float speed, int damage, float delay, float radius)
        {
            _speedMultiplier = speed;
            _damageMultiplier = damage;
            _delay = delay;
            _radius = radius;
        }

        private Vector3 FindEnemyPosition()
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(_transform.position, _radius, _layerMask);

            if (enemies == null || enemies.Length == 0)
                return Vector3.zero;

            return enemies[Random.Range(0, enemies.Length)].transform.position;
        }

        private Quaternion CalculateRotation(Vector3 targetPosition)
        {
            Vector2 direction = (targetPosition - _transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            return Quaternion.Euler(0, 0, angle);
        }

        private async UniTask SpawnIterating()
        {
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_delay));

                var enemyPosition = FindEnemyPosition();
                var rotation = CalculateRotation(enemyPosition);
                var magicArrow = Spawn();

                if (!_magicArrows.Contains(magicArrow))
                    _magicArrows.Add(magicArrow);

                magicArrow.ApplyStats(_speedMultiplier, _damageMultiplier);
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