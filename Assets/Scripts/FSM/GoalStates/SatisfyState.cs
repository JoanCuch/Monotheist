using Monotheist.Human;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist.FSM
{
	public class SatisfyState : GoalState
	{
		Transform _owner;
		Interactable _currentTarget;
		Need _currentNeed;

		public SatisfyState(HumanConfig humanConfig, HumanNeeds humanNeeds, Transform owner) : base(humanConfig, humanNeeds)
		{
			_owner = owner;

			_actionList.Add(new WalkAction(_humanConfig, owner));
			_actionList.Add(new InteractAction(_humanNeeds, owner));

			foreach (ActionState action in _actionList)
			{
				action.Subscribe(FinishedAction);
			}
		}

		public override void Enter()
		{
			base.Enter();

			_currentNeed = _humanNeeds.GetUrgentNeed();

			if (_currentNeed == null)
			{
				//There is no need, go walk a little
				Finish(typeof(WanderState));
			}
			else if(_currentNeed.CurrentItemListState == NeedItemStates.empty)
			{
				//There are no objects, go search them
				Finish(typeof(RecollectState));
			}
			else
			{
				_currentTarget = SearchTarget(_currentNeed);

				if (_currentTarget == null)
				{
					//There are no objectes, go search them
					Finish(typeof(RecollectState));
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

			if (_currentNeed.CurrentStateValue == NeedStates.satisfied)
			{
				Finish(typeof(WanderState));
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
					Finish(typeof(WanderState));
					break;

				case ActionTags.walk:
					if (completed)
					{
						ChangeAction(ActionTags.interact);
						((InteractAction)_currentAction).SetTarget(_currentTarget);
					}
					else
					{
						Finish(typeof(WanderState));
					}
					break;
				default:
					Debug.LogWarning("Defualt on switch");
					Finish(typeof(WanderState));
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
	}
}
