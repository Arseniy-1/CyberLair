using Project.Scripts.MessageBroker;
using Project.Scripts.PlayerSystem;
using UnityEngine;

public class BossChest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            MessageBrokerHolder.Chest.Publish(new M_ChestRaised());
        }
    }
}