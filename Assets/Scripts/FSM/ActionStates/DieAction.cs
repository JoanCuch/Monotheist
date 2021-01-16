using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.Human;

namespace Monotheist.FSM
{

	public class DieAction : ActionState
	{
		public Transform _owner;


		public DieAction(Transform owner) : base(ActionTags.die)
		{
			_owner = owner;
		}

		public override void Enter()
		{
			base.Enter();
		}
		public override void Execute()
		{
			base.Execute();
			Debug.Log(_owner.name + " has died. RIP in Pepperoni");
			Object.Destroy(_owner.gameObject);
		}
		public override void Exit()
		{
			base.Exit();
		}
	}
}
