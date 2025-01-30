using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New MultiplyerEffector", menuName = "Skill/BulletEffectors/MultiplyerEffector",
    order = 51)]
public class MultiplyerEffector : BulletEffector
{
    [SerializeField] private Bullet _bonusBulletPrefab;
    [SerializeField] private float _spreadAngle = 30f;

    private int _bonusBulletsCount = 3;

    public override void Initialize(Weapon weapon)
    {
        Weapon = weapon;
        weapon.OnShooted += OnShooted;
    }

    public void SetBulletsCount(int bulletsCount)
    {
        if (bulletsCount <= 0) return;
        _bonusBulletsCount = bulletsCount;
    }

    private void OnShooted(Bullet bullet)
    {
        if (_bonusBulletsCount <= 0) return;

        bool isTotalOdd = (1 + _bonusBulletsCount) % 2 != 0;
        float startAngle, angleStep;

        // Расчет углов
        if (isTotalOdd)
        {
            angleStep = _spreadAngle / _bonusBulletsCount;
            startAngle = -_spreadAngle / 2 + angleStep / 2;
        }
        else
        {
            angleStep = _spreadAngle / (_bonusBulletsCount - 1);
            startAngle = -_spreadAngle / 2;
        }

        // Создание дополнительных пуль
        for (int i = 0; i < _bonusBulletsCount; i++)
        {
            float currentAngle = startAngle + i * angleStep;
            var spreadRotation = bullet.transform.rotation * Quaternion.Euler(0, 0, currentAngle);

            // Создание и инициализация пули
            Bullet newBullet = Instantiate(
                _bonusBulletPrefab, 
                bullet.transform.position, 
                spreadRotation
            );

            newBullet.Init(
                bullet.transform.position,
                spreadRotation,
                bullet.Damage // Передаем урон основной пули
            );

            newBullet.Activate(); // Активируем движение
        }
    }
}