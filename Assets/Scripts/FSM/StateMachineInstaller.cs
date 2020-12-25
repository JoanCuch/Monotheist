using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Monotheist;

namespace Monotheist.FSM
{
    public class StateMachineInstaller : MonoBehaviour
    {    
        public HumanNecessities human;
        public StateMachineModel model;
        public HumanConfig humanConfig;

        void Start()
        {
            Install();
        }

        void Update()
        {

        }

        private void Install()
        {
            model = new StateMachineModel();
            
            model.idleState = new IdleState();
            model.walkState = new WalkState(humanConfig);
            model.interactState = new InteractState();
            model.startState = new StartState();
            model.owner = human;

            List<State> stateList = new List<State> {
                model.idleState,
                model.walkState,
                model.interactState,
                model.startState
            };

            StateMachineController controller = new StateMachineController();

            foreach (State state in stateList)
			{
                state.SetModel(model);
                state.Subscribe(controller.ChangeState);
			}

            human.Configurate(controller, humanConfig);

            controller.ChangeState(model.startState);
        }
    }
}
