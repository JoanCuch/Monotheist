using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;

namespace Monotheist.Human
{
	public class NullNeed : Need
	{
		public static NullNeed _instance;
		
		public static NullNeed Instance
		{
			get
			{
				if (_instance == null)
					_instance = new NullNeed();

				return _instance;
			}
		}

		
		
		public NullNeed() : base(HumanConfig.NullConfig)
		{	
			
		}

		public new void Update() { }

		public new void AddItem(Interactable item) { }

		public new void RemoveItem(Interactable item) { }

		public new List<Interactable> GetItemList()
		{
			Assert.IsTrue(false);
			return null;
		}

		public new void Subscribe(Action<Need> action) { }
	}
}
