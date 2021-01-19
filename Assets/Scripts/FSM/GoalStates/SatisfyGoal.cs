using Monotheist.Human;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Monotheist.FSM
{
	public class SatisfyGoal : GoalState
	{
		Transform _owner;
		Interactable _currentTarget;
		Need _currentNeed;

		public SatisfyGoal(HumanConfig humanConfig, HumanNeeds humanNeeds, Transform owner) : base(humanConfig, humanNeeds, GoalTags.satisfy)
		{
			_owner = owner;

			_actionsList.Add(new WalkAction(_humanConfig, owner));
			_actionsList.Add(new InteractAction(_humanNeeds));

			foreach (ActionState action in _actionsList)
			{
				action.Subscribe(FinishedAction);
			}

			_currentTarget = NullInteractable.Instance;
			_currentNeed = NullNeed.Instance;
		}

		public override void Enter()
		{
			base.Enter();

			_currentNeed = _humanNeeds.GetMostUrgentNeed();

			Assert.IsNotNull(_currentNeed);
			Assert.AreNotEqual(_currentNeed, NullNeed.Instance);
			
			if(_currentNeed.CurrentItemListState == NeedItemStates.empty)
			{
				//There are no objects, go search them
				Finish(GoalTags.recollect);
				return;
			}

			_currentTarget = Utils.SearchInteractableFromList(_owner.position, Mathf.Infinity, _currentNeed.GetItemsList());

			//If the list doesn't find any interactable, the ItemListState should be empty.
			Assert.AreNotEqual(_currentTarget, NullInteractable.Instance);
					
			ChangeAction(ActionTags.walk);
			_currentAction.SetTarget(_currentTarget);				
		}

		public override void Execute()
		{
			Assert.IsNotNull(_currentNeed);
			Assert.AreNotEqual(_currentNeed, NullNeed.Instance);
			
			if (_currentNeed.CurrentState == NeedStates.fullfilled)
			{
				Finish(GoalTags.wander);
				return;
			}

			base.Execute();
		}

		public override void Exit()
		{
			base.Exit();
			_currentNeed = NullNeed.Instance;
			_currentAction = NullAction.Instance;
			_currentTarget = NullInteractable.Instance;
		}

		private void FinishedAction(bool completed)
		{
			switch (_currentAction.Tag)
			{
				case ActionTags.interact:
					Finish(GoalTags.wander);
					break;

				case ActionTags.walk:
					if (completed)
					{
						ChangeAction(ActionTags.interact);
						((InteractAction)_currentAction).SetTarget(_currentTarget);
					}
					else
					{
						Finish(GoalTags.wander);
					}
					break;
				default:
					Debug.LogWarning("Defualt on switch");
					Finish(GoalTags.wander);
					break;
			}
		}

		public static bool ThereAreItems(HumanNeeds _humanNeeds,string tag)
		{
			return _humanNeeds.GetNeed(tag).CurrentItemListState != NeedItemStates.empty;
		}
	}
}
