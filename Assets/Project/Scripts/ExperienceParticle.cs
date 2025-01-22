using System;
using UnityEngine;

public class ExperienceParticle : MonoBehaviour, IInteractable, IDestoyable<ExperienceParticle>
{
    public event Action<ExperienceParticle> OnDestroyed;

    public void Interact()
    {
        // Destroy(gameObject);
    }
}
