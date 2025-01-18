using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.EnemySystem;
using Project.Scripts.Weapon;
using UnityEngine;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = "New ThunderBulletEffector", menuName = "Skill/BulletEffectors/ThunderBulletEffector", order = 51)]
public class ThunderBulletEffector : BulletEffector
{
    [SerializeField] private float _chainRadius = 5f; // Радиус поиска следующей цели
    [SerializeField] private int _maxBounces = 5; // Максимальное количество отскоков
    [SerializeField] private float _damageFalloff = 0.8f; // Падение урона с каждым отскоком
    [SerializeField] private LineRenderer _linePrefab; // Префаб для эффекта молнии
    [SerializeField] private LayerMask _enemyLayer; // Слой врагов

    public override void Initialize(Weapon weapon)
    {
        Weapon = weapon;
        weapon.OnShooted += RegisterBullet;
    }

    private void RegisterBullet(Bullet bullet)
    {
        bullet.OnDestroyed += CastLightning;
    }

    private void CastLightning(Bullet bullet)
    {
        bullet.OnDestroyed -= CastLightning;

        List<Enemy> hitTargets = new List<Enemy>();
        Vector2 currentPosition = bullet.transform.position;
        Enemy currentTarget = FindClosestTarget(currentPosition, hitTargets);

        if (currentTarget == null)
            return; // Если нет цели, прерываем цепь

        for (int bounce = 0; bounce < _maxBounces && currentTarget != null; bounce++)
        {
            hitTargets.Add(currentTarget);

            // Наносим урон
            int currentDamage = Mathf.RoundToInt(bullet.Damage * Mathf.Pow(_damageFalloff, bounce));
            currentTarget.TakeDamage(currentDamage);

            if(bounce != 0)
                DrawLightning(currentPosition, currentTarget.transform.position, bullet);

            // Переходим к следующей цели
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
        LineRenderer line = Instantiate(_linePrefab, bullet.transform.position, Quaternion.identity);
        line.SetPosition(0, start);
        line.SetPosition(1, end);

        // Удаляем линию после небольшой задержки
        Destroy(line.gameObject, 0.1f);
    }
}