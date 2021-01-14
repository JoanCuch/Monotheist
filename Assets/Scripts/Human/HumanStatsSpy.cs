using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.Human;
using Monotheist.FSM;


namespace Monotheist
{
    public class HumanStatsSpy : MonoBehaviour
    {
        public List<NeedStats> needsList;

        public GoalState currentGoalState;
        public ActionState currentActionState;


        void Start()
        {
        }

        void Update()
        {
        }
        
        public void Configurate(HumanNeeds humanNeeds, StateMachineController stateMachineController)
		{
            stateMachineController.CurrentStateProperty.Subscribe(ChangeGoalState);
            
            foreach (Need need in humanNeeds.NeedList)
            {
                NeedStats stats = new NeedStats();

                stats.tag = need.Tag;
                stats.satisfaction = 0;
                stats.currentState = NeedStates.lethal;
                stats.listCount = 0;
                stats.listState = NeedItemStates.empty;

                need.SatisfactionProperty.Subscribe((float value) => stats.satisfaction = value);
                need.CurrentStateProperty.Subscribe((NeedStates value) => stats.currentState = value);
                need.ItemsListCount.Subscribe((int value) => stats.listCount = value);
                need.ListStateProperty.Subscribe((NeedItemStates value) => stats.listState = value);

                needsList.Add(stats);
            }           
        }
        
        public void ChangeGoalState(GoalState newGoal)
		{
            if (currentGoalState != null)
            {
                currentGoalState.UnsubscribeActionChange(UpdateAction);
            }

            currentGoalState = newGoal;

            if (currentGoalState != null)
            {
                currentGoalState.SubscribeActionChange(UpdateAction);
            }
		}

        public void UpdateAction(ActionState action)
		{
            currentActionState = action;
		}
    }

    [System.Serializable]
    public class NeedStats
    {
        public string tag;
        public float satisfaction;
        public NeedStates currentState;
        public float listCount;
        public NeedItemStates listState;
    }
}
