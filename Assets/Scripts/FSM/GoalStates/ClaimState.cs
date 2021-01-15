using Monotheist.Human;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Monotheist.FSM
{

	public class ClaimState : GoalState
	{
		Transform _owner;
		Interactable _currentTarget;
		Need _currentNeed;

		public ClaimState(HumanConfig humanConfig, HumanNeeds humanNeeds, Transform owner) : base(humanConfig, humanNeeds)
		{
			_owner = owner;

			_actionList.Add(new WalkAction(_humanConfig, owner));
			_actionList.Add(new ClaimAction(_humanNeeds));

			foreach(ActionState action in _actionList)
			{
				action.Subscribe(FinishedAction);
			}

		}

		public override void Enter()
		{
			Assert.IsTrue(true, "Claim state invoked. That can never happen");
			base.Enter();

			_currentNeed = _humanNeeds.GetMostUrgentNeed();

			if (_currentNeed == null)
			{
				Finish(typeof(WanderState));
			}
			else if (_currentNeed.CurrentItemListState == NeedItemStates.satisfied)
			{
				Finish(typeof(WanderState));
			}
			else
			{
				_currentTarget = Utils.SearchInteractable(_owner.position, _humanConfig.searchRange, _currentNeed.Tag);

				if (_currentTarget == null)
				{
					//There are no objectes, go search them
					Finish(typeof(WanderState));
				}
				else
				{
					ChangeAction(ActionTags.walk);
					((WalkAction)_currentAction).SetTarget(_currentTarget.transform.position);
				}
			}

		}

		public override void Execute()
		{
			base.Execute();



		}

		public override void Exit()
		{
			base.Exit();

			_currentAction = null;
			_currentNeed = null;
			_currentTarget = null;
		}

		private void FinishedAction(bool completed)
		{
			switch (_currentAction.Tag)
			{
				case ActionTags.claim:				
					Finish(typeof(WanderState));
					break;

				case ActionTags.walk:
					if (completed)
					{
						ChangeAction(ActionTags.claim);
						((ClaimAction)_currentAction).SetTarget(_currentTarget);
					}
					else
					{
						Finish(typeof(WanderState));
					}
					break;
			}
		}
	}
}
