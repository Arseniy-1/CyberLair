using UnityEngine;

public abstract class PassiveSkill : ISkill
{
    [field: SerializeField] public SkillInfo SkillInfo { get; }

    protected PassiveSkillConfig PassiveSkillConfig;

    public abstract void Apply(PlayerStats playerStats, PlayerConfig playerConfig, int level);
}

public class HealthSkill : PassiveSkill
{
    public override void Apply(PlayerStats playerStats, PlayerConfig playerConfig, int level)
    {
        playerStats.Damage = playerConfig.Damage * PassiveSkillConfig.Multipliers[level];
        Debug.Log("1");
    }
}