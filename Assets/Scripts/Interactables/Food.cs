using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.Human;

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

        /// <summary>
        /// Interact with the humanNeeds class and returns false when the object cannot interact any more.
        /// </summary>
        /// <param name="humanNeeds"></param>
        /// <returns></returns>
        public override bool Interact(HumanNeeds humanNeeds)
        {
            float nutrition = _nutritionPerSecond * Time.deltaTime;
            
            if(_totalNutrition >= nutrition)
			{
                //TODO being able to change needs
                //human.AddSatiation(nutrition);
                _totalNutrition -= nutrition;
                return true;
			}
			else
			{
                //human.AddSatiation(_totalNutrition);
                Destroy(this.gameObject);
                return false;
			}
        }
    }
}
