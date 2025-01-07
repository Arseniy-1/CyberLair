using UnityEngine;

public abstract class PassiveSkill : Skill
{
    [field: SerializeField] public SkillInfo SkillInfo;

    [field: SerializeField] protected PassiveSkillConfig PassiveSkillConfig;

    public abstract void Apply(PlayerStats playerStats, PlayerConfig playerConfig, int level);
}