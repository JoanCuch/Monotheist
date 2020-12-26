using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist
{
	[CreateAssetMenu(menuName = "Configuration", fileName = "Configuration")]
	public class HumanConfig : ScriptableObject
	{
		public enum Necessities
		{
			fullfilled,
			energy,
			satiation
		}
		
		//Energy
		[Range(0, 100)] public float initialEnergy;
		[Range(0, 100)] public float energyTiredLimit;
		[Range(0, 100)] public float energyAwakeLimit;
		public float energyReductionPerSecond;

		//Satiation
		[Range(0, 100)] public float initialSatiation;
		[Range(0, 100)] public float satiationHungryLimit;
		[Range(0, 100)] public float satiationSatiatedLimit;
		public float satiationReductionPerSecond;

		//Movement & Interact
		public float velocity;
		public float interactRange;
		public float searchRange;
	}
}
