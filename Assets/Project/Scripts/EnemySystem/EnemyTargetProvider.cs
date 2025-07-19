using Project.Scripts.PlayerSystem;
using UnityEngine;

namespace Project.Scripts.EnemySystem
{
    public class EnemyTargetProvider : MonoBehaviour
    {
        private float _attackDistance;

        public bool HasPlayer => Player != null;
        public bool IsPlayerInRange => Vector2.Distance(Position, Player.Position) < _attackDistance;
        private Vector2 Position => transform.position;
        
        public Player Player { get; private set; }

        public void Initialize(Player player, float attackDistance)
        {
            Player = player;   
            _attackDistance = attackDistance;
        }
    }
}