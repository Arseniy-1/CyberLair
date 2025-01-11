using Project.Scripts.Weapon.ActiveSkills.MagicArrow;
using UnityEngine;

namespace Project.Scripts.LevelSystem.ActiveSkills
{
    [CreateAssetMenu(fileName = "New Magic Arrow Skill", menuName = "Skill/Active/MagicArrow", order = 51)]
    public class MagicArrowSkill : ActiveSkill
    {
        [SerializeField] private MagicArrowSpawner _arrow;
        [SerializeField] private SkillConfig _damageConfig;
        
        private MagicArrowSpawner _spawnerInstance;
        
        public override void Apply(WeaponHolder weaponHolder, int level)
        {
            _spawnerInstance = Instantiate(_arrow, weaponHolder.transform);
        }
    }
}