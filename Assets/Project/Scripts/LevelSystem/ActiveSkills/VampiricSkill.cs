using Project.Scripts.Weapon.ActiveSkills;
using UnityEngine;

namespace Project.Scripts.LevelSystem.ActiveSkills
{
    [CreateAssetMenu(fileName = "New Vampirism Skill", menuName = "Skill/Active/Vampirism", order = 51)]
    public class VampiricSkill : ActiveSkill
    {
        [SerializeField] private HealthSphereSpawner _spawnerPrefab;
        [SerializeField] private SkillConfig _skillConfig;
        
        private HealthSphereSpawner _spawner;
        
        public override void Apply(WeaponHolder weaponHolder, int level)
        {
            if (!_spawner)
            {
                _spawner = Instantiate(_spawnerPrefab);
                _spawner.Initialize();
            }
            
            _spawner.ApplyMultiplier(_skillConfig.Multipliers[level - 1]);
        }
    }
}