using Project.Scripts.Weapon.ActiveSkills.MagicArrow;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.LevelSystem.ActiveSkills
{
    [CreateAssetMenu(fileName = "New Magic Arrow Skill", menuName = "Skill/Active/MagicArrow", order = 51)]
    public class MagicArrowSkill : ActiveSkill
    {
        [SerializeField] private MagicArrowSpawner _arrowSpawner;
        [SerializeField] private SkillConfig _speedConfig;
        [SerializeField] private SkillConfig _damageConfig;
        
        [SerializeField] private MagicArrow _finalForm;
        
        private MagicArrowSpawner _spawner;
        
        public override void Apply(WeaponHolder weaponHolder, int level)
        {
            if (level > MaxLevel || level < 1)
                return;
            
            if(!_spawner)
                _spawner = Instantiate(_arrowSpawner, weaponHolder.transform);
            
            if(level == MaxLevel)
                _spawner.ChangeArrowPrefab(_finalForm);

            var speed = _speedConfig.Multipliers[level - 1];
            var damage = (int)_damageConfig.Multipliers[level - 1];
                
            _spawner.ApplyStats(speed, damage);
        }
    }
}