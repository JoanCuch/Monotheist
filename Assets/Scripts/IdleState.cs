using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist.FSM
{
	public class IdleState : State
	{
		public IdleState(StateMachineModel model) : base(model)
		{
		}

		public override void Enter()
		{
		}

		public override void Execute()
		{
		}

		public override void Exit()
		{
			throw new System.NotImplementedException();
		}
	}
}
