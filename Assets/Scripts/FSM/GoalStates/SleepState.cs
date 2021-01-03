using Monotheist.Human;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist.FSM
{
	public class SleepState : GoalState
	{
		public SleepState(HumanConfig humanConfig, HumanNeeds humanNeeds) : base(humanConfig, humanNeeds)
		{
		}
	}
}
