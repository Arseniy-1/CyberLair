using Project.Scripts.Interfaces;
using Project.Scripts.Props;
using Project.Scripts.Services;
using Project.Scripts.Services.Enum;
using Project.Scripts.Services.Extensions;
using Project.Scripts.Stats;
using UnityEngine;

namespace Project.Scripts.PlayerSystem
{
    public class PlayerCollisionHandler : CollisionHandler
    {
        [SerializeField] private AudioID _experienceSound = AudioID.PlayerExperience;
    
        private Health _health;
        private ExperienceStorage _experienceStorage;

        public void Initialize(Health health, ExperienceStorage experienceStorage)
        {
            _health = health;
            _experienceStorage = experienceStorage;
        }

        protected override void HandleCollision(Collider2D collider)
        {
            if (collider.TryGetComponent(out ExperienceParticle experienceParticle))
            {
                _experienceStorage.AddExperience(experienceParticle.ExperienceAmount);
                _experienceSound.Play();
            }
            else if (collider.TryGetComponent(out HealingHeart sphere))
            {
                _health.Heal(sphere.HealAmount);
            }
        
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }
}