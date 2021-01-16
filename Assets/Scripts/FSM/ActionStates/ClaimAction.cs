using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.Human;

namespace Monotheist.FSM
{
	public class ClaimAction : ActionState
	{
		private Interactable _target;
		private HumanNeeds _humanNeeds;


		public ClaimAction(HumanNeeds humanNeeds) : base(ActionTags.claim)
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
			if(_target == null)
			{
				Finish(false);
			}
			else
			{
				bool added = _target.Claim();

				if (added)
				{
					_humanNeeds.GetNeed(_target.tag).AddItem(_target);
					Finish(true);
				}
				else
				{
					Finish(false);
				}
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
