using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.Human;

namespace Monotheist.FSM
{
	public class InteractAction : ActionState 
	{
		HumanNeeds _humanNeeds;
		HumanConfig _humanConfig;
		Transform _owner;
		Interactable _target;

		public InteractAction(HumanConfig humanConfig, HumanNeeds humanNeeds, Transform owner) : base(ActionTags.interact)
		{
			_humanConfig = humanConfig;
			_humanNeeds = humanNeeds;
			_owner = owner;
		}

		public override void Enter()
		{
			base.Enter();
			
		}

		public override void Execute()
		{
			base.Execute();


			bool canContinue = _target.Interact(_humanNeeds);

			if (!canContinue) Finish(true);


			/*if (_target != null)
			{
				if (Vector3.Distance(_target.transform.position, _owner.position) >= _humanConfig.interactRange)
				{
					Finish(false);
				}
				else
				{
					bool canContinue = _target.Interact(_humanNeeds);

					if(!canContinue)Finish(true);
				}
			}*/
		}

		public override void Exit()
		{
			base.Exit();
			_target = null;
		}

		public void SetTarget(Interactable target)
		{
			_target = target;
		}
	}
}
