using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Passive/Health", order = 51)]
public class HealthSkill : PassiveSkill
{
    public override void Apply(PlayerStats playerStats, PlayerConfig playerConfig, int level)//TODO: попробовать связать общим классом
    {
        playerStats.Damage = playerConfig.Damage * PassiveSkillConfig.Multipliers[level];
        Debug.Log("Health Skill Applied");
    }
}