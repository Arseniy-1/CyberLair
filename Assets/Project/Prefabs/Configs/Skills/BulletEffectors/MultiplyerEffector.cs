using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New MultiplyerEffector", menuName = "Skill/BulletEffectors/MultiplyerEffector", order = 51)]
public class MultiplyerEffector : BulletEffector
{
    [SerializeField] private Bullet _bonusBulletPrefab;
    
    private int _bonusBulletsCount = 1;

    public override void Initialize(Weapon weapon)
    {
        Weapon = weapon;
        weapon.OnShooted += OnShooted;
    }

    public void SetBulletsCount(int bulletsCount)
    {
        if (bulletsCount <= 0)
            return;

        _bonusBulletsCount = bulletsCount;
    }

    private void OnShooted(Bullet bullet)
    {
        Vector3 bulletDirection = bullet.transform.forward;

        Instantiate(_bonusBulletPrefab, bullet.transform.position, bullet.transform.rotation);
    }
}


