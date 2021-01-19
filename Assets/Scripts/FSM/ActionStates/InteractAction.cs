using Monotheist.Human;
using UnityEngine.Assertions;

namespace Monotheist.FSM
{
	public class InteractAction : ActionState 
	{
		HumanNeeds _humanNeeds;
		Interactable _target;

		public InteractAction(HumanNeeds humanNeeds) : base(ActionTags.interact)
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
			base.Execute();
			Assert.AreNotEqual(_target, NullInteractable.Instance);
			bool canContinue = _target.Interact(_humanNeeds);

			if (!canContinue)
				Finish(true);
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
