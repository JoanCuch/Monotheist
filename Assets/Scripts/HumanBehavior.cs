using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.FSM;

namespace Monotheist
{
    public class HumanBehavior : MonoBehaviour
    {
        StateMachineController _stateMachine;

        void Start()
        {
        }

        void Update()
        {
            if(_stateMachine != null)
            {
                _stateMachine.Update();
            }
        }     
        
        public void Configurate(StateMachineController stateMachine)
		{
            _stateMachine = stateMachine;
		}
    }
}
