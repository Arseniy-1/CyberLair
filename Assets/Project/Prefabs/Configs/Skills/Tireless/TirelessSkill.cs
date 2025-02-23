using UnityEngine;

[CreateAssetMenu(fileName = "TirelessSkill", menuName = "Skill/Hard/Tireless", order = 51)]
public class TirelessSkill : HardSkill
{
    [SerializeField] private StatModifier _jumpReloadTimeModifier;
}

public class Tireless : SkillInstance
{
    public Tireless(SkillHolder skillHolder) : base(skillHolder)
    {
        
    }
}