using UnityEngine;

public class PlayerConfig : MonoBehaviour
{
    [field: SerializeField] public float JumpDistance { get; private set; }
    [field: SerializeField] public float JumpTime { get; private set; }

    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public float ReloadTime { get; private set; }
    [field: SerializeField] public Bullet BulletPrefab { get; private set; }
}