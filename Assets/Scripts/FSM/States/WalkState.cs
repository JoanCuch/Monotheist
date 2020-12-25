using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Monotheist.FSM {
	public class WalkState : State
	{
		private HumanConfig _ownerConfig;
		public WalkState(HumanConfig humanConfig)
		{
			_ownerConfig = humanConfig;
		}
		
		public override void Enter()
		{
			_model.owner.SearchTarget("Bed");
		}

		public override void Execute()
		{		
			HumanNecessities owner = _model.owner;

			if (Vector2.Distance(owner.Target.position, owner.transform.position) <= _ownerConfig.interactRange)
			{
				ChangeState(_model.interactState);
			}
			else
			{
				owner.transform.position = Vector2.MoveTowards(owner.transform.position, owner.Target.position, _ownerConfig.velocity * Time.deltaTime);
			}
		}

		public override void Exit()
		{

		}
	}
}
