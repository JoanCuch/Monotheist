using Monotheist.Human;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Monotheist.FSM
{
	public class RecollectState : GoalState
	{
		private Transform _owner;
		private Need _currentNeed;
		private Interactable _currentTarget;
		private ActionTags _lastAction;

		public RecollectState(HumanConfig humanConfig, HumanNeeds humanNeeds, Transform owner) : base(humanConfig, humanNeeds)
		{
			_owner = owner;

			_actionList.Add(new WalkAction(humanConfig, owner));
			_actionList.Add(new DragAction(humanConfig, humanNeeds, owner));
			_actionList.Add(new DropAction(humanNeeds));
			_actionList.Add(new ClaimAction(humanNeeds));

			foreach(ActionState action in _actionList)
			{
				action.Subscribe(FinishedAction);
			}

		}

		public override void Enter()
		{
			base.Enter();

			_currentNeed = _humanNeeds.GetUrgentItemsNeed();

			Debug.Log("need tag: " + _currentNeed.Tag);

			if(_currentNeed == null)
			{
				Debug.LogWarning("current need null");
				Finish(typeof(WanderState));
			}
			else if(_currentNeed.CurrentItemListState == NeedItemStates.satisfied)
			{
				Debug.LogWarning("current list satisfied");
				Finish(typeof(WanderState));
			}
			else
			{
				SelectTargetAndWalk();
				_lastAction = ActionTags.drop;
			}
		}

		public override void Execute()
		{
			Assert.IsNotNull(_currentNeed);

			if (_currentNeed.CurrentItemListState == NeedItemStates.satisfied)
			{
				Debug.LogWarning("current satisifed");
				Finish(typeof(WanderState));
			}

			base.Execute();
		}

		public override void Exit()
		{
			base.Exit();
			_currentNeed = null;
			_currentTarget = null;
			_currentAction = null;
		}

		public void FinishedAction(bool completed)
		{
			switch (_currentAction.Tag)
			{
				case ActionTags.drag:
					if (completed)
					{
						ChangeAction(ActionTags.walk);
						((WalkAction)_currentAction).SetTarget(_humanNeeds.HomePosition);
					}
					else
					{
						Debug.LogWarning("something went wrong");
						Finish(typeof(WanderState));
					}
					break;

				case ActionTags.drop:
					if (completed)
					{
						//SelectTargetAndWalk();
						Debug.Log("dropped object");
						ChangeAction(ActionTags.claim);
						(_currentAction as ClaimAction).SetTarget(_currentTarget);
					}
					else
					{
						Debug.LogWarning("something went wrong");
						Finish(typeof(WanderState));
					}
					break;

				case ActionTags.walk:
					if (completed)
					{
						if (_currentTarget.Transportable)
						{
							if (_lastAction == ActionTags.drop)
							{
								_lastAction = ActionTags.drag;
								ChangeAction(ActionTags.drag);
								(_currentAction as DragAction).SetTarget(_currentTarget);
							}
							else
							{
								_lastAction = ActionTags.drop;
								ChangeAction(ActionTags.drop);
								(_currentAction as DropAction).SetTarget(_currentTarget);
							}
						}
						else
						{
							ChangeAction(ActionTags.claim);
							(_currentAction as ClaimAction).SetTarget(_currentTarget);
						}
					}
					else
					{
						Debug.LogWarning("something went wrong");
						Finish(typeof(WanderState));
					}
					break;

				case ActionTags.claim:
					Finish(typeof(WanderState));
					break;

				default:
					Debug.LogWarning("Defualt on switch");
					Finish(typeof(WanderState));
					break;
			}
		}

		/*private Interactable SearchTarget(Need _need)
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
		}*/

		private void SelectTargetAndWalk()
		{
			_currentTarget = Utils.SearchInteractable(_owner.position, _humanConfig.searchRange, _currentNeed.Tag);

			if (_currentTarget == null)
			{
				Debug.LogWarning("current target null");
				Finish(typeof(WanderState));
			}
			else
			{
				Debug.Log("target tag: " + _currentTarget.name);
				ChangeAction(ActionTags.walk);
				((WalkAction)_currentAction).SetTarget(_currentTarget.transform.position);
			}
		}

		public static bool ThereAreTargetsAround(Vector3 origin, float searchRange, string tag)
		{
			return Utils.SearchInteractable(origin, searchRange, tag) != null;
		}
	}
}
