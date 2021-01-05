using Monotheist.Human;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Monotheist.FSM
{
	public class StartState : GoalState
	{
		public StartState(HumanConfig humanConfig, HumanNeeds humanNeeds) : base(humanConfig, humanNeeds)
		{
		}

		public override void Enter()
		{
		}

		public override void Execute()
		{
			base.Execute();
			Finish(typeof(WanderState));
		}

		public override void Exit()
		{
		}
	}
}
