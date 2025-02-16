using System;
using System.Linq;
using Project.Scripts.LevelSystem.ActiveSkills;
using Project.Scripts.Weapon.ActiveSkills.MagicArrow;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.PhantomArrows
{
    [CreateAssetMenu(fileName = "PhantomArrowsSkill", menuName = "Skill/Hard/PhantomArrows", order = 51)]
    public class PhantomArrowsSkill : HardSkill
    {
        [SerializeField] private MagicArrowSpawner _phantomArrowSpawner;
        [SerializeField] private MagicArrow _phantomArrowPrefab;
        
        private MagicArrowSkill _magicArrowSkill;
        private MagicArrowSpawner PastSpawner => _magicArrowSkill.MagicArrowSpawner;
        
        private void OnValidate()
        {
            _magicArrowSkill = NeededSkills.FirstOrDefault(skill => skill.GetType() == typeof(MagicArrowSkill)) as MagicArrowSkill;
            
            if(_magicArrowSkill == false)
                throw new NullReferenceException("MagicArrowSkill is not set");
        }
        
        public override void Apply(SkillData skillData)
        {
            PastSpawner.Disable();
            
            _phantomArrowSpawner.Initialize(_phantomArrowPrefab, skillData.WeaponHolder.transform);
        }
    }
}