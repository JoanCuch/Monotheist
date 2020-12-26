using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist.FSM {

	public class DeathState : State
	{
		public override void Enter()
		{
			Debug.LogWarning(_model.owner.name + " has died. Rin in pepperoni");
			GameObject.Destroy(_model.owner.gameObject);
		}

		public override void Execute()
		{
		}

		public override void Exit()
		{
		}
	}
}
