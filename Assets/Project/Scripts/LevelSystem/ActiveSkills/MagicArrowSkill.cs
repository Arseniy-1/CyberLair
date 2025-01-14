using Project.Scripts.Weapon.ActiveSkills.MagicArrow;
using UnityEngine;

namespace Project.Scripts.LevelSystem.ActiveSkills
{
    [CreateAssetMenu(fileName = "New Magic Arrow Skill", menuName = "Skill/Active/MagicArrow", order = 51)]
    public class MagicArrowSkill : ActiveSkill
    {
        [SerializeField] private MagicArrowSpawner _arrow;
        [SerializeField] private SkillConfig _damageConfig;
        
        [SerializeField] private MagicArrow _finalForm;
        
        private MagicArrowSpawner _spawnerInstance;
        
        public override void Apply(WeaponHolder weaponHolder, int level)
        {
            if (!_spawnerInstance)
                _spawnerInstance = Instantiate(_arrow, weaponHolder.transform);
            
            if(level == MaxLevel)
                _spawnerInstance.ChangeArrowPrefab(_finalForm);
            
            
        }
    }
}