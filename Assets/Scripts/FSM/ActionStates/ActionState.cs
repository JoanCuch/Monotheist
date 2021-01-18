using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Monotheist.FSM
{
	public abstract class ActionState : State
	{
		public readonly ActionTags Tag;

		private Action<bool> _finished;

		public ActionState(ActionTags tag)
		{
			Tag = tag;
		}

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
			Assert.IsNotNull(_finished);
			_finished.Invoke(completed);
		}

		public virtual void SetTarget(Interactable target)
		{
		
		}
	}
}
