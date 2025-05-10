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
            Weapon.Shot -= Shot;
        }
    }

    public override void Initialize(Weapon weapon)
    {
        Weapon = weapon;
        weapon.Shot += Shot;
    }

    private void Shot(Bullet bullet)
    {
        bullet.transform.localScale = Vector3.one * _scaleMultiplier;
    }
}