using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillSOClasses
{
    [CreateAssetMenu(fileName = "FirstAidSkill", menuName = "Skill/Simple/FirstAid", order = 51)]
    public class FirstAidSkill : Skill
    {
        [field: SerializeField, Range(0f, 1f)] public float HealProportion { get; private set; }
    }
}