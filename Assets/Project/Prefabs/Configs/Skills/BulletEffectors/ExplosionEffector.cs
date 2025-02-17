using Project.Scripts.Servises;
using UnityEngine;
using Project.Scripts.Weapon;

[CreateAssetMenu(fileName = "New ExplosionEffector", menuName = "Skill/BulletEffectors/ExplosionEffector",order = 51)]
public class ExplosionEffector : BulletEffector
{
    [SerializeField] private Explosion _explosion;
    
    public override void Initialize(Weapon weapon)
    {
        Weapon = weapon;
        weapon.Shooted += Shooted;
    }

    private void Shooted(Bullet bullet)
    {
        bullet.OnDestroyed += Explode;
    }

    private void Explode(Bullet bullet)
    {
        bullet.OnDestroyed -= Explode;
        
        _explosion.Explode(bullet.transform.position);
    }
}