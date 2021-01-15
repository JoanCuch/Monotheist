using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.Human;
using UnityEngine.Assertions;

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

			Assert.IsNotNull(_target);

			if (Vector2.Distance(_target.transform.position, _owner.position) <= _humanConfig.interactRange)
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
