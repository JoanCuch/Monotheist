using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.Human;
using UnityEngine.Assertions;
using System;

namespace Monotheist
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] protected bool _hasOwner;

        private Action<Interactable> onDestroy;

		void Start()
		{
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

        public void SubscribeOnDestroy(Action<Interactable> action)
		{
            onDestroy += action;
		}

		private void OnDestroy()
		{
            if(onDestroy != null)onDestroy.Invoke(this);
		}
	}
}
