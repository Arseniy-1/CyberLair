using Project.Scripts.Weapon.ActiveSkills.MagicArrow;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.LevelSystem.ActiveSkills
{
    [CreateAssetMenu(fileName = "New Magic Arrow Skill", menuName = "Skill/Active/MagicArrow", order = 51)]
    public class MagicArrowSkill : Skill
    {
        [SerializeField] private MagicArrowSpawner _arrowSpawner;
        [SerializeField] private SkillConfig _speedConfig;
        [SerializeField] private SkillConfig _damageConfig;
        
        [SerializeField] private MagicArrow _finalForm;
        
        private MagicArrowSpawner _spawner;
        
        public override void Apply(SkillData skillData)
        {
            if (skillData.Level > MaxLevel || skillData.Level < 1)
                return;
            
            if(!_spawner)
                _spawner = Instantiate(_arrowSpawner, skillData.WeaponHolder.transform);
            
            if(skillData.Level == MaxLevel)
                _spawner.ChangeArrowPrefab(_finalForm);

            var speed = _speedConfig.Multipliers[skillData.Level - 1];
            var damage = (int)_damageConfig.Multipliers[skillData.Level - 1];
                
            _spawner.ApplyStats(speed, damage);
        }
    }
}