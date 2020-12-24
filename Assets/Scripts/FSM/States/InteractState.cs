using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist.FSM
{
	public class InteractState : State
	{
		Interactable item;
		public override void Enter()
		{
			item = _model.owner.Target.transform.GetComponent<Interactable>();
			if (item == null) ChangeState(_model.idleState);
		}

		public override void Execute()
		{
			if(item != null) item.Interact(_model.owner);
			
			ChangeState(_model.idleState);
		}

		public override void Exit()
		{
			item = null;
		}
	}
}
