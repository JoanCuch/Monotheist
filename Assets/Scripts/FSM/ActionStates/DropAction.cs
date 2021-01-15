using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.Human;
using UnityEngine.Assertions;

namespace Monotheist.FSM
{
	public class DropAction : ActionState
	{
		HumanNeeds _humanNeeds;
		Interactable _target;

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
		
			if(_target != null)
			{
				_target.transform.SetParent(null);
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

		public void SetTarget(Interactable target)
		{
			_target = target;
		}
	}
}
