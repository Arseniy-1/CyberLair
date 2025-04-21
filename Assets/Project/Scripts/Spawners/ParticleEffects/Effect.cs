using System;
using UnityEngine;

public class Effect : MonoBehaviour, IDestoyable<Effect>
{
    [SerializeField] private ParticleSystem _particle;
    
    public event Action<Effect> OnDestroyed;

    private void OnEnable()
    {
        _particle.Play();
    }
}