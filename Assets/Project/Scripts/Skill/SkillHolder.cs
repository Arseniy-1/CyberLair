using System.Collections.Generic;
using System.Linq;
using Project.Prefabs.Configs.Skills.AffectedArea;
using Project.Prefabs.Configs.Skills.ArtayaShield;
using Project.Prefabs.Configs.Skills.Boomerang;
using Project.Prefabs.Configs.Skills.Durability;
using Project.Prefabs.Configs.Skills.StormBlade;
using Project.Scripts.Weapon.ActiveSkills;

public class SkillHolder
{
    private SkillData _skillData;
    private List<SkillInstance> _skillInstances = new();

    public SkillHolder(SkillData skillData)
    {
        _skillData = skillData;
    }

    public void CreateSkillHui(Skill skill)
    {
        switch (skill)
        {
            case DurabilitySkill affectedAreaSkill:
                _skillInstances.Add(new Durability(_skillData, affectedAreaSkill, this));
                break;

            case AffectedAreaSkill affectedAreaSkill:
                _skillInstances.Add(new AffectedArea(_skillData, affectedAreaSkill, this));
                break;

            case ArtayaShieldSkill artayaShieldSkill:
                _skillInstances.Add(new ArtayaShield(_skillData, artayaShieldSkill));
                break;

            case WeaponUpdateSkill weaponUpdateSkill:
                _skillInstances.Add(new WeaponUpdate(_skillData, weaponUpdateSkill));
                break;
            
            case ThunderSkill thunderSkill:
                _skillInstances.Add(new Thunder(_skillData, thunderSkill));
                break;
                
            case SummonSkill summonSkill:
                _skillInstances.Add(new SummonInstance(_skillData, summonSkill));
                break;
            
            case StormBladeSkill stormBladeSkill:
                BoomerangOrbital boomerangInstance = _skillInstances.FirstOrDefault();
                _skillInstances.Add(new StormBlade(stormBladeSkill, boomerangInstance));
                break;
            
            case RecoveryPainSkill recoveryPainSkill:
                _skillInstances.Add(new PainHealler(_skillData, recoveryPainSkill));
                break;
        }
    }
}