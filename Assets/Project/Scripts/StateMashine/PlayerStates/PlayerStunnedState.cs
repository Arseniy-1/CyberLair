using Project.Scripts.Interfaces;
using Project.Scripts.PlayerSystem;
using Project.Scripts.Services;
using UnityEngine;

namespace Project.Scripts.StateMashine.PlayerStates
{
    public class PlayerStunnedState : IState
    {
        private readonly PlayerMover _playerMover;
        private readonly Jumper _playerJumper;
        
        private Player _player;

        public PlayerStunnedState(PlayerMover playerMover, Jumper playerJumper)
        {
            _playerMover = playerMover;
            _playerJumper = playerJumper;
        }

        public void Enter()
        {
            _playerMover.enabled = false;
            _playerJumper.enabled = false;
        }

        public void Exit()
        {
            _playerMover.enabled = true;
            _playerJumper.enabled = true;
        }

        public void Initialize(IStateSwitcher stateSwitcher, Animator animator) { }

        public void Update() { }
    }
}
