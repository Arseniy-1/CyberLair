using Project.Scripts.Weapon;
using UnityEngine;

[CreateAssetMenu(fileName = "New ScaleEffector", menuName = "Skill/BulletEffectors/ScaleEffector", order = 51)]
public class ScaleEffector : BulletEffector
{
    [SerializeField] private float _scaleMultiplier = 1.5f;
    
    private Vector3 _originalScale;

    private void OnDisable()
    {
        if (Weapon != null)
        {
            Weapon.Shooted -= Shooted;
        }
    }

    public override void Initialize(Weapon weapon)
    {
        Weapon = weapon;
        weapon.Shooted += Shooted;
    }

    private void Shooted(Bullet bullet)
    {
        bullet.transform.localScale = Vector3.one * _scaleMultiplier;
    }
}