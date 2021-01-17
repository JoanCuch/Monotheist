using Monotheist.Human;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Monotheist.FSM
{
	public class RecollectGoal : GoalState
	{
		private Transform _owner;
		private Need _currentNeed;
		private Interactable _currentTarget;
		private ActionTags _lastAction;

		public RecollectGoal(HumanConfig humanConfig, HumanNeeds humanNeeds, Transform owner) : base(humanConfig, humanNeeds, GoalTags.recollect)
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

			if(_currentNeed == null)
			{
				Debug.LogWarning("current need null");
				Finish(GoalTags.wander);
			}
			else if(_currentNeed.CurrentItemListState == NeedItemStates.satisfied)
			{
				Debug.LogWarning("current list satisfied");
				Finish(GoalTags.wander);
			}
			else
			{
				
				/*if(_currentNeed.Tag == NeedTags.satiation.ToString() &&
					_humanNeeds.GetNeed(NeedTags.home.ToString()).CurrentItemListState == NeedItemStates.empty
					)
				{
					Finish(GoalTags.recollect);
				}*/
				
				
				SelectTargetAndWalk();
				_lastAction = ActionTags.drop;
			}
		}

		public override void Execute()
		{
			Assert.IsNotNull(_currentNeed);
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
					if(completed == false)
					{
						Debug.LogWarning("something went wrong");
						Finish(GoalTags.wander);
						break;
					}
					
					ChangeAction(ActionTags.walk);
					((WalkAction)_currentAction).SetTarget(_humanNeeds.HomePosition);
					break;

				case ActionTags.drop:
					if (completed == false)
					{
						Debug.LogWarning("something went wrong");

					}
											
					Finish(GoalTags.wander);		
					break;

				case ActionTags.walk:
					if (completed == false)
					{
						Finish(GoalTags.wander);
						break;
					}
				
					if (_lastAction == ActionTags.drop)
					{
						//The human get to the object and wants to recollect it. Bet before, he has to claim it.
						ChangeAction(ActionTags.claim);
						(_currentAction as ClaimAction).SetTarget(_currentTarget);
					}
					else
					{
						//The human already has the object and wants to release it.
						_lastAction = ActionTags.drop;
						ChangeAction(ActionTags.drop);
						(_currentAction as DropAction).SetTarget(_currentTarget);
					}
					
					
					
					break;

				case ActionTags.claim:
					if(completed == false)
					{
						//The item was already claimed or there was no item at all
						Finish(GoalTags.wander);
						break;
					}

					if (_currentTarget.Transportable) 
					{
						//After the item is claimed, get it.
						_lastAction = ActionTags.drag;
						ChangeAction(ActionTags.drag);
						(_currentAction as DragAction).SetTarget(_currentTarget);
					}
					else
					{
						Finish(GoalTags.wander);
					}
					break;

				default:
					Debug.LogWarning("Defualt Recollect State switch");
					Finish(GoalTags.wander);
					break;
			}
		}

		private void SelectTargetAndWalk()
		{
			_currentTarget = Utils.SearchInteractable(_owner.position, _humanConfig.searchRange, _currentNeed.Tag);

			if (_currentTarget == null)
			{
				Debug.LogWarning("current target null");
				Finish(GoalTags.wander);
			}
			else
			{
				ChangeAction(ActionTags.walk);
				((WalkAction)_currentAction).SetTarget(_currentTarget.transform.position);
			}
		}

		public static bool SearchUnclaimedItemsAround(Vector3 origin, float searchRange, string tag)
		{
			Interactable item = Utils.SearchInteractable(origin, searchRange, tag);

			if (item != null && item.Owned == false) return true;

			return false;
		}
	}
}
