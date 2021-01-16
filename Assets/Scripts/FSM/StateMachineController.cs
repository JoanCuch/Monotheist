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
        private ReactiveProperty<GoalState> _currentState;

        private HumanNeeds _humanNeeds;
        private HumanConfig _humanConfig;
        private Transform _owner;

        public ReactiveProperty<GoalState> CurrentStateProperty => _currentState;

        public StateMachineController(HumanConfig humanConfig, HumanNeeds humanNeeds, Transform owner)
		{
            _currentState = new ReactiveProperty<GoalState>();

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
            Need currentNeed = _humanNeeds.GetMostUrgentNeed();

            if (currentNeed.CurrentState == NeedStates.lethal)
			{
                ChangeState(typeof(DeathState));
			}

            if(_currentState.Value.GetType() == typeof(WanderState))
			{
                SelectNextGoal(currentNeed);
			}

            if (_currentState != null && _currentState.Value != null)
            {
                _currentState.Value.Execute();
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
            }
            else
            {
                if (_currentState.Value != null)
                {
                    _currentState.Value.Exit();
                }

                _currentState.Value = newState;

                if (_currentState.Value != null)
                {
                    _currentState.Value.Enter();
                }
            }           
		}       
    
        public void SelectNextGoal(Need currentNeed)
		{
            //If the human is wandering, search something to do.
            //TODO This should be thorugh event send by human needs.

            if (currentNeed.CurrentState != NeedStates.satisfied &&
                currentNeed.CurrentState != NeedStates.fullfilled &&
                SatisfyState.ThereAreItems(_humanNeeds, currentNeed.Tag)
                )
            {               
                ChangeState(typeof(SatisfyState));
            }
            else if (
                currentNeed.CurrentItemListState != NeedItemStates.satisfied &&
                RecollectState.SearchUnclaimedItemsAround(_owner.position, _humanConfig.searchRange, currentNeed.Tag)
                )
            {
                ChangeState(typeof(RecollectState));
            }
        }   
    }   
}
