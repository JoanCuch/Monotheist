using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public override bool Interact(HumanNecessities human)
		{
            human.AddEnergy(_regenerationPerSecond * Time.deltaTime);
            return true;
		}
    }
}
