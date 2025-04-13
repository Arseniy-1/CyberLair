using System.Collections.Generic;
using Project.Scripts.EnemySystem;
using UnityEngine;
using DG.Tweening;
using Project.Prefabs.Configs.Skills.Zap;
using Project.Scripts.Weapon;
using Random = UnityEngine.Random;

public class ChainZap : ISkillInstance
{
    private readonly float _chainRadius;
    private readonly int _maxBounces;
    private readonly float _damageFalloff;
    private readonly LayerMask _enemyLayer;
    private readonly int _segments;
    private readonly float _chance;
    private readonly StatModifier _enemySpeedModifier;

    private readonly Weapon _weapon;
    private readonly ChainZapViewSpawner _viewSpawner;

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
        _weapon.Shooted += InnerSubscribe;

        ChainZapView zapView = stats.ZapView;
        _viewSpawner = new ChainZapViewSpawner(zapView, 0);
    }

    public void Disable()
    {
        _weapon.Shooted -= InnerSubscribe;
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

        List<Enemy> hitTargets = new List<Enemy>();
        Vector2 currentPosition = bullet.transform.position;
        Enemy currentTarget = FindClosestTarget(currentPosition, hitTargets);

        if (currentTarget == false)
            return;

        for (int bounce = 0; bounce <= _maxBounces && currentTarget; bounce++)
        {
            hitTargets.Add(currentTarget);

            currentTarget.TakeDamage(_weapon.WeaponStats.WeaponDamage.CurrentValue * Mathf.Pow(_damageFalloff, bounce));

            if (bounce != 0)
            {
                currentTarget.EnemyStats.Speed.AddModifier(_enemySpeedModifier.Copy());
                DrawLightning(currentPosition, currentTarget.transform.position, bullet);
            }

            currentPosition = currentTarget.transform.position;
            currentTarget = FindClosestTarget(currentPosition, hitTargets);
        }
    }

    private Enemy FindClosestTarget(Vector3 position, List<Enemy> excludedTargets)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, _chainRadius, _enemyLayer);

        Enemy closestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out Enemy target) && !excludedTargets.Contains(target))
            {
                float distance = Vector2.Distance(position, target.transform.position);

                if (distance < closestDistance)
                {
                    closestTarget = target;
                    closestDistance = distance;
                }
            }
        }

        return closestTarget;
    }

    private void DrawLightning(Vector2 start, Vector2 end, Bullet bullet)
    {
        ChainZapView view = _viewSpawner.Spawn();
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

        float duration = 0.2f;
        view.ZapView.material.DOFade(0f, duration).SetEase(Ease.InOutFlash).OnComplete(() =>
        {
            view.Disable();
            view.ZapView.material.DOFade(1f, 0f);
        });
    }
}