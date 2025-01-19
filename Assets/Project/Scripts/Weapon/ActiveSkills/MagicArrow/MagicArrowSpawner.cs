using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Weapon.ActiveSkills.MagicArrow
{
    public class MagicArrowSpawner : Spawner<MagicArrow>
    {
        [SerializeField] private float _delay;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _radius;

        private float _speedMultiplier;
        private int _damageMultiplier;
        
        private List<MagicArrow> _magicArrows = new ();
        
        private Transform _transform;

        private void Awake()
        {
            Pool = new MagicArrowPool(Prefab);
        }
        
        private void OnEnable()
        {
            _transform = transform;
            
            StartCoroutine(SpawnIterating());
        }

        public void ChangeArrowPrefab(MagicArrow magicArrow)
        {
            Unsubscribe();
            
            Pool = new MagicArrowPool(magicArrow);
        }

        public void ApplyStats(float speed, int damage)
        {
            _speedMultiplier = speed;
            _damageMultiplier = damage;
        }

        private Vector3 FindEnemyPosition()
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _radius, _layerMask);
            
            if(enemies == null || enemies.Length == 0)
                return Vector3.zero;
            
            return enemies[Random.Range(0, enemies.Length)].transform.position;
        }

        private Quaternion CalculateRotation(Vector3 target)
        {
            Vector2 direction = (target - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            return Quaternion.Euler(0, 0, angle);
        }

        private IEnumerator SpawnIterating()
        {
            var delay = new WaitForSeconds(_delay);
            
            while (isActiveAndEnabled)
            {
                yield return delay;

                var enemyPosition = FindEnemyPosition();
                var rotation = CalculateRotation(enemyPosition);
                var magicArrow = Spawn();
                
                if(_magicArrows.Contains(magicArrow) == false)
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
                arrow.OnDestroyed -= OnSpawnedDestroed;

                arrow.OnDestroyed += DestroyArrow;
            }
        }

        private void DestroyArrow(MagicArrow magicArrow)
        {
            Destroy(magicArrow.gameObject);
        }
    }
}