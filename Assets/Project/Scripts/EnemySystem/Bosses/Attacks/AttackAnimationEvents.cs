using System;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses
{
    public class AttackAnimationEvents : MonoBehaviour
    {
        public event Action Attacking;
        
        public event Action Ending;

        public void InvokeAttackingEvent() => Attacking?.Invoke();

        public void InvokeEndingEvent() => Ending?.Invoke();
    }
}