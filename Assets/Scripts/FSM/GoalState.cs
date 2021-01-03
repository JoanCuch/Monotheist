using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Monotheist.Human;

namespace Monotheist.FSM {

	public abstract class GoalState : State
	{
		private Action<Type> _finished;

		protected List<ActionState> _actionList;
		protected ActionState _currentAction;
		protected int _currentActionIndex;

		protected HumanNeeds _humanNeeds;
		protected HumanConfig _humanConfig;

		public GoalState(HumanConfig humanConfig, HumanNeeds humanNeeds)
		{
			_actionList = new List<ActionState>();
			_currentActionIndex = 0;
			_humanNeeds = humanNeeds;
			_humanConfig = humanConfig;
		}

		public virtual void Enter()
		{

		}

		public virtual void Execute()
		{
			if(_currentAction == null)
			{
				Debug.LogWarning("Null current action");
				return;
			}
			_currentAction.Execute();
		}

		public virtual void Exit()
		{

		}

		public void Subscribe(Action<Type> action)
		{
			_finished += action;
		}

		/*public void NextAction()
		{		
			if(_currentAction!= null)
			{
				_currentAction.Exit();
			}

			_currentActionIndex++;
			if(_currentActionIndex < _actionList.Count)
			{
				_currentAction = _actionList[_currentActionIndex];

				if (_currentAction != null)
				{
					_currentAction.Enter();
				}
			}
			else
			{

			}		
		}*/

		protected void Finish(Type nextType)
		{
			_finished.Invoke(nextType);
		}


		protected void ChangeAction(ActionTags newActionTag)
		{
			ActionState newAction = null;

			foreach (ActionState action in _actionList)
			{
				if (action.Tag == newActionTag)
				{
					newAction = action;
					break;
				}
			}

			if (newAction == null)
			{
				Debug.LogWarning("Error 404, action not found");
				return;
			}
			else
			{
				Debug.Log("Change action: " + newAction);
			}


			if (_currentAction != null)
			{
				_currentAction.Exit();
			}

			_currentAction = newAction;

			if (_currentAction != null)
			{
				_currentAction.Enter();
			}
		}	
	}
}
