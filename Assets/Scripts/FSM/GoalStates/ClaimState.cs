using Monotheist.Human;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
			base.Enter();

			_currentNeed = _humanNeeds.GetUrgentNeed();

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

		private Interactable SearchTarget(Need _need)
		{
			//Search for the acceptable objects around
			Collider2D[] colliders = Physics2D.OverlapCircleAll(_owner.position, _humanConfig.searchRange);

			List<Interactable> _targetList = _need.GetItemsList();

			string needTag = _humanNeeds.GetUrgentNeed().NeedConfig.tag;

			foreach (Collider2D collider in colliders)
			{
				if(collider.tag == needTag)
				{
					_targetList.Add(collider.GetComponent<Interactable>());
				}
			}
			
			//From the around objects, go to the nearest
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
