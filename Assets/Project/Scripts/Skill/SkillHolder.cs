using System.Collections.Generic;
using Project.Prefabs.Configs.Skills.AffectedArea;
using Project.Prefabs.Configs.Skills.ArtayaShield;
using Project.Prefabs.Configs.Skills.Durability;
using UnityEngine;

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
                _skillInstances.Add(new ArtayaShield(_skillData, artayaShieldSkill, this));
                break;
            
            case 
        }
    }
}