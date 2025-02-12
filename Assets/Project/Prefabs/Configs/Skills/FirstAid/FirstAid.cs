using System;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills
{
    [Serializable]
    public class FirstAid
    {
        [SerializeField, Range(0f, 1f)] private float _healProportion;
        
        private Health _health;
        
        public void Initialize(Health health)
        {
            _health = health;
            _health.DamageTaken += HealPart;
        }

        private void HealPart(float damage)
        {
            if (damage < 0)
                return;
            
            _health.Heal(damage * _healProportion);
        }
    }
}