using System;
using Project.Prefabs.Configs.Skills.Durability;
using Project.Scripts.Weapon;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Prefabs.Configs.Skills.FireZone
{
    [Serializable]
    public class FireZoneManager : SkillInstance
    {
        private FireZoneSpawner _fireZoneSpawner;
        private float _chance;

        private SkillData _skillData;
        
        public FireZoneManager(SkillData skillData, FireZoneSkill fireZoneSkill)
        {
            _skillData = skillData;
            
            _fireZoneSpawner = new FireZoneSpawner(fireZoneSkill.FireZonePrefab);
            skillData.WeaponHolder.Weapon.Shooted += OnShot;
        }

        private void OnShot(Bullet bullet)
        {
            bullet.OnDestroyed += Explode;
        }

        private void Explode(Bullet bullet)
        {
            bullet.OnDestroyed -= Explode;
            
            if(Random.value >= _chance)
                return;

            var fireZone = _fireZoneSpawner.Spawn();
            fireZone.transform.position = bullet.transform.position;
        }

        public override void Disable()
        {
            _skillData.WeaponHolder.Weapon.Shooted -= OnShot;
        }
    }
}