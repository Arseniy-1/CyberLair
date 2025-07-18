using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Project.Scripts.EnemySystem;
using Project.Scripts.Interfaces;
using Project.Scripts.Services.Enum;
using Project.Scripts.Skill;
using Project.Scripts.Spawners.ChainZap;
using Project.Scripts.Stats;
using Project.Scripts.Weapon;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Prefabs.Configs.Skills.ChainZap
{
    public class ChainZap : ISkillInstance
    {
        private const float Duration = 0.2f;
        private const int MaxHits = 8;
        
        private readonly Collider2D[] _results = new Collider2D[MaxHits];
    
        private readonly float _chainRadius;
        private readonly int _maxBounces;
        private readonly float _damageFalloff;
        private readonly LayerMask _enemyLayer;
        private readonly int _segments;
        private readonly float _chance;
        private readonly StatModifier _enemySpeedModifier;

        private readonly Weapon _weapon;
        private readonly ChainZapViewSpawner _viewSpawner;
    
        private Tween _fadeTween;

        public ChainZap(SkillData skillData, IChainZapStats stats)
        {
            _chainRadius = stats.ChainRadius;
            _maxBounces = stats.MaxBounces;
            _damageFalloff = stats.DamageFalloff;
            _enemyLayer = stats.EnemyLayer;
            _segments = stats.Segments;
            _chance = stats.Chance;
            _enemySpeedModifier = stats.EnemySpeedModifier;

            _weapon = skillData.WeaponHolder.Weapon;
            _weapon.Shot += InnerSubscribe;

            ChainZapView zapView = stats.ZapView;
            _viewSpawner = new ChainZapViewSpawner(zapView, 0);
        }

        public void Disable()
        {
            _weapon.Shot -= InnerSubscribe;
        
            _fadeTween?.Kill();
        }

        private void InnerSubscribe(Bullet bullet)
        {
            bullet.OnDestroyed += CastLightning;
        }

        private void CastLightning(Bullet bullet)
        {
            bullet.OnDestroyed -= CastLightning;

            if (Random.value > _chance)
                return;

            var hitTargets = new List<Enemy>();
            Vector2 currentPosition = bullet.Position;
            Enemy currentTarget = FindClosestTarget(currentPosition, hitTargets);
            
            for (int bounce = 0; bounce <= _maxBounces && currentTarget; bounce++)
            {
                if (currentTarget == false)
                    break;
                
                hitTargets.Add(currentTarget);

                currentTarget.TakeDamage(_weapon.WeaponStats.WeaponDamage.CurrentValue * Mathf.Pow(_damageFalloff, bounce));

                if (bounce != 0)
                {
                    DrawLightning(currentPosition, currentTarget.transform.position, bullet);

                    if (Enum.IsDefined(typeof(BossTypes), (BossTypes)(int)currentTarget.EnemyType))
                        currentTarget.EnemyStats.Speed.AddModifier(_enemySpeedModifier.Copy());
                }

                currentPosition = currentTarget.transform.position;
                currentTarget = FindClosestTarget(currentPosition, hitTargets);
            }
        }

        private Enemy FindClosestTarget(Vector3 position, List<Enemy> excludedTargets)
        {
            int hitCount = Physics2D.OverlapCircleNonAlloc(position, _chainRadius, _results, _enemyLayer);
            
            Enemy closestTarget = _results
                .Take(hitCount)
                .Select(hit =>
                {
                    hit.TryGetComponent(out Enemy enemy);
                    return enemy;
                })
                .Where(enemy => enemy && excludedTargets.Contains(enemy) == false)
                .OrderBy(enemy => Vector2.Distance(position, enemy.Position))
                .FirstOrDefault();
            
            return closestTarget;
        }

        private void DrawLightning(Vector2 start, Vector2 end, Bullet bullet)
        {
            ChainZapView view = _viewSpawner.Spawn();
            
            view.gameObject.SetActive(true);
            view.transform.position = bullet.transform.position;
            view.ZapView.positionCount = _segments;

            for (int i = 0; i < _segments; i++)
            {
                float t = i / (float)(_segments - 1);
                Vector2 point = Vector2.Lerp(start, end, t);
                float offset = Random.Range(-0.2f, 0.2f);

                point += Vector2.Perpendicular(end - start).normalized * offset;
                view.ZapView.SetPosition(i, point);
            }

            view.ZapView.material.mainTextureScale = new Vector2(Vector2.Distance(start, end), 1f);

            _fadeTween?.Kill();

            _fadeTween = view.ZapView.material
                .DOFade(0f, Duration)
                .SetEase(Ease.InOutFlash)
                .OnKill(view.Disable);
        }
    }
}