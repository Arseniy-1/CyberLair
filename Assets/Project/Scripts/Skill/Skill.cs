using UnityEngine;

public abstract class Skill : ScriptableObject
{
    [field: SerializeField] public SkillInfo SkillInfo { get; private set; }
    [field: SerializeField] public int MaxLevel { get; private set; } = 5;
    
    public abstract void Apply(SkillData skillData);
}