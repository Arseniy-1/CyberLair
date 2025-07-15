using Project.Scripts.Skill;
using Project.Scripts.Stats;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.ReactiveBoots
{
    [CreateAssetMenu(fileName = "ReactiveBootsSkill", menuName = "Skill/Simple/ReactiveBoots", order = 51)]
    public class ReactiveBootsSkill : Skill
    {
        [field: SerializeField] public StatModifier SpeedModifier { get; private set; }
    }
}