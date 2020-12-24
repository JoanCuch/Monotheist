using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist.FSM
{
    public struct StateMachineModel
    {
        public Individual owner;
        public WalkState walkState;
        public InteractState interactState;
        public IdleState idleState;
        public StartState startState;
    }
}
