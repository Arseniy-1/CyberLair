using System;
using UnityEngine;

namespace Project.Scripts.Weapon.ActiveSkills.Vampirism
{
    public class HealthSphere : MonoBehaviour, IDestoyable<HealthSphere>, IInteractable
    {
        public int CurrentHealth {get; private set; }

        public event Action<HealthSphere> OnDestroyed;

        public void ApplyStats(int health)
        {
            CurrentHealth = health;
        }

        public void Interact()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}