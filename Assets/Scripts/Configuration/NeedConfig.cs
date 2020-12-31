using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist.Human
{
    [CreateAssetMenu(fileName = "NeedConfig")]
    public class NeedConfig : ScriptableObject
    {
        public float satisfactionInitial;
        
        public float satisfactionReductionPerSecond;
        public string tag;

        public float satisfactionMax;
        public float satisfactionUnsatisfiedLevel;
        public float satisfactionCriticLevel;
        public float satisfactionLethalLevel;
        public float satisfactionMin;

        public float itemListMax;
        public float itemListUnsatisfiedLevel;

    }
}
