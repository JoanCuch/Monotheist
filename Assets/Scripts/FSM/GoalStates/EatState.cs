using Monotheist.Human;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist.FSM
{
	public class EatState : GoalState
	{
		public EatState(HumanConfig humanConfig, HumanNeeds humanNeeds) : base(humanConfig, humanNeeds)
		{
		}
	}
}
