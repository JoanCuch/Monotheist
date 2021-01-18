using System.Collections.Generic;
using UnityEngine;
using Monotheist.Human;

namespace Monotheist.FSM
{
	public class WanderGoal : GoalState
	{
		private Transform _owner;

		public WanderGoal(HumanConfig humanConfig, HumanNeeds humanNeeds, Transform owner) : base(humanConfig, humanNeeds, GoalTags.wander)
		{
			_owner = owner;
			_actionsList = new List<ActionState>();

			_actionsList.Add(new IdleAction(humanConfig));
			_actionsList.Add(new WalkAction(humanConfig, owner));

			foreach(ActionState action in _actionsList)
			{
				action.Subscribe(FinishedAction);
			}
		}

		public override void Enter()
		{
			base.Enter();
			ChangeAction(ActionTags.idle);
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
