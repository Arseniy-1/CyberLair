using Project.Prefabs.Configs.Skills.Durability;
using UnityEngine;

public class SummonInstance : ISkillInstance
{
    private Summon _summonInstance;
    
    public SummonInstance(SkillData skillData, SummonSkill skill)
    {
        Transform weaponHolderTransform = skillData.WeaponHolder.transform;
        
        var summon = Object.Instantiate(skill.SummonPrefab, weaponHolderTransform.position, weaponHolderTransform.rotation);
        summon.Initialize(weaponHolderTransform);
    }

    public void Disable()
    {
        Object.Destroy(_summonInstance);
    }
}