using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist
{
    public class Bed : Interactable
    {	
		void Start()
        {

        }

        void Update()
        {
        
        }

        public override void Interact(Human human)
		{
            human.AddEnergy(100);
		}
    }
}
