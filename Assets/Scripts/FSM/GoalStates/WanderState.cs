using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.Human;

namespace Monotheist.FSM
{
	public class WanderState : GoalState
	{
		private Transform _owner;

		public WanderState(HumanConfig humanConfig, HumanNeeds humanNeeds, Transform owner) : base(humanConfig, humanNeeds)
		{
			_owner = owner;
			_actionList = new List<ActionState>();
			_actionList.Add(new IdleAction());
			_actionList.Add(new WalkAction(humanConfig, owner));

			foreach(ActionState action in _actionList)
			{
				action.Subscribe(FinishedAction);
			}
		}

		public override void Enter()
		{
			base.Enter();
			Debug.Log("enter wander");
			ChangeAction(ActionTags.idle);
			((IdleAction)_currentAction).SetTimer(_humanConfig.idleTime);
		}

		public override void Execute()
		{
			base.Execute();
		}

		public void FinishedAction(bool completed)
		{
			switch (_currentAction.Tag)
			{
				case ActionTags.walk:
					ChangeAction(ActionTags.idle);
					((IdleAction)_currentAction).SetTimer(_humanConfig.idleTime); 
					break;

				case ActionTags.idle:
					ChangeAction(ActionTags.walk);

					float randomX = Random.Range(-1 * _humanConfig.wanderRange, _humanConfig.wanderRange);
					float randomY = Random.Range(-1 * _humanConfig.wanderRange, _humanConfig.wanderRange);
					Vector3 randomPosition = new Vector3(_owner.position.x + randomX, _owner.position.y + randomY, _owner.position.z);

					((WalkAction)_currentAction).SetTarget(randomPosition);
					break;
			}		
		}
	}
}
