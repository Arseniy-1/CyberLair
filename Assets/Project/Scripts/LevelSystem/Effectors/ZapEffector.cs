using System.Collections.Generic;
using Project.Scripts.EnemySystem;
using Project.Scripts.Weapon;
using UnityEngine;
using DG.Tweening; // Подключаем DoTween

[CreateAssetMenu(fileName = "New ZapEffector", menuName = "Skill/BulletEffectors/ZapEffector", order = 51)]
public class ZapEffector : BulletEffector
{
    [SerializeField] private float _chainRadius = 5f; // Радиус поиска следующей цели
    [SerializeField] private int _maxBounces = 5; // Максимальное количество отскоков
    [SerializeField] private float _damageFalloff = 0.8f; // Падение урона с каждым отскоком
    [SerializeField] private LineRenderer _linePrefab; // Префаб для эффекта молнии
    [SerializeField] private LayerMask _enemyLayer; // Слой врагов

    public override void Initialize(Weapon weapon)
    {
        Weapon = weapon;
        weapon.OnShooted += OnShooted;
    }

    private void OnShooted(Bullet bullet)
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

            if (bounce != 0) // Эффект молнии только для последующих отскоков
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

        // Устанавливаем извилистость
        int segments = 20;
        line.positionCount = segments;

        for (int i = 0; i < segments; i++)
        {
            float t = i / (float)(segments - 1);
            Vector2 point = Vector2.Lerp(start, end, t);
            float offset = Random.Range(-0.2f, 0.2f);
            point += Vector2.Perpendicular(end - start).normalized * offset;
            line.SetPosition(i, point);
        }

        // Применяем текстуру молнии
        line.material.mainTextureScale = new Vector2(Vector2.Distance(start, end), 1f);

        // Анимация мигания с помощью DoTween
        Color initialColor = line.material.color;
        float duration = 0.2f; // Длительность жизни линии
        line.material.DOFade(0f, duration).SetEase(Ease.InOutFlash).OnComplete(() =>
        {
            Destroy(line.gameObject); // Удаление линии после анимации
        });
    }
}