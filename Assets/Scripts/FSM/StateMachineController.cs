using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Monotheist.Human;


namespace Monotheist.FSM
{   
    public class StateMachineController
    {
        private List<GoalState> _stateList;        
        private GoalState _currentState;

        public StateMachineController(HumanConfig humanConfig, HumanNeeds humanNeeds, Transform owner)
		{
            _stateList = new List<GoalState>();
            _stateList.Add(new WanderState(humanConfig, humanNeeds, owner));
            _stateList.Add(new ClaimState(humanConfig, humanNeeds));
            _stateList.Add(new RecollectState(humanConfig, humanNeeds));
            _stateList.Add(new EatState(humanConfig, humanNeeds));
            _stateList.Add(new SleepState(humanConfig, humanNeeds));

            ChangeState(typeof(WanderState));

            foreach(GoalState state in _stateList)
			{
                state.Subscribe(ChangeState);
			}
		}

        public void ChangeState(Type newStateType)
		{
            GoalState newState = null;

            foreach(GoalState state in _stateList)
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
