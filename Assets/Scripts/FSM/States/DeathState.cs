using Monotheist.Human;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist.FSM {

	public class DeathState : GoalState
	{
		public DeathState(HumanNeeds humanNeeds, HumanConfig humanConfig) : base(humanConfig, humanNeeds)
		{
		}

		public override void Enter()
		{
			//TODO implement Death
		}

		public override void Execute()
		{
		}

		public override void Exit()
		{
		}
	}
}
