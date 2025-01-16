using System;
using UnityEngine;

public class AidKit: MonoBehaviour, IInteractable, IDestoyable<AidKit>
{
    [field: SerializeField] public int HealAmount { get; private set; }
 
    public event Action<AidKit> OnDestroyed;

    public void Interact()
    {
        Destroy(gameObject);
    }
}
