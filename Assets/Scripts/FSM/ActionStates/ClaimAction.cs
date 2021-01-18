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
			
			bool claimed = _target.Claim();

			if (claimed)
			{
				_humanNeeds.GetNeed(_target.tag).AddItem(_target);
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
