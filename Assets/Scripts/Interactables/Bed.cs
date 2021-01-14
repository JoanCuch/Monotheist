using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.Human;
using UnityEngine.Assertions;

namespace Monotheist
{
    public class Bed : Interactable
    {
        [SerializeField] private float _regenerationPerSecond;


        void Update()
        {
        
        }

        public override bool Interact(HumanNeeds humanNeeds)
		{
            Need need = humanNeeds.GetNeed(tag);

            Assert.IsNotNull(need);
            
            need.AddSatisfaction(_regenerationPerSecond * Time.deltaTime);
            return true;
            
		}
    }
}
