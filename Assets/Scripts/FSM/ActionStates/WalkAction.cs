using UnityEngine;
using Monotheist.Human;

namespace Monotheist.FSM {
	public class WalkAction : ActionState
	{	
		private HumanConfig _humanConfig;
		private Vector3 _target;
		private Transform _owner;
		
		public WalkAction(HumanConfig humanConfig, Transform owner) : base(ActionTags.walk)
		{
			_humanConfig = humanConfig;
			_owner = owner;
			_target = Vector3.zero;
		}
		
		public override void Enter()
		{
			base.Enter();
		}

		public override void Execute()
		{	
			base.Execute();

			if (Vector2.Distance(_target, _owner.position) <= _humanConfig.interactRange)
			{
				Finish(true);
				return;
			}
			
			_owner.position = Vector2.MoveTowards(_owner.position, _target, _humanConfig.velocity * Time.deltaTime);		
		}

		public override void Exit()
		{
			base.Exit();
			_target = Vector3.zero;
		}

		public override void SetTarget(Interactable target)
		{
			_target = target.transform.position;
		}
		public  void SetTarget(Vector3 target)
		{
			_target = target;
		}
	}
}
