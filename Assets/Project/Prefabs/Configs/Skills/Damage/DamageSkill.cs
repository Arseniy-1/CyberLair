using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Passive/Damage", order = 51)]
public class DamageSkill : Skill
{
    [SerializeField] private SkillConfig _skillConfig;
    [SerializeField] private ScaleEffector _scaleEffector;
    
    public override void Apply(SkillData skillData)
    {
        Debug.Log(skillData.Level);
        skillData.PlayerStats.SetDamage((int)(skillData.StartPlayerStats.WeaponDamage *
                                                   _skillConfig.Multipliers[skillData.Level]));
        
        if (skillData.Level == MaxLevel)
        {
        }
    }
}