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

        public abstract void Enter();
        public virtual void Execute()
        {
            if(_model.owner.GetCurrentNecessity() == HumanConfig.Necessities.dead)
			{
                ChangeState(_model.deathState);
			}
        }
        public abstract void Exit();

        public void Subscribe(Action<State> action)
        {
            _changeStateEvent += action;
        }
        protected void ChangeState(State newState)
        {
            if (newState == null)
            {
                Debug.LogWarning("Null newState");
                return;
            }

            _changeStateEvent.Invoke(newState);
        }

        public void SetModel(StateMachineModel model)
		{
            _model = model;
		}

    }
}
