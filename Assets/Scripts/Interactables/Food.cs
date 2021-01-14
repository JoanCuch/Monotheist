using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.Human;
using UnityEngine.Assertions;

namespace Monotheist
{
    public class Food : Interactable
    {
        [SerializeField] private float _nutritionPerSecond;
        [SerializeField] private float _totalNutrition;

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

            Need need = humanNeeds.GetNeed(tag);

            Assert.IsNotNull(need);

            if(_totalNutrition >= nutrition)
			{
                need.AddSatisfaction(nutrition * Time.deltaTime);
                _totalNutrition -= nutrition;
                return true;
			}
			else
			{
                need.AddSatisfaction(nutrition * Time.deltaTime);
                Destroy(this.gameObject);
                return false;
			}
        }
    }
}
