using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Weapon;
using UnityEngine;

[CreateAssetMenu(fileName = "New FireZoneEffector", menuName = "Skill/BulletEffectors/FireZoneEffector",order = 51)]
public class FireZoneEffector : BulletEffector
{
    [SerializeField] private FireZone _fireZonePrefab;
    [SerializeField] private FireZoneSpawner _fireZoneSpawner;
    public override void Initialize(Weapon weapon)
    {
        _fireZoneSpawner = new FireZoneSpawner(_fireZonePrefab);
        Weapon = weapon;
        weapon.OnShot += OnShot;
    }

    private void OnShot(Bullet bullet)
    {
        bullet.OnDestroyed += Explode;
    }

    private void Explode(Bullet bullet)
    {
        bullet.OnDestroyed -= Explode;

        var fireZone = _fireZoneSpawner.Spawn();
        fireZone.transform.position = bullet.transform.position;
    }
}