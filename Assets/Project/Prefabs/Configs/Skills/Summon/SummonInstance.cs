using UnityEngine;

public class SummonInstance : SkillInstance
{
    private Summon _summonInstance;
    
    public SummonInstance(SkillData skillData, SummonSkill skill)
    {
        Transform weaponHolderTransform = skillData.WeaponHolder.transform;
        
        var summon = Object.Instantiate(skill.SummonPrefab, weaponHolderTransform.position, weaponHolderTransform.rotation);
        summon.Initialize(weaponHolderTransform);
    }

    public override void Disable()
    {
        Object.Destroy(_summonInstance);
    }
}