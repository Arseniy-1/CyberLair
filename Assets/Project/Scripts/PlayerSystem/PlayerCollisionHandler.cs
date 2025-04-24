using UnityEngine;

public class PlayerCollisionHandler : CollisionHandler
{
    [SerializeField] private SoundPlayer _experienceSoundPlayer;
    
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
            _experienceSoundPlayer.Play();
        }
        else if (collider.TryGetComponent(out HealingHeart sphere))
        {
            _health.Heal(sphere.HealAmount);
        }
        
        if (collider.TryGetComponent(out IInteractable IInteractable))
        {
            IInteractable.Interact();
        }
    }
}