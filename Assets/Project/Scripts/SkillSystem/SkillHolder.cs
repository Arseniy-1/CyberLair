using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Project.Scripts.Interfaces;
using Project.Scripts.SkillSystem.SkillInstances;
using Project.Scripts.SkillSystem.SkillSOClasses;
using Project.Scripts.SkillSystem.SkillViews;
using Project.Scripts.Spawners.MagicArrows;
using Project.Scripts.Spawners.StreamingEnergies;

namespace Project.Scripts.SkillSystem
{
    public class SkillHolder
    {
        private readonly SkillData _skillData;
        private readonly List<ISkillInstance> _skillInstances = new ();
        private readonly CancellationToken _token;

        public SkillHolder(SkillData skillData, CancellationToken token)
        {
            _skillData = skillData;
            _token = token;
        }

        public void CreateSkill(Skill skill)
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
                    _skillInstances.Add(new ArtayaShield(_skillData, artayaShieldSkill, _token));
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
                    _skillInstances.FirstOrDefault(thunder => thunder.GetType() == typeof(Thunder))?.Disable();

                    _skillInstances.Add(new Thunder(_skillData, overloadSkill));
                    break;

                case PhantomArrowsSkill phantomArrowsSkill:
                    _skillInstances.FirstOrDefault(magicArrow => magicArrow.GetType() == typeof(MagicArrowSpawner))?.Disable();

                    _skillInstances.Add(new MagicArrowSpawner(_skillData, phantomArrowsSkill));
                    break;

                case ReactiveBootsSkill reactiveBootsSkill:
                    _skillInstances.Add(new ReactiveBoots(_skillData, reactiveBootsSkill));
                    break;

                case RecoveryPainSkill recoveryPainSkill:
                    _skillInstances.Add(new PainHealer(_skillData, recoveryPainSkill));
                    break;

                case ReducedResistanceSkill reducedResistanceSkill:
                    _skillInstances.FirstOrDefault(chainZap => chainZap.GetType() == typeof(ChainZap))?.Disable();

                    _skillInstances.Add(new ChainZap(_skillData, reducedResistanceSkill));
                    break;

                case SnowBloodSkill snowBloodSkill:
                    _skillInstances.Add(new SnowBlood(_skillData, snowBloodSkill));
                    break;

                case StormBladeSkill stormBladeSkill:
                    var boomerang =
                        _skillInstances.FirstOrDefault(boomerang => boomerang.GetType() == typeof(Boomerang));

                    if (boomerang != null)
                        _skillInstances.Add(new StormBlade(stormBladeSkill, (boomerang as Boomerang)?.Orbital, _token));

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

                case NonStopSkill nonStopSkill:
                    _skillInstances.Add(new NonStop(_skillData, nonStopSkill, _token));
                    break;

                case ArtayaWillSkill artayaWillSkill:
                    _skillInstances.Add(new ArtayaWill(_skillData, artayaWillSkill));
                    break;

                case AllOnLineSkill allOnLineSkill:
                    _skillInstances.Add(new AllOnLine(_skillData, allOnLineSkill));
                    break;

                case ThunderStormSkill thunderStormSkill:
                    var thunder =
                        _skillInstances.FirstOrDefault(skillInstance => skillInstance.GetType() == typeof(Thunder));

                    _skillInstances.Add(new ThunderStorm(thunderStormSkill, thunder as Thunder));
                    break;

                case StreamingEnergySkill streamingEnergySkill:
                    _skillInstances.Add(new StreamingEnergySpawner(streamingEnergySkill, _token));
                    break;

                case TeleportationJumpSkill teleportationJumpSkill:
                    _skillInstances.Add(new TeleportationJump(_skillData, teleportationJumpSkill));
                    break;

                case MercuryBlessSkill mercuryBlessSkill:
                    _skillInstances.Add(new MercuryBless(_skillData, mercuryBlessSkill));
                    break;

                case HellCatsSkill hellCatsSkill:
                    var fireZoneInstance =
                        _skillInstances.FirstOrDefault(skillInstance => skillInstance.GetType() == typeof(FireZoneManager));

                    if (fireZoneInstance != null)
                        _skillInstances.Add(new HellCats(hellCatsSkill, fireZoneInstance as FireZoneManager));
                
                    break;
            }
        }

        public void Disable()
        {
            _skillInstances.ForEach(skill => skill.Disable());
        }
    }
}