using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Passive/Damage", order = 51)]
public class DamageSkill : Skill
{
    [SerializeField] private SkillConfig _skillConfig;
    
    public override void Apply(SkillData skillData)
    {
        skillData.PlayerStats.SetDamage((int)(skillData.StartPlayerStats.Damage *
                                                   _skillConfig.Multipliers[skillData.Level]) - skillData.StartPlayerStats.Damage);
        
        Debug.Log(ReferenceEquals(skillData.StartPlayerStats, skillData.PlayerStats));
        Debug.Log(skillData.StartPlayerStats.Damage);
        Debug.Log(skillData.PlayerStats.Damage);
    }
}