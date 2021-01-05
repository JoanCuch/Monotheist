using Monotheist.Human;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist.FSM {

	public class DeathState : GoalState
	{
		public DeathState(HumanNeeds humanNeeds, HumanConfig humanConfig, Transform owner) : base(humanConfig, humanNeeds)
		{

			_actionList.Add(new DieAction(owner));
		}

		public override void Enter()
		{
			ChangeAction(ActionTags.die);
		}

		public override void Execute()
		{
			
		}

		public override void Exit()
		{
		}
	}
}
