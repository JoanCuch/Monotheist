using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist.FSM
{
	public class IdleAction : ActionState
	{
		private float _timer;

		public IdleAction() : base(ActionTags.idle)
		{
		}


		public override void Enter()
		{
			base.Enter();			
			_timer = 1;
		}

		public override void Execute()
		{
			base.Execute();
			_timer += -1 * Time.deltaTime;

			if (_timer <= 0) Finish(true);
		}

		public override void Exit()
		{
			base.Exit();
		}

		public void SetTimer(float newTimer) { _timer = newTimer; }
	}
}
