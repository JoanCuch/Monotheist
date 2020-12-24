using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist
{
    public abstract class Interactable : MonoBehaviour
    {
        public abstract bool Interact(Human human);
    }
}
