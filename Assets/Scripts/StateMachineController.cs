using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Monotheist.FSM
{
    public class StateMachineController
    {        
        State currentState;

        public StateMachineController()
		{
		}

        public void ChangeState(State newState)
		{
            Debug.Log("Change state: " + newState);
            if(currentState != null)
			{
                currentState.Exit();
			}

            currentState = newState;

            if (currentState != null)
            {
                currentState.Enter();
            }
		}

        public void Update()
		{
            if (currentState != null) currentState.Execute();
		}
    }
}
