using Monotheist.Human;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Monotheist.FSM
{
	public class StartGoal: GoalState
	{
		public StartGoal(HumanConfig humanConfig, HumanNeeds humanNeeds) : base(humanConfig, humanNeeds, GoalTags.start)
		{
		}

		public override void Enter()
		{
		}

		public override void Execute()
		{
			Finish(GoalTags.wander);
		}

		public override void Exit()
		{
		}
	}
}
