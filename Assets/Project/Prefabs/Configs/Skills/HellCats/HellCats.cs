using Project.Prefabs.Configs.Skills.FireZone;
using UnityEngine;

public class HellCats : ISkillInstance
{
    private HellCatSpawner _hellCatSpawner;
    private FireZoneManager _fireZoneInstance;

    private HellCatsSkill _skill;

    public HellCats(HellCatsSkill skill, FireZoneManager fireZoneInstance)
    {
        //Котятки, люблю котяток :3 ^_^ мяу
        _skill = skill;
        _fireZoneInstance = fireZoneInstance;
        _hellCatSpawner = new HellCatSpawner(_skill.HellCatPrefab);

        _fireZoneInstance.FireZoneSpawned += OnFireZoneSpawned;
    }

    public void Disable()
    {
        _fireZoneInstance.FireZoneSpawned -= OnFireZoneSpawned;
    }

    private void OnFireZoneSpawned(FireZone fireZone)
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
        float spawnOffsetX = 0.7f;
        float spawnOffsetY = 0.7f;
        
        Vector2 spawnOffset = new Vector2(spawnOffsetX, spawnOffsetY);
        
        float randomX = Random.Range(-spawnOffset.x, spawnOffset.x);
        float randomY = Random.Range(-spawnOffset.y, spawnOffset.y);
        
        return new Vector2(basePosition.x + randomX, basePosition.y + randomY);
    }
}