using UnityEngine;

namespace Project.Prefabs.Configs.Skills
{
    [CreateAssetMenu(fileName = "FirstAidSkill", menuName = "Skill/Simple/FirstAid", order = 51)]
    public class FirstAidSkill : Skill
    {
        [field: SerializeField, Range(0f, 1f)] public float HealProportion { get; private set; }
    }
}