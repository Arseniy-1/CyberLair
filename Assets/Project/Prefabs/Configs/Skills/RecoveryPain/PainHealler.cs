using System;
using UnityEngine;

[Serializable]
public class PainHealler : SkillInstance
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

    public override void Disable()
    {
        throw new NotImplementedException();
    }
}