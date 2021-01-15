﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.Human;
using UnityEngine.Assertions;
using System;

namespace Monotheist
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] protected bool _owned;
        [SerializeField] protected bool _transportable;

        private Action<Interactable> onDestroy;


        public bool Transportable => _transportable;

        void Start()
		{
        }


		public abstract bool Interact(HumanNeeds humanNeeds);

        public bool Claim()
		{
            if(_owned)
			{
                return false;
			}
            else
			{
                _owned = true;
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
