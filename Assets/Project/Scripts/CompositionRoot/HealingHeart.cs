using System;
using UnityEngine;

public class HealingHeart : MonoBehaviour,IInteractable, IDestoyable<HealingHeart>
{
    public event Action<HealingHeart> OnDestroyed;
    
    [field: SerializeField] public int HealAmount { get; private set; }
    
    public void Interact()
    {
        throw new NotImplementedException();
    }
}