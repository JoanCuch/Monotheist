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
            Need need = humanNeeds.GetNeed(_tag);

            if (need == null)
            {
                return false;
            }
            else
            {
                need.AddSatisfaction(_regenerationPerSecond * Time.deltaTime);
                return true;
            }
		}
    }
}
