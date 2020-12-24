using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Monotheist.FSM
{
	public class StartState : State
	{
		public StartState(StateMachineModel model) : base(model)
		{
		}

		public override void Enter()
		{
		}

		public override void Execute()
		{
			ChangeState(_model.idleState);
		}

		public override void Exit()
		{
		}
	}
}
