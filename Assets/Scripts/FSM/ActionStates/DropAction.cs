using Monotheist.Human;

namespace Monotheist.FSM
{
	public class DropAction : ActionState
	{
		HumanNeeds _humanNeeds;
		Interactable _target;

		public DropAction(HumanNeeds humanNeeds) : base(ActionTags.drop)
		{
			_humanNeeds = humanNeeds;
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
			
			_target.transform.SetParent(null);
			Finish(true);
		}

		public override void Exit()
		{
			base.Exit();
		}

		public override void SetTarget(Interactable target)
		{
			_target = target;
		}
	}
}
