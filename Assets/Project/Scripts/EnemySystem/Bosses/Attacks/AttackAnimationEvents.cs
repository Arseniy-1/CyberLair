using System;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses
{
    public class AttackAnimationEvents : MonoBehaviour
    {
        public event Action Attacking;

        public void InvokeAttackingEvent() => Attacking?.Invoke();

    }
}