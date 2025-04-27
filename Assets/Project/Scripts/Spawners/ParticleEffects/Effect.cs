using System;
using System.Threading.Tasks;
using UnityEngine;

public class Effect : MonoBehaviour, IDestoyable<Effect>
{
    [SerializeField] private ParticleSystem _particle;
    
    public event Action<Effect> OnDestroyed;

    private void OnEnable()
    {
        _particle.Play();
        WaitForParticleAsync();
    }
    
    private async void WaitForParticleAsync()
    {
        while (_particle != null && _particle.IsAlive(true))
        {
            await Task.Delay(100);
        }

        OnDestroyed?.Invoke(this);
    }
}