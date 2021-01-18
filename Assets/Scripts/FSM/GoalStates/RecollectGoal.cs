using Monotheist.Human;
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

			_actionsList.Add(new WalkAction(humanConfig, owner));
			_actionsList.Add(new DragAction(humanConfig, owner));
			_actionsList.Add(new DropAction(humanNeeds));
			_actionsList.Add(new ClaimAction(humanNeeds));

			foreach(ActionState action in _actionsList)
			{
				action.Subscribe(FinishedAction);
			}

			_currentTarget = NullInteractable.Instance;
		}

		public override void Enter()
		{
			base.Enter();

			_currentNeed = _humanNeeds.GetMostUrgentItemNeed();

			Assert.IsNotNull(_currentNeed);
			
			if(_currentNeed.CurrentItemListState == NeedItemStates.satisfied)
			{
				Finish(GoalTags.wander);
			}
			else
			{				
				SelectTargetAndWalk();
				_lastAction = ActionTags.drop;
			}

			Finish(GoalTags.wander);
		}

		public override void Execute()
		{
			Assert.IsNotNull(_currentNeed);
			base.Execute();
		}

		public override void Exit()
		{
			base.Exit();
			_currentTarget = NullInteractable.Instance;
			_currentAction = NullAction.Instance;
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
						_currentAction.SetTarget(_currentTarget);
					}
					else
					{
						//The human already has the object and wants to release it.
						_lastAction = ActionTags.drop;
						ChangeAction(ActionTags.drop);
						_currentAction.SetTarget(_currentTarget);
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
						_currentAction.SetTarget(_currentTarget);
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
			Interactable newTarget = Utils.SearchInteractable(_owner.position, _humanConfig.searchRange, _currentNeed.Tag);

			if (newTarget == NullInteractable.Instance)
			{
				Finish(GoalTags.wander);
				return;
			}

			
			_currentTarget = newTarget;
			ChangeAction(ActionTags.walk);
			_currentAction.SetTarget(_currentTarget);			
		}

		public static bool ThereAreUnclaimedItemsAround(Vector3 origin, float searchRange, string tag)
		{
			Interactable item = Utils.SearchInteractable(origin, searchRange, tag);

			if (item != NullInteractable.Instance && item.Owned == false)
			{
				return true;
			}

			return false;
		}
	}
}
