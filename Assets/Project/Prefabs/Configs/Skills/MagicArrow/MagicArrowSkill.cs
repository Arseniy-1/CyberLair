using Project.Scripts.Weapon.ActiveSkills.MagicArrow;
using UnityEngine;

namespace Project.Scripts.LevelSystem.ActiveSkills
{
    [CreateAssetMenu(fileName = "MagicArrowSkill", menuName = "Skill/Simple/MagicArrow", order = 51)]
    public class MagicArrowSkill : Skill
    {
        [SerializeField] private MagicArrowSpawner _magicArrowSpawner;
        [SerializeField] private MagicArrow _magicArrowPrefab;
        
        public override void Apply(SkillData skillData)
        {
            _magicArrowSpawner.Initialize(_magicArrowPrefab, skillData.WeaponHolder.transform);
        }
    }
}