using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Monotheist.FSM
{
	public class NullAction : ActionState
	{
		private static NullAction _instance;

		public static NullAction Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new NullAction();
				}
				return _instance;
			}
		}

		public NullAction() : base(ActionTags.nullAction)
		{
		}

		public override void Enter()
		{
		}
		public override void Execute()
		{
		}

		public override void Exit()
		{
		}
	}
}
