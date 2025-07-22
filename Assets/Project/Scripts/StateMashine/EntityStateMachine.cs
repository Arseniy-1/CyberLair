using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Interfaces;

namespace Project.Scripts.StateMashine
{
    public class EntityStateMachine : IStateSwitcher
    {
        private readonly List<IState> _states;
        private IState _currentState;

        public EntityStateMachine(List<IState> states)
        {
            _states = states;
        }

        public void Initialize()
        {
            _currentState = _states[0];
            _currentState.Enter();
        }

        public void SwitchState<T>() 
            where T : IState
        {
            IState state = _states.FirstOrDefault(state => state is T);

            if (state == null)
                throw new ArgumentNullException(nameof(T));

            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void Update() => _currentState.Update();
    }
}