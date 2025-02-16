using Project.Scripts.Weapon.ActiveSkills.MagicArrow;
using UnityEngine;

namespace Project.Scripts.LevelSystem.ActiveSkills
{
    [CreateAssetMenu(fileName = "MagicArrowSkill", menuName = "Skill/Simple/MagicArrow", order = 51)]
    public class MagicArrowSkill : Skill
    {
        [SerializeField] private MagicArrow _magicArrowPrefab;
        
        [field:SerializeField] public MagicArrowSpawner MagicArrowSpawner { get; private set; }
        
        public override void Apply(SkillData skillData)
        {
            MagicArrowSpawner.Initialize(_magicArrowPrefab, skillData.WeaponHolder.transform);
        }
    }
}