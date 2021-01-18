using UnityEngine;
using Monotheist.Human;

namespace Monotheist.FSM
{
    public class DragAction : ActionState
    {
		Interactable _target;
		HumanConfig _humanConfig;
		Transform _owner;

		public DragAction(HumanConfig humanConfig, Transform owner) : base(ActionTags.drag)
		{
			_humanConfig = humanConfig;
			_owner = owner;
			_target = NullInteractable.Instance;
		}

		public override void Enter()
		{
			base.Enter();
		}
		public override void Execute()
		{
			if (_target == NullInteractable.Instance)
				NullInteractable.Instance.SendError();

			base.Execute();			

			if (Vector2.Distance(_target.transform.position, _owner.position) <= _humanConfig.interactRange)
			{
				_target.transform.SetParent(_owner);
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
			_target = NullInteractable.Instance;
		}

		public override void SetTarget(Interactable target)
		{
			_target = target;
		}
	}
}
