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

        private HumanNeeds _humanNeeds;
        private HumanConfig _humanConfig;
        private Transform _owner;

        public StateMachineController(HumanConfig humanConfig, HumanNeeds humanNeeds, Transform owner)
		{
            _stateList = new List<GoalState>();
            _stateList.Add(new WanderState(humanConfig, humanNeeds, owner));
            _stateList.Add(new ClaimState(humanConfig, humanNeeds, owner));
            _stateList.Add(new RecollectState(humanConfig, humanNeeds, owner));
            _stateList.Add(new SatisfyState(humanConfig, humanNeeds, owner));
            _stateList.Add(new StartState(humanConfig, humanNeeds));
            _stateList.Add(new DeathState(humanNeeds, humanConfig, owner));

            foreach(GoalState state in _stateList)
			{
                state.Subscribe(ChangeState);
			}

            _humanConfig = humanConfig;
            _humanNeeds = humanNeeds;
            _owner = owner;

            ChangeState(typeof(StartState));
        }

        public void Update()
        {
            Need currentNeed = _humanNeeds.GetUrgentNeed();
            
            /*if(currentNeed.CurrentState == NeedStates.lethal)
			{
                ChangeState(typeof(DeathState));
			}
            else if(currentNeed.CurrentState != NeedStates.satisfied)
			{
                ChangeState(typeof(SatisfyState));
			}
            else if(currentNeed.CurrentItemListState != NeedItemStates.satisfied)
			{
                ChangeState(typeof(RecollectState));
			}
			else if(_currentState.GetType() != typeof(WanderState))
			{
                ChangeState(typeof(WanderState));
			}*/
                                 
            if (_currentState != null) _currentState.Execute();
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
            }
            else
            {
                Debug.Log("Change state: " + newState);

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
		}       
    }   
}
