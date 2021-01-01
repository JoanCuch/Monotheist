using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.FSM;

namespace Monotheist.Human
{

    public class HumanManager : MonoBehaviour
    {
        StateMachineController _stateMachine;
        HumanNeeds _needs;
    
        void Start()
        {

        }

        void Update()
        {
            _needs.Update();
            _stateMachine.Update();        
        }

        public void Install(HumanConfig config)
		{
            _needs = new HumanNeeds(config);
            _stateMachine = new StateMachineController();
		}
    }
}
