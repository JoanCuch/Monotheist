using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Monotheist.Human
{
	public class HumanNeeds
	{
		private List<Need> _needList;
		private List<Need> _unsatisfiedList;
		private List<Need> _criticList;

		public HumanNeeds(HumanConfig humanConfig)
		{
			_needList = new List<Need>();
			_unsatisfiedList = new List<Need>();
			_criticList = new List<Need>();
			
			_needList.Add(new Need(humanConfig.energyConfig));
			_needList.Add(new Need(humanConfig.satiationConfig));
			//_needList.Add(new Need(humanConfig.homeConfig));

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

		public Need GetMostUrgentNeed()
		{
			if(_criticList.Count > 0)
			{
				return GetNeedViaSatisfaction(_criticList);
			}
			else if(_unsatisfiedList.Count > 0)
			{
				return GetNeedViaSatisfaction(_unsatisfiedList);
			}
			else
			{
				return GetNeedViaSatisfaction(_needList);
			}
		}

		public Need GetMostUrgentItemNeed()
		{
			if (_criticList.Count > 0)
			{
				return GetNeedViaItemList(_criticList);
			}
			else if (_unsatisfiedList.Count > 0)
			{
				return GetNeedViaItemList(_unsatisfiedList);
			}
			else
			{
				return GetNeedViaItemList(_needList);
			}
		}

		public Need GetNeed(string tag)
		{
			Need urgent = null;

			foreach (Need need in _needList)
			{
				if (need.Tag == tag) urgent = need;
			}
			Assert.IsNotNull(urgent);
			return urgent;
		}

		private Need GetNeedViaSatisfaction(List<Need> list)
		{
			Need urgent = null;

			foreach (Need need in list)
			{
				if (urgent == null) urgent = need;
				else if (need.Satisfaction < urgent.Satisfaction)
				{
					urgent = need;
				}
			}
			Assert.IsNotNull(urgent);
			return urgent;
		}

		private Need GetNeedViaItemList(List<Need> list)
		{
			Need urgent = null;

			foreach (Need need in list)
			{
				if (urgent == null) urgent = need;
				else if (need.ItemsListCount.Value < urgent.ItemsListCount.Value) urgent = need;
			}
			Assert.IsNotNull(urgent);
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

		public Vector3 HomePosition => Vector3.zero;

		public List<Need> NeedList => _needList;		
	}
}
