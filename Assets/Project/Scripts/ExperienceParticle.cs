using System;
using UnityEngine;

public class ExperienceParticle : MonoBehaviour, IInteractable, IDestoyable<ExperienceParticle>
{
    public event Action<ExperienceParticle> OnDestroyed;

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.TryGetComponent(out IInteractable interactable))
    //     {
    //         HandleCollision(interactable);
    //     }
    // }
    //
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.collider.TryGetComponent(out IInteractable interactable))
    //     {
    //         HandleCollision(interactable);
    //     }
    // }

    public void Interact()
    {
        Destroy(gameObject);
    }
}

public class AidKit
{

}
