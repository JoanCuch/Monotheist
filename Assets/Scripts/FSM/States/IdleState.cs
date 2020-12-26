using System.Collections;
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
			base.Execute();
			if(_model.owner.GetCurrentNecessity() != HumanConfig.Necessities.fullfilled)
			{
				Debug.Log("New necessity detected: " + _model.owner.GetCurrentNecessity().ToString());
				_model.owner.SearchTarget(_model.owner.GetCurrentNecessity().ToString());

				if (_model.owner.Target != null)
				{
					ChangeState(_model.walkState);					
				}
				else
				{
					Debug.Log("no target found");
				}			
			}
		}

		public override void Exit()
		{
		}
	}
}
