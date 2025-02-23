using System.Collections.Generic;
using System.Linq;
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
using Project.Prefabs.Configs.Skills.MercuryMimicry;
using Project.Prefabs.Configs.Skills.StormBlade;
using Project.Prefabs.Configs.Skills.StunZap;
using Project.Scripts.Weapon.ActiveSkills;
using Project.Scripts.Weapon.ActiveSkills.MagicArrow;

public class SkillHolder
{
    private readonly SkillData _skillData;
    private List<ISkillInstance> _skillInstances = new();

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
            
            case MagicArrowSkill magicArrowSkill:
                _skillInstances.Add(new MagicArrowSpawner(_skillData, magicArrowSkill));
                break;
            
            case MercuryMimicrySkill mercuryMimicrySkill:
                _skillInstances.Add(new MercuryMimicry(_skillData, mercuryMimicrySkill));
                break;
            
            case MultishotSkill multishotSkill:
                _skillInstances.Add(new Multishot(_skillData, multishotSkill));
                break;
            
            case OverloadSkill overloadSkill:
                _skillInstances.FirstOrDefault(skill => skill.GetType() == typeof(Thunder)).Disable();
                
                _skillInstances.Add(new Thunder(_skillData, overloadSkill));
                break;
            
            case PhantomArrowsSkill phantomArrowsSkill:
                _skillInstances.FirstOrDefault(skill => skill.GetType() == typeof(MagicArrowSpawner)).Disable();
                
                _skillInstances.Add(new MagicArrowSpawner(_skillData, phantomArrowsSkill));
                break;
            
            case ReactiveBootsSkill reactiveBootsSkill:
                _skillInstances.Add(new ReactiveBoots(_skillData, reactiveBootsSkill));
                break;
            
            case RecoveryPainSkill recoveryPainSkill:
                _skillInstances.Add(new PainHealler(_skillData, recoveryPainSkill));
                break;
            
            case ReducedResistanceSkill reducedResistanceSkill:
                _skillInstances.FirstOrDefault(skill => skill.GetType() == typeof(ChainZap)).Disable();
                
                _skillInstances.Add(new ChainZap(_skillData, reducedResistanceSkill));
                break;
            
            case SnowBloodSkill snowBloodSkill:
                _skillInstances.Add(new SnowBlood(_skillData, snowBloodSkill));
                break;
            
            case StormBladeSkill stormBladeSkill:
                var boomerang = _skillInstances.
                    FirstOrDefault(skillInstance => skillInstance.GetType() == typeof(Boomerang));
                
                if(boomerang != null)
                    _skillInstances.Add(new StormBlade(stormBladeSkill, (boomerang as Boomerang)?.Orbital));
                
                break;
            
            case StunZapSkill stunZapSkill:
                _skillInstances.Add(new StunZap(_skillData, stunZapSkill));
                break;
            
            case SummonSkill summonSkill:
                _skillInstances.Add(new SummonInstance(_skillData, summonSkill));
                break;
            
            case TacticalEfficiencySkill tacticalEfficiencySkill:
                _skillInstances.Add(new TacticalEfficiency(_skillData, tacticalEfficiencySkill));
                break;
            
            case ThunderSkill thunderSkill:
                _skillInstances.Add(new Thunder(_skillData, thunderSkill));
                break;
            
            case TirelessSkill tirelessSkill:
                _skillInstances.Add(new Tireless(_skillData, tirelessSkill));
                break;
            
        }
    }
}