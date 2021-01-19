using UnityEngine;
using Monotheist.Human;

namespace Monotheist.FSM
{
	public class IdleAction : ActionState
	{
		private float _timer;
		private float _initialTime;

		public IdleAction(HumanConfig humanConfig) : base(ActionTags.idle)
		{
			_initialTime = humanConfig.idleTime;
		}

		public override void Enter()
		{
			base.Enter();			
			_timer = _initialTime;
		}

		public override void Execute()
		{
			base.Execute();
			_timer += -1 * Time.deltaTime;

			if (_timer <= 0)
			{
				Finish(true);
			}
		}

		public override void Exit()
		{
			base.Exit();
		}

		public void SetTimer(float newTime) { _timer = newTime; }
	}
}
