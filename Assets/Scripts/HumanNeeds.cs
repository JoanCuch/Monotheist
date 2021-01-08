using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.FSM;

namespace Monotheist.Human
{
	public class HumanNeeds
	{
		private List<Need> _needList;
		private List<Need> _unsatisfiedList;
		private List<Need> _criticList;

		//This should not be here
		private Interactable _dragObject;

		public HumanNeeds(HumanConfig humanConfig)
		{
			_needList = new List<Need>();
			_unsatisfiedList = new List<Need>();
			_criticList = new List<Need>();
			
			_needList.Add(new Need(humanConfig.energyConfig));
			_needList.Add(new Need(humanConfig.satiationConfig));

			foreach(Need need in _needList)
			{
				need.Subscribe(UpdateNeedList);
			}
		}

		public void Update()
		{
			foreach(Need need in _needList)
			{
				need.Update();
			}
		}

		public Need GetUrgentNeed()
		{
			if(_criticList.Count > 0)
			{
				return GetUrgentNeedFromList(_criticList);
			}
			else if(_unsatisfiedList.Count > 0)
			{
				return GetUrgentNeedFromList(_unsatisfiedList);
			}
			else
			{
				return GetUrgentNeedFromList(_needList);
			}
		}

		public Need GetUrgentItemsNeed()
		{
			//TODO get need that requires objects
			return GetUrgentNeed();
		}

		private Need GetUrgentNeedFromList(List<Need> list)
		{
			Need urgent = null;

			foreach (Need need in list)
			{
				if (urgent == null) urgent = need;
				else if (need.Satisfaction < urgent.Satisfaction) urgent = need;
			}

			return urgent;
		}

		private void UpdateNeedList(Need need)
		{
			switch (need.LastState)
			{
				case NeedStates.unsatisfied:
					_unsatisfiedList.Remove(need);
					break;
				case NeedStates.critic:
					_criticList.Remove(need);
					break;	
			}

			switch (need.CurrentState)
			{
				case NeedStates.unsatisfied:
					_unsatisfiedList.Add(need);
					break;
				case NeedStates.critic:
					_criticList.Add(need);
					break;
			}
		}

		public Interactable GetDragObject()
		{
			_dragObject = null;
			return _dragObject;
		}

		public bool SetDragObject(Interactable dragObject)
		{
			if(_dragObject == null)
			{
				_dragObject = dragObject;
				return true;
			}
			else
			{
				return false;
			}
		}

		public Vector3 HomePosition => Vector3.zero;		
	}
}
