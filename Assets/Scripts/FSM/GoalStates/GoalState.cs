using System.Collections.Generic;
using UnityEngine;
using System;
using Monotheist.Human;
using UnityEngine.Assertions;

namespace Monotheist.FSM {

	public abstract class GoalState : State
	{
		public readonly GoalTags Tag;

		protected HumanNeeds _humanNeeds;
		protected HumanConfig _humanConfig;

		private Action<GoalTags> _finishedGoal;
		private Action<ActionState> _changedActionState;

		protected List<ActionState> _actionsList;
		protected ActionState _currentAction;

		

		public GoalState(HumanConfig humanConfig, HumanNeeds humanNeeds, GoalTags tag)
		{		
			_humanNeeds = humanNeeds;
			_humanConfig = humanConfig;
			Tag = tag;

			_actionsList = new List<ActionState>();
			_currentAction = NullAction.Instance;
		}

		public virtual void Enter() { }

		public virtual void Execute()
		{
			if (_currentAction == NullAction.Instance)
				return;
			
			_currentAction.Execute();
		}

		public virtual void Exit() { }

		public void Subscribe(Action<GoalTags> action)
		{
			_finishedGoal += action;
		}

		protected void Finish(GoalTags nextGoalTag)
		{
			_finishedGoal.Invoke(nextGoalTag);
		}

		protected void ChangeAction(ActionTags newActionTag)
		{
			ActionState newAction = NullAction.Instance;

			foreach (ActionState action in _actionsList)
			{
				if (action.Tag == newActionTag)
				{
					newAction = action;
					break;
				}
			}

			Assert.AreNotEqual(newAction, NullAction.Instance);
			Debug.Log(newAction);
			_currentAction.Exit();
			_currentAction = newAction;		
	      	_currentAction.Enter();
			_changedActionState?.Invoke(newAction);	
		}	
	
		public void SubscribeActionChange(Action<ActionState> action)
		{
			_changedActionState += action;
		}

		public void UnsubscribeActionChange(Action<ActionState> action)
		{
			_changedActionState -= action;
		}
	}
}
