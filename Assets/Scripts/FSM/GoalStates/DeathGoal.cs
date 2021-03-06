﻿using Monotheist.Human;
using UnityEngine;

namespace Monotheist.FSM {

	public class DeathGoal : GoalState
	{
		public DeathGoal(HumanNeeds humanNeeds, HumanConfig humanConfig, Transform owner) : base(humanConfig, humanNeeds, GoalTags.die)
		{
			_actionsList.Add(new DieAction(owner));
		}

		public override void Enter()
		{
			ChangeAction(ActionTags.die);
		}

		public override void Execute()
		{
			base.Execute();
		}

		public override void Exit()
		{
		}
	}
}
