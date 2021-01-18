using System.Collections.Generic;
using UnityEngine;
using Monotheist.Human;
using UnityEngine.Assertions;


namespace Monotheist.FSM
{   
    public class StateMachineController
    {
        private List<GoalState> _GoalPool;        
        private ReactiveProperty<GoalState> _currentGoal;

        private HumanNeeds _humanNeeds;
        private HumanConfig _humanConfig;
        private Transform _owner;

        public ReactiveProperty<GoalState> CurrentGoalProperty => _currentGoal;

        public StateMachineController(HumanConfig humanConfig, HumanNeeds humanNeeds, Transform owner)
		{
            _humanConfig = humanConfig;
            _humanNeeds = humanNeeds;
            _owner = owner;

            _currentGoal = new ReactiveProperty<GoalState>(NullGoal.Instance);

            _GoalPool = new List<GoalState>();
            _GoalPool.Add(new WanderGoal(humanConfig, humanNeeds, owner));
            _GoalPool.Add(new RecollectGoal(humanConfig, humanNeeds, owner));
            _GoalPool.Add(new SatisfyGoal(humanConfig, humanNeeds, owner));
            _GoalPool.Add(new StartGoal(humanConfig, humanNeeds));
            _GoalPool.Add(new DeathGoal(humanNeeds, humanConfig, owner));

            foreach(GoalState goal in _GoalPool)
			{
                goal.Subscribe(ChangeState);
			}

            ChangeState(GoalTags.start);
        }

        public void Update()
        {
            Need currentNeed = _humanNeeds.GetMostUrgentNeed();
            Assert.IsNotNull(currentNeed);

            if (currentNeed.CurrentState == NeedStates.lethal)
			{
                ChangeState(GoalTags.die);
			}

            if(_currentGoal.Value.Tag == GoalTags.wander)
			{
                SelectNextGoal(currentNeed);
			}

            if (_currentGoal.Value != NullGoal.Instance)
            {
                _currentGoal.Value.Execute();
            }
        }

        public void ChangeState(GoalTags newGoalTag)
		{                     
            GoalState newGoal = NullGoal.Instance;

            foreach(GoalState goal in _GoalPool)
			{
                if(goal.Tag == newGoalTag)
				{
                    newGoal = goal;
                    break;
				}
			}

            Assert.AreEqual(newGoal, NullGoal.Instance);
            _currentGoal.Value.Exit();
            _currentGoal.Value = newGoal;
            _currentGoal.Value.Enter();   
		}       
    
        public void SelectNextGoal(Need currentNeed)
		{
            if (currentNeed.CurrentState != NeedStates.satisfied &&
                currentNeed.CurrentState != NeedStates.fullfilled &&
                SatisfyGoal.ThereAreItems(_humanNeeds, currentNeed.Tag)
                )
            {               
                ChangeState(GoalTags.satisfy);
            }
            else if (
                currentNeed.CurrentItemListState != NeedItemStates.satisfied &&
                RecollectGoal.ThereAreUnclaimedItemsAround(_owner.position, _humanConfig.searchRange, currentNeed.Tag)
                )
            {
                ChangeState(GoalTags.recollect);
            }
        }   
    }   
}
