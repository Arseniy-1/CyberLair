using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExperienceParticle : MonoBehaviour, IInteractable, IMoveable, IDestoyable<ExperienceParticle>
{
    public event Action<ExperienceParticle> OnDestroyed;

    [field: SerializeField] public int ExperienceAmount { get; private set; } = 10;
    public Rigidbody2D Rigidbody2D { get; private set; }

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Initialize(int experienceAmount)
    {
        if (experienceAmount <= 0)
            return;

        ExperienceAmount = experienceAmount;
    }

    public void Interact()
    {
        OnDestroyed?.Invoke(this);
    }
}