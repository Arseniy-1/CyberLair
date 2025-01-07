using UnityEngine;

public abstract class Skill : ScriptableObject, ISkill
{
    [field: SerializeField] public SkillInfo SkillInfo { get; private set; }
}