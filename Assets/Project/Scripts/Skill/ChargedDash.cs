using UnityEngine;

[CreateAssetMenu(fileName = "ChargedDash", menuName = "Skill/Hard/ChargedDash", order = 51)]
public class ChargedDash : HardSkill
{
    [SerializeField] private StatModifier _speedStatModifier;
    [SerializeField] private StatModifier _chargeTimeStatModifier;
    
    public override void Apply(SkillData skillData)
    {
        skillData.PlayerStats.WeaponDamage.AddModifier(_speedStatModifier);
        skillData.PlayerStats.WeaponDamage.AddModifier(_chargeTimeStatModifier);
    }
}