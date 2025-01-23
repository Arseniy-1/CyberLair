using UnityEngine;

namespace Project.Scripts.EnemySystem
{
    public class EnemyTargetProvider : MonoBehaviour
    {
        private float _attackDistance;
        [SerializeField] private Player _player;
        
        public Vector2 Position => transform.position;
        public bool HasPlayer => _player != null;
        public Player Player => _player;
        public bool IsPlayerInRange => Vector2.Distance(Position, _player.Position) < _attackDistance;

        public void Initialize(Player player, float attackDistance)
        {
            _player = player;   
            _attackDistance = attackDistance;
        }
    }
}