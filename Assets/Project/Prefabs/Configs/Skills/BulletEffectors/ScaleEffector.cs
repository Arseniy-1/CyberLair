using System;
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
            Weapon.OnShooted -= OnShooted;
        }
    }

    public override void Initialize(Weapon weapon)
    {
        Weapon = weapon;
        weapon.OnShooted += OnShooted;
    }

    private void OnShooted(Bullet bullet)
    {
        bullet.transform.localScale = Vector3.one * _scaleMultiplier;
    }
}