using Monotheist.Human;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Monotheist
{
    public class NullInteractable : Interactable
    {
        private static NullInteractable _instance;

		public static NullInteractable Instance => _instance;

		public void Awake()
		{
			_instance = this;
			Debug.Log("Null interactable: " + this);
		}

		public override bool Interact(HumanNeeds humanNeeds)
		{
            Assert.IsTrue(false, "There is a null Interactable being activated");
            return false;
		}	
		
		public void SendError()
		{
			Assert.IsTrue(false, "There is a null Interactable being activated");
		}
    }
}

