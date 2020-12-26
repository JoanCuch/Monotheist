using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist
{
    public class Food : Interactable
    {
        [SerializeField] private float _nutritionPerSecond;
        [SerializeField] private float _totalNutrition;


		void Start()
        {

        }

        void Update()
        {

        }

        public override bool Interact(HumanNecessities human)
        {
            float nutrition = _nutritionPerSecond * Time.deltaTime;
            
            if(_totalNutrition >= nutrition)
			{
                human.AddSatiation(nutrition);
                _totalNutrition -= nutrition;
                return true;
			}
			else
			{
                human.AddSatiation(_totalNutrition);
                Destroy(this.gameObject);
                return false;
			}
        }
    }
}
