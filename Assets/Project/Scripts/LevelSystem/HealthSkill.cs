using UnityEngine;

public class HealthSkill : PassiveSkill
{
    public override void Apply(PlayerStats playerStats, PlayerConfig playerConfig, int level)
    {
        playerStats.Damage = playerConfig.Damage * PassiveSkillConfig.Multipliers[level];
        Debug.Log("1");
    }
}