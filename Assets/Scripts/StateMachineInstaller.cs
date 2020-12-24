using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Monotheist;

namespace Monotheist.FSM
{
    public class StateMachineInstaller : MonoBehaviour
    {    
        public HumanBehavior human;

        // Start is called before the first frame update
        void Start()
        {
            Install();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void Install()
		{             
            StateMachineModel model = new StateMachineModel();
            model.walkState = new WalkState(model);
            model.interactState = new InteractState(model);
            model.idleState = new IdleState(model);
            model.startState = new StartState(model);

            StateMachineController controller = new StateMachineController();

            model.startState.Subscribe(controller.ChangeState);
            model.idleState.Subscribe(controller.ChangeState);
            model.interactState.Subscribe(controller.ChangeState);
            model.walkState.Subscribe(controller.ChangeState);

            human.Configurate(controller);

            controller.ChangeState(model.startState);

        }


    }
}
