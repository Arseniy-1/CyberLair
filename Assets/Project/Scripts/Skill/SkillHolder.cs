using System.Collections.Generic;
using Project.Prefabs.Configs.Skills;
using Project.Prefabs.Configs.Skills.AffectedArea;
using Project.Prefabs.Configs.Skills.ArtayaShield;
using Project.Prefabs.Configs.Skills.Athletics;
using Project.Prefabs.Configs.Skills.Boomerang;
using Project.Prefabs.Configs.Skills.BulletonsLast;
using Project.Prefabs.Configs.Skills.Durability;
using Project.Prefabs.Configs.Skills.FireZone;
using Project.Prefabs.Configs.Skills.Harding;
using Project.Prefabs.Configs.Skills.InternalVoltage;
using Project.Prefabs.Configs.Skills.JumpSwirl;
using Project.Prefabs.Configs.Skills.Lair_1;
using Project.Prefabs.Configs.Skills.Zap;

public class SkillHolder
{
    private readonly SkillData _skillData;
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
                _skillInstances.Add(new Durability(_skillData, affectedAreaSkill));
                break;
            
            case AffectedAreaSkill affectedAreaSkill:
                _skillInstances.Add(new AffectedArea(_skillData, affectedAreaSkill));
                break;
            
            case ArtayaShieldSkill artayaShieldSkill:
                _skillInstances.Add(new ArtayaShield(_skillData, artayaShieldSkill));
                break;
            
            case AthleticsSkill athleticsSkill:
                _skillInstances.Add(new Athletics(_skillData, athleticsSkill));
                break;
            
            case BerserkRageSkill berserkRageSkill:
                _skillInstances.Add(new BerserkHealthRegenerator(_skillData, berserkRageSkill));
                break;
            
            case BulletonsLastSkill:
                _skillInstances.Add(new BulletonsLast(_skillData));
                break;
            
            case ChainZapSkill chainZapSkill:
                _skillInstances.Add(new ChainZap(_skillData, chainZapSkill));
                break;
            
            case BoomerangSkill boomerangSkill:
                _skillInstances.Add(new Boomerang(_skillData, boomerangSkill));
                break;
            
            case FireZoneSkill fireZoneSkill:
                _skillInstances.Add(new FireZoneManager(_skillData, fireZoneSkill));
                break;
            
            case FirstAidSkill firstAidSkill:
                _skillInstances.Add(new FirstAid(_skillData, firstAidSkill));
                break;
            
            case HardingSkill hardingSkill:
                _skillInstances.Add(new Harding(_skillData, hardingSkill));
                break;
            
            case InternalVoltageSkill internalVoltageSkill:
                _skillInstances.Add(new InternalVoltage(_skillData, internalVoltageSkill));
                break;
            
            case JumpSwirlSkill jumpSwirlSkill:
                _skillInstances.Add(new JumpSwirl(_skillData, jumpSwirlSkill));
                break;
            
            case LairOneSkill lairOneSkill:
                _skillInstances.Add(new LairOne(_skillData, lairOneSkill));
                break;
        }
    }
}