using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist.Human
{
    public class Need
    {        
        private NeedConfig _config;

        private float _satisfaction;
        private ReactiveProperty<NeedStates> _currentState;
        private NeedStates _lastState;

        private List<Interactable> _itemsList;
        private ReactiveProperty<NeedItemStates> _currentListState;
        private NeedItemStates _lastListState;


        public ReactiveProperty<NeedStates> CurrentState => _currentState;
        public NeedStates LastState => _lastState;
        public ReactiveProperty<NeedItemStates> CurrentItemListState => _currentListState;
        public NeedItemStates LastItemListState => _lastListState;

        public void Install(NeedConfig config)
		{
            _config = config;

            _satisfaction = _config.satisfactionInitial;
            CheckState();

            _itemsList = new List<Interactable>();
            CheckItemListState();    
		}

        public void Update()
		{
            _satisfaction += -1 * _config.satisfactionReductionPerSecond;
            CheckState();
		}

        private void CheckState()
		{
            if (_satisfaction >= _config.satisfactionUnsatisfiedLevel)
            {
                ChangeState(NeedStates.satisfied);
            }
            else if (_satisfaction >= _config.satisfactionCriticLevel)
			{
                ChangeState(NeedStates.unsatisfied);
			}
            else if(_satisfaction > _config.satisfactionLethalLevel)
			{
                ChangeState(NeedStates.critic);
			}
			else
			{
                ChangeState(NeedStates.lethal);
			}
		}
        
        private void ChangeState(NeedStates newState)
		{
            _lastState = _currentState.Value;
            _currentState.Value = newState;
		}  
     
        private void CheckItemListState()
		{
            if(_itemsList.Count > _config.itemListUnsatisfiedLevel)
			{
                ChangeItemListState(NeedItemStates.satisfied);
			}
            else if(_itemsList.Count > 0)
			{
                ChangeItemListState(NeedItemStates.unsatisfied);
			}
			else
			{
                ChangeItemListState(NeedItemStates.empty);
			}
		}

        private void ChangeItemListState(NeedItemStates newState)
		{
            _lastListState = _currentListState.Value;
            _currentListState.Value = newState;
		}

        public bool AddItem(Interactable item)
        { 
            if(_itemsList.Count == _config.itemListMax)
			{
                return false;
			}
			else
			{
                _itemsList.Add(item);
                CheckItemListState();
                return true;
			}
        }

        public List<Interactable> GetItemsList()
		{
            return _itemsList;
		}
    }
}
