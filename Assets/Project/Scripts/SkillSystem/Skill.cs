using UnityEngine;

namespace Project.Scripts.SkillSystem
{
    public abstract class Skill : ScriptableObject
    {
        [field: SerializeField] public SkillInfo SkillInfo { get; protected set; }
    }
}
