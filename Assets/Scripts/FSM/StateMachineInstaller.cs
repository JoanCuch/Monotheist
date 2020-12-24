using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Monotheist;

namespace Monotheist.FSM
{
    public class StateMachineInstaller : MonoBehaviour
    {    
        public Human human;
        public StateMachineModel model;
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
            model = new StateMachineModel();
            
            model.idleState = new IdleState();
            model.walkState = new WalkState();
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

            human.Configurate(controller);

            controller.ChangeState(model.startState);
        }
    }
}
