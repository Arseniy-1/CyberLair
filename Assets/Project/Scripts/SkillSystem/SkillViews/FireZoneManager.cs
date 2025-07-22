using System;
using Project.Scripts.Interfaces;
using Project.Scripts.SkillSystem.SkillInstances;
using Project.Scripts.SkillSystem.SkillSOClasses;
using Project.Scripts.Spawners.FireZones;
using Project.Scripts.Weapon;
using Random = UnityEngine.Random;

namespace Project.Scripts.SkillSystem.SkillViews
{
    [Serializable]
    public class FireZoneManager : ISkillInstance
    {
        private FireZoneSpawner _fireZoneSpawner;
        private float _chance;

        private SkillData _skillData;
        
        public FireZoneManager(SkillData skillData, FireZoneSkill fireZoneSkill)
        {
            _skillData = skillData;

            _chance = fireZoneSkill.SpawnChance;
            _fireZoneSpawner = new FireZoneSpawner(fireZoneSkill.FireZonePrefab);
            skillData.WeaponHolder.Weapon.Shot += OnShot;
        }
        
        public event Action<FireZone> FireZoneSpawned;
        
        public void Disable()
        {
            _skillData.WeaponHolder.Weapon.Shot -= OnShot;
        }

        private void OnShot(Bullet bullet)
        {
            bullet.OnDestroyed += Explode;
        }

        private void Explode(Bullet bullet)
        {
            bullet.OnDestroyed -= Explode;
            
            if (Random.value >= _chance)
                return;

            var fireZone = _fireZoneSpawner.Spawn();
            fireZone.transform.position = bullet.Position;
            
            FireZoneSpawned?.Invoke(fireZone);
        }
    }
}