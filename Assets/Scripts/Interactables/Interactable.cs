using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.Human;
using UnityEngine.Assertions;

namespace Monotheist
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] protected bool _hasOwner;
        [SerializeField] protected string _tag;

		public void Start()
		{
            Assert.AreNotEqual(_tag.Length, 0, name + "has an empty tag");
        }


		public abstract bool Interact(HumanNeeds humanNeeds);

        public bool Claim()
		{
            if(_hasOwner)
			{
                return false;
			}
            else
			{
                _hasOwner = true;
                return true;
            }
        }
    }
}
