using UnityEngine;

namespace Project.Scripts.Skill
{
    public abstract class Skill : ScriptableObject
    {
        [field: SerializeField] public SkillInfo SkillInfo { get; protected set; }
    }
}
