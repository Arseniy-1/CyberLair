using System;

[Serializable]
public class PainHealer : ISkillInstance
{
    private SkillData _data;
    private RecoveryPainSkill _skill;
    
    public PainHealer(SkillData skillData, RecoveryPainSkill skill)
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
        _data.PlayerStats.Health.DamageTaken -= Heal;
    }
}