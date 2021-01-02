using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Monotheist.FSM
{
    public interface State
    {
        void Enter();
        void Execute();
        void Exit();
        void Subscribe(Action<Type> action);
    }
}
