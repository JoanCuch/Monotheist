using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Monotheist.FSM {
	public class WalkState : State
	{
		public override void Enter()
		{

		}

		public override void Execute()
		{		
			Individual owner = _model.owner;

			if (Vector2.Distance(owner.Target.position, owner.transform.position) <= owner.TargetRange)
			{
				ChangeState(_model.idleState);
			}
			else
			{
				owner.transform.position = Vector2.MoveTowards(owner.transform.position, owner.Target.position, owner.Velocity * Time.deltaTime);
			}
		}

		public override void Exit()
		{

		}
	}
}
