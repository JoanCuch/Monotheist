using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Monotheist.FSM
{   
    public class StateMachineController
    {
        private List<State> _stateList;        
        private State _currentState;

        public StateMachineController()
		{
            _stateList.Add(new WanderState());
            _stateList.Add(new ClaimState());
            _stateList.Add(new RecollectState());
            _stateList.Add(new EatState());
            _stateList.Add(new SleepState());

            ChangeState(typeof(WanderState));

            foreach(State state in _stateList)
			{
                state.Subscribe(ChangeState);
			}
		}

        public void ChangeState(Type newStateType)
		{
            State newState = null;

            foreach(State state in _stateList)
			{
                if(state.GetType() == newStateType)
				{
                    newState = state;
                    break;
				}
			}

            if (newState == null)
            {
                Debug.LogWarning("Error 404, state not found");
                return;
            }
            else
            {
                Debug.Log("Change state: " + newState);
            }

            if (_currentState != null)
			{
                _currentState.Exit();
			}

            _currentState = newState;

            if (_currentState != null)
            {
                _currentState.Enter();
            }
		}

        public void Update()
		{
            if (_currentState != null) _currentState.Execute();
		}
    }
}
