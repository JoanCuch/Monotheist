using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.Human;

namespace Monotheist
{
    public abstract class Interactable : MonoBehaviour
    {
        public abstract bool Interact(HumanNeeds human);
    }
}
