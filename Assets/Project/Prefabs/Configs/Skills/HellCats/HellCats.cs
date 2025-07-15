using Project.Prefabs.Configs.Skills.FireZone;
using Project.Scripts.Interfaces;
using Project.Scripts.Spawners.HellCats;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.HellCats
{
    public class HellCats : ISkillInstance
    {
        private const float SpawnOffsetScale = 0.7f;
    
        private readonly HellCatSpawner _hellCatSpawner;
        private readonly FireZoneManager _fireZoneInstance;

        private readonly HellCatsSkill _skill;

        public HellCats(HellCatsSkill skill, FireZoneManager fireZoneInstance)
        {
            _skill = skill;
            _fireZoneInstance = fireZoneInstance;
            _hellCatSpawner = new HellCatSpawner(_skill.HellCatPrefab);

            _fireZoneInstance.FireZoneSpawned += OnFireZoneSpawned;
        }

        public void Disable()
        {
            _fireZoneInstance.FireZoneSpawned -= OnFireZoneSpawned;
        }

        private void OnFireZoneSpawned(FireZone.FireZone fireZone)
        {
            int catsCount = Random.Range(_skill.MinSummonCount, _skill.MaxSummonCount);

            for (int i = 0; i < catsCount; i++)
            {
                var hellCat = _hellCatSpawner.Spawn();
                hellCat.transform.position = GetSpawnPosition(fireZone.transform.position);
            }
        }
    
        private Vector2 GetSpawnPosition(Vector2 basePosition)
        {
            Vector2 offset = Random.insideUnitCircle * SpawnOffsetScale;
        
            return basePosition + offset;
        }
    }
}