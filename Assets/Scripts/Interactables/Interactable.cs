using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.Human;

namespace Monotheist
{
    public abstract class Interactable : MonoBehaviour
    {
        protected bool _hasOwner;

        public abstract bool Interact(HumanNeeds human);

        public bool AddOnwer()
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
