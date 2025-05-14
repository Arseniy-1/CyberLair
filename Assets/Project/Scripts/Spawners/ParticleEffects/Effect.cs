using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Effect : MonoBehaviour, IDestoyable<Effect>
{
    [SerializeField] private List<ParticleSystem> _particles;

    public event Action<Effect> OnDestroyed;

    private void OnEnable()
    {
        foreach (var particle in _particles)
        {
            particle.Play();
            WaitForParticleAsync(particle);
        }
    }

    private async void WaitForParticleAsync(ParticleSystem particle)
    {
        while (isActiveAndEnabled == false && particle.IsAlive(true))
        {
            await Task.Yield();
        }

        OnDestroyed?.Invoke(this);
    }
}