using System;
using Random = UnityEngine.Random;

namespace Project.Prefabs.Configs.Skills.FireZone
{
    [Serializable]
    public class FireZoneManager : ISkillInstance
    {
        private FireZoneSpawner _fireZoneSpawner;
        private float _chance;

        private SkillData _skillData;
        
        public event Action<FireZone> FireZoneSpawned;
        
        public FireZoneManager(SkillData skillData, FireZoneSkill fireZoneSkill)
        {
            _skillData = skillData;

            _chance = fireZoneSkill.SpawnChance;
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
            
            FireZoneSpawned?.Invoke(fireZone);
        }

        public void Disable()
        {
            _skillData.WeaponHolder.Weapon.Shooted -= OnShot;
        }
    }
}