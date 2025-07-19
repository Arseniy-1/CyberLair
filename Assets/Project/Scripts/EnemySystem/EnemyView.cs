using System;
using Project.Scripts.PlayerSystem.TakeDamageEffect;
using UnityEngine;

namespace Project.Scripts.EnemySystem
{
    [Serializable]
    public class EnemyView : EntityDamageView
    {
        [SerializeField] private ParticleSystem _takeDamageParticles;
        
        [field: SerializeField] public Animator Animator { get; private set; }

        public override void StartBlink()
        {
            base.StartBlink();
            
            _takeDamageParticles.Play();
        }
    }
}