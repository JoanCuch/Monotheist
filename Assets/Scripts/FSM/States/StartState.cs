using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Monotheist.FSM
{
	public class StartState : State
	{

		public override void Enter()
		{
		}

		public override void Execute()
		{
			base.Execute();
			ChangeState(_model.idleState);
		}

		public override void Exit()
		{
		}
	}
}
