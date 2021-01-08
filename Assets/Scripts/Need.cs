using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Monotheist.Human
{
    public class Need
    {        
        private NeedConfig _config;

        private float _satisfaction;
        private NeedStates _currentState;
        private NeedStates _lastState;
        private Action<Need> _stateChanged;

        private List<Interactable> _itemsList;
        private NeedItemStates _currentListState;
        private NeedItemStates _lastListState;

        public float Satisfaction => _satisfaction;
        public NeedStates CurrentState => _currentState;
        public NeedStates LastState => _lastState;
        public NeedItemStates CurrentItemListState => _currentListState;
        public NeedItemStates LastItemListState => _lastListState;
        public NeedConfig NeedConfig => _config;
        public string Tag => _config.tag;

        public Need(NeedConfig config)
		{
            _config = config;

            _satisfaction = _config.satisfactionInitial;
            CheckState();

            _itemsList = new List<Interactable>();
            CheckItemListState();    
		}

        public void Update()
		{
            if (_satisfaction > _config.satisfactionMin)
            {
                _satisfaction += -1 * _config.satisfactionReductionPerSecond * Time.deltaTime;
            }
            CheckState();


            Debug.LogWarning(_config.tag + ": " + _satisfaction + " " + _currentState);
            Debug.LogWarning(_config.tag + ": " + _itemsList.Count+ " " + _currentListState);



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
            _lastState = _currentState;
            _currentState = newState;
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
            _lastListState = _currentListState;
            _currentListState = newState;
            if(_stateChanged != null)
                _stateChanged.Invoke(this);
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

        public void Subscribe(Action<Need> action)
		{
            _stateChanged += action;
		}
    }
}
