using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist.FSM
{
	public class WanderState : GoalState
	{
		public WanderState() : base()
		{
			_actionList.Add(new IdleAction());
		}

		public override void Enter()
		{
			base.Enter();

			ChangeAction(typeof(IdleAction));

		}

		public override void Execute()
		{
			base.Execute();
		}


	}
}
