using System;
using Project.Prefabs.Configs.Skills.Durability;
using UnityEngine;

[Serializable]
public class PainHealler : ISkillInstance
{
    private SkillData _data;
    private RecoveryPainSkill _skill;
    
    public PainHealler(SkillData skillData, RecoveryPainSkill skill)
    {
        _data = skillData;
        _skill = skill;
        
        _data.PlayerStats.Health.DamageTaken += Heal;
    }

    private void Heal(float amount)
    {
        _data.PlayerStats.HealthRegenerateAmount.AddModifier(_skill.RegenerateModifier.Copy());
    }

    public void Disable()
    {
        throw new NotImplementedException();
    }
}