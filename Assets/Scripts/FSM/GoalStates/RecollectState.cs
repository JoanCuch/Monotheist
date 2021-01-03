using Monotheist.Human;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist.FSM
{
	public class RecollectState : GoalState
	{
		public RecollectState(HumanConfig humanConfig, HumanNeeds humanNeeds) : base(humanConfig, humanNeeds)
		{
		}
	}
}
