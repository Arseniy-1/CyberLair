using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Passive/Health", order = 51)]
public class HealthSkill : PassiveSkill
{
    public override void Apply(PlayerStats playerStats, PlayerConfig playerConfig, int level)//TODO: попробовать связать общим классом
    {
        playerStats.Health.IncreaseHealth((int)(playerConfig.Health.MaxValue * skillConfig.Multipliers[level]) - playerConfig.Health.MaxValue);
        Debug.Log("Health Skill Applied");
    }
}