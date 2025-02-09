using UnityEngine;
using UnityEngine.Serialization;

public abstract class Skill : ScriptableObject
{
    [field: SerializeField] public SkillInfo SkillInfo { get; protected set; }
    
    public abstract void Apply(SkillData skillData);
}
