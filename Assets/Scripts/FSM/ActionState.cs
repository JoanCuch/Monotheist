using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist.FSM
{
	public abstract class ActionState : State
	{
		private Action<bool> _finished;

		public virtual void Enter()
		{
		}

		public virtual void Execute()
		{

		}

		public virtual void Exit()
		{

		}

		public void Subscribe(Action<bool> action)
		{
			_finished += action;
		}

		protected void Finish(bool completed)
		{
			_finished.Invoke(completed);
		}
	}
}
