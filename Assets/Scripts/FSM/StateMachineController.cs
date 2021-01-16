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
            _stateList.Add(new WanderGoal(humanConfig, humanNeeds, owner));
            _stateList.Add(new RecollectGoal(humanConfig, humanNeeds, owner));
            _stateList.Add(new SatisfyGoal(humanConfig, humanNeeds, owner));
            _stateList.Add(new StartGoal(humanConfig, humanNeeds));
            _stateList.Add(new DeathGoal(humanNeeds, humanConfig, owner));

            foreach(GoalState state in _stateList)
			{
                state.Subscribe(ChangeState);
			}

            _humanConfig = humanConfig;
            _humanNeeds = humanNeeds;
            _owner = owner;

            ChangeState(GoalTags.start);
        }

        public void Update()
        {
            Need currentNeed = _humanNeeds.GetMostUrgentNeed();

            if (currentNeed.CurrentState == NeedStates.lethal)
			{
                ChangeState(GoalTags.die);
			}

            if(_currentState.Value.GetType() == typeof(WanderGoal))
			{
                SelectNextGoal(currentNeed);
			}

            if (_currentState != null && _currentState.Value != null)
            {
                _currentState.Value.Execute();
            }
        }

        public void ChangeState(GoalTags newGoalTag)
		{                     
            GoalState newGoal = null;

            foreach(GoalState goal in _stateList)
			{
                if(goal.Tag == newGoalTag)
				{
                    newGoal = goal;
                    break;
				}
			}

            if (newGoal == null)
            {
                Debug.LogWarning("Error 404, state not found");               
            }
            else
            {
                if (_currentState.Value != null)
                {
                    _currentState.Value.Exit();
                }

                _currentState.Value = newGoal;

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
                SatisfyGoal.ThereAreItems(_humanNeeds, currentNeed.Tag)
                )
            {               
                ChangeState(GoalTags.satisfy);
            }
            else if (
                currentNeed.CurrentItemListState != NeedItemStates.satisfied &&
                RecollectGoal.SearchUnclaimedItemsAround(_owner.position, _humanConfig.searchRange, currentNeed.Tag)
                )
            {
                ChangeState(GoalTags.recollect);
            }
        }   
    }   
}
