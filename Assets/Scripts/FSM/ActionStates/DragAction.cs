using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.Human;

namespace Monotheist.FSM
{
    public class DragAction : ActionState
    {
		Interactable _target;
		HumanNeeds _humanNeeds;
		HumanConfig _humanConfig;
		Transform _owner;

		public DragAction(HumanConfig humanConfig, HumanNeeds humanNeeds, Transform owner) : base(ActionTags.drag)
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
		}
		public override void Exit()
		{
			base.Exit();

			if(_target != null)
			{
				if(Vector3.Distance(_target.transform.position, _owner.position) <= _humanConfig.interactRange)
				{
					bool completed = _humanNeeds.SetDragObject(_target);

					if (completed)
					{
						_target.transform.SetParent(_owner);
					}

					Finish(completed);
				}
				else
				{
					Finish(false);
				}
			}
			else
			{
				Finish(false);
			}
		}

		public void SetTarget(Interactable target)
		{
			_target = target;
		}
	}
}
