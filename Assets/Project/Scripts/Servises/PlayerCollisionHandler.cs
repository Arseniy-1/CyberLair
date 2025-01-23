using Project.Scripts.Weapon.ActiveSkills.Vampirism;
using UnityEngine;

public class PlayerCollisionHandler : CollisionHandler
{
    private Health _health;
    private ExperienceStorage _experienceStorage;

    public void Initialize(Health health, ExperienceStorage experienceStorage)
    {
        _health = health;
        _experienceStorage = experienceStorage;
    }

    protected override void HandleCollision(Collider2D collider)
    {
        if (collider.TryGetComponent(out AidKit aidKit))
        {
            _health.Heal(aidKit.HealAmount);
        }
        else if (collider.TryGetComponent(out ExperienceParticle experienceParticle))
        {
            Debug.Log("1111");
            _experienceStorage.AddExperience(experienceParticle.ExperienceAmount);
        }
        else if (collider.TryGetComponent(out HealthSphere sphere))
        {
            _health.Heal(sphere.CurrentHealth);
        }
        
        if (collider.TryGetComponent(out IInteractable IInteractable))
        {
            IInteractable.Interact();
        }
    }
}