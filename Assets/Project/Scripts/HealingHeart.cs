using System;
using UnityEngine;

public class HealingHeart : MonoBehaviour, IMoveable, IInteractable, IDestoyable<HealingHeart>
{
    public event Action<HealingHeart> OnDestroyed;

    [field: SerializeField] public int HealAmount { get; private set; }
    public Rigidbody2D Rigidbody2D { get; private set; }
    
    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Initialize(int amount)
    {
        if(amount <= 0)
            return; 
        
        HealAmount = amount;
    }
    
    public void Interact()
    {
        Destroy(gameObject);
    }
}