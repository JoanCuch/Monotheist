using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Monotheist.FSM
{
    public abstract class State
    {
        private Action<State> _changeStateEvent;
        protected StateMachineModel _model;

        public State (StateMachineModel model)
		{
            _model = model;
        }

        public abstract void Enter();
        public abstract void Execute();
        public abstract void Exit();

        public void Subscribe(Action<State> action)
        {
            _changeStateEvent += action;
        }
        protected void ChangeState(State newState)
        {
            _changeStateEvent.Invoke(newState);
        }

    }
}
