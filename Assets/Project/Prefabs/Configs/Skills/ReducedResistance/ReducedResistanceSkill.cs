using System;
using System.Linq;
using Project.Prefabs.Configs.Skills.Zap;
using UnityEngine;

[CreateAssetMenu(fileName = "ReducedResistanceSkill", menuName = "Skill/Hard/ReducedResistance", order = 51)]
public class ReducedResistanceSkill : HardSkill
{
    [SerializeField] private ChainZap _chainZap;

    private ChainZapSkill _chainZapSkill;
    private ChainZap PastZap => _chainZapSkill.ChainZap;

    private void OnValidate()
    {
        _chainZapSkill =
            NeededSkills.FirstOrDefault(skill => skill.GetType() == typeof(ChainZapSkill)) as ChainZapSkill;

        if (_chainZapSkill == false)
            throw new NullReferenceException("ChainZapSkill is not set");
    }

    public override void Apply(SkillData skillData)
    {
        PastZap.Disable();

        _chainZap.Initialize(skillData.WeaponHolder.Weapon);
    }
}