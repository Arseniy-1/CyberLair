using System.Collections;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses
{
    public abstract class BossTimedAttack : BossAttack
    {
        [field: SerializeField] public float Time { get; private set; }

        protected abstract override IEnumerator Attack();
    }
}