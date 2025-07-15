using UnityEngine;

namespace Project.Scripts.Interfaces
{
    public interface IState
    {
        public void Enter();

        public void Update();

        public void Exit();

        public void Initialize(IStateSwitcher stateSwitcher, Animator animator);
    }
}
