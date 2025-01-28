using Project.Scripts.Weapon.ActiveSkills.MagicArrow;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.LevelSystem.ActiveSkills
{
    [CreateAssetMenu(fileName = "New Magic Arrow Skill", menuName = "Skill/Active/MagicArrow", order = 51)]
    public class MagicArrowSkill : Skill
    {
        [SerializeField] private SkillConfig _speedConfig;
        [SerializeField] private SkillConfig _damageConfig;
        [SerializeField] private SkillConfig _radiusConfig;
        [SerializeField] private SkillConfig _reloadConfig;

        [SerializeField] private MagicArrow _defaultForm;
        [SerializeField] private MagicArrow _finalForm;
        [SerializeField] private float _spawnDelay;
        [SerializeField] private float _searchRadius;
        [SerializeField] private LayerMask _layerMask;

        [SerializeField] private MagicArrowSpawner _spawner;

        public override void Apply(SkillData skillData)
        {
            if (skillData.Level > MaxLevel || skillData.Level < 0)
                return;

            if (skillData.Level == 0)
                _spawner = new MagicArrowSpawner(_defaultForm, skillData.WeaponHolder.transform, _spawnDelay,
                    _searchRadius, _layerMask);

            if (skillData.Level == MaxLevel - 1)
                _spawner.ChangeArrowPrefab(_finalForm);

            var speed = _speedConfig.Multipliers[skillData.Level];
            var damage = (int)_damageConfig.Multipliers[skillData.Level];
            var radius = _radiusConfig.Multipliers[skillData.Level];
            var reloadTime = _reloadConfig.Multipliers[skillData.Level];
            
            _spawner.ApplyStats(speed, damage, radius, reloadTime);
        }
    }
}