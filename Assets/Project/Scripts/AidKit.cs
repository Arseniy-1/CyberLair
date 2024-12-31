using System;
using UnityEngine;

public class AidKit: MonoBehaviour, IInteractable, IDestoyable<ExperienceParticle>
{
    public int HealAmount { get; private set; }
 
    public event Action<ExperienceParticle> OnDestroyed;

    public void Interact()
    {
        Destroy(gameObject);
    }
}
