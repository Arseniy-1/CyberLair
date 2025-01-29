using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Passive/UpdateWeapon", order = 51)]
public class WeaponUpdateSkill : Skill
{
    [SerializeField] private SkillConfig _damageConfig;
    [SerializeField] private SkillConfig _magazineSizeConfig;
    [SerializeField] private SkillConfig _reloadTimeConfig;
    [SerializeField] private SkillConfig _rechargeTimeConfig;
    
    [SerializeField] private ScaleEffector _scaleEffector;
    
    public override void Apply(SkillData skillData)
    {
        skillData.PlayerStats.SetDamage((int)(skillData.StartPlayerStats.WeaponDamage *
                                                   _damageConfig.Multipliers[skillData.Level - 1]));
        
        skillData.PlayerStats.SetMagazineSize((int)(skillData.StartPlayerStats.WeaponMagazineSize *
                                                    _magazineSizeConfig.Multipliers[skillData.Level - 1]));
        
        skillData.PlayerStats.SetWeaponRealoadTime(skillData.StartPlayerStats.WeaponBulletReloadTime *
                                                    _reloadTimeConfig.Multipliers[skillData.Level - 1]);
        
        skillData.PlayerStats.SetWeaponRechargeTime(skillData.StartPlayerStats.WeaponRechargingTime *
                                                          _rechargeTimeConfig.Multipliers[skillData.Level - 1]);
        
        if (skillData.Level == MaxLevel)
        {
            skillData.WeaponHolder.Weapon.ApplyEffector(_scaleEffector);
        }
    }
}