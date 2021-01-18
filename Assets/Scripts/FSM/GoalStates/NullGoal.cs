using Monotheist.Human;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist.FSM
{
    public class NullGoal : GoalState
    {
		private static NullGoal _instance;

		public static NullGoal Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new NullGoal();
				}
				return _instance;
			}
		}

		public NullGoal() : base(null, null, GoalTags.nullGoal)
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
