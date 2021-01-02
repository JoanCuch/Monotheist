using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.Human;

namespace Monotheist
{
    public class Bed : Interactable
    {
        [SerializeField] private float _regenerationPerSecond;
        
        
        void Start()
        {

        }

        void Update()
        {
        
        }

        public override bool Interact(HumanNeeds human)
		{
            //TODO being able to add energy
            //human.AddEnergy(_regenerationPerSecond * Time.deltaTime);
            return true;
		}
    }
}
