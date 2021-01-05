using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.Human;

namespace Monotheist.FSM
{
	public class DropAction : ActionState
	{
		HumanNeeds _humanNeeds;

		public DropAction(HumanNeeds humanNeeds) : base(ActionTags.drop)
		{
			_humanNeeds = humanNeeds;
		}

		public override void Enter()
		{
			base.Enter();
		}
		public override void Execute()
		{
			base.Execute();

			Interactable target = _humanNeeds.GetDragObject();
			
			if(target != null)
			{
				target.transform.SetParent(null);
				Finish(true);
			}
			else
			{
				Finish(false);
			}
		}

		public override void Exit()
		{
			base.Exit();
		}
	}
}
