using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Passive/UpdateWeapon", order = 51)]
public class WeaponUpdateSkill : Skill
{
    [SerializeField] private StatModifier _damageStatModifier;
    [SerializeField] private StatModifier _damageStatModifier2;
    
    public override void Apply(SkillData skillData)
    {
        skillData.PlayerStats.WeaponDamage.AddModifier(_damageStatModifier);
        skillData.PlayerStats.WeaponDamage.AddModifier(_damageStatModifier2);
    }
}