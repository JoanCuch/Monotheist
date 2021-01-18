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
		}

		public override void Enter()
		{
			base.Enter();

			_currentNeed = _humanNeeds.GetMostUrgentNeed();

			if (_currentNeed == null)
			{
				//There is no need, go walk a little
				Finish(GoalTags.wander);
			}
			else if(_currentNeed.CurrentItemListState == NeedItemStates.empty)
			{
				//There are no objects, go search them
				Finish(GoalTags.recollect);
			}
			else
			{
				_currentTarget = SearchTarget(_currentNeed);

				if (_currentTarget == null)
				{
					//There are no objectes, go search them
					Finish(GoalTags.recollect);
				}
				else
				{
					ChangeAction(ActionTags.walk);
					_currentAction.SetTarget(_currentTarget);
				}
			}				
		}

		public override void Execute()
		{
			Assert.IsNotNull(_currentNeed);
			if (_currentNeed.CurrentState == NeedStates.fullfilled)
			{
				Finish(GoalTags.wander);
			}
			else
			{
				base.Execute();
			}		
		}

		public override void Exit()
		{
			base.Exit();
			_currentNeed = null;
			_currentAction = null;
			_currentTarget = null;
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

		private Interactable SearchTarget(Need _need)
		{
			List<Interactable> _targetList = _need.GetItemsList();
			Interactable target = null;

			float minDistance = Mathf.Infinity;

			foreach (Interactable inter in _targetList)
			{
				float distance = Vector3.Distance(inter.transform.position, _owner.position);

				if (distance < minDistance)
				{
					minDistance = distance;
					target = inter;
				}
			}
			return target;		
		}

		public static bool ThereAreItems(HumanNeeds _humanNeeds,string tag)
		{
			return _humanNeeds.GetNeed(tag).CurrentItemListState != NeedItemStates.empty;
		}
	}
}
