﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist.FSM
{
	public class IdleState : State
	{
		public override void Enter()
		{
		}

		public override void Execute()
		{
			if(_model.owner.GetCurrentNecessity() != HumanConfig.Necessities.fullfilled)
			{
				ChangeState(_model.walkState);
			}
		}

		public override void Exit()
		{
		}
	}
}