using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Assertions;

namespace Monotheist.Human
{
    public class Need
    {        
        private NeedConfig _config;

        private ReactiveProperty<float> _satisfaction;
        private ReactiveProperty<NeedStates> _currentState;
        private NeedStates _lastState;
        private Action<Need> _stateChanged;

        private ReactiveProperty<List<Interactable>> _itemsList;
        private ReactiveProperty<int> _listCount;
        private ReactiveProperty<NeedItemStates> _currentListState;
        private NeedItemStates _lastListState;


        public ReactiveProperty<float> SatisfactionProperty => _satisfaction;
        public ReactiveProperty<NeedStates> CurrentStateProperty => _currentState;
        public ReactiveProperty<int> ItemsListCount => _listCount;
        public ReactiveProperty<NeedItemStates> ListStateProperty => _currentListState;


        public float Satisfaction => _satisfaction.Value;
        public NeedStates CurrentState => _currentState.Value;
        public NeedStates LastState => _lastState;
        public NeedItemStates CurrentItemListState => _currentListState.Value;
        public NeedItemStates LastItemListState => _lastListState;
        public NeedConfig NeedConfig => _config;
        public string Tag => _config.tag;

        public Need(NeedConfig config)
		{
            _config = config;
            Assert.IsNotNull(_config);
            _satisfaction = new ReactiveProperty<float>(_config.satisfactionInitial);
            _currentState = new ReactiveProperty<NeedStates>();

            CheckState();

            _currentListState = new ReactiveProperty<NeedItemStates>();
            _itemsList = new ReactiveProperty<List<Interactable>>(new List<Interactable>());
            _listCount = new ReactiveProperty<int>(_itemsList.Value.Count);

            CheckItemListState();

            
		}

        public void Update()
		{
            if (_satisfaction.Value > _config.satisfactionMin)
            {
                _satisfaction.Value += -1 * _config.satisfactionReductionPerSecond * Time.deltaTime;
            }
            CheckState();
        }

        private void CheckState()
		{
            if(_satisfaction.Value >= _config.satisfactionFullfilledLevel)
			{
                ChangeState(NeedStates.fullfilled);
			}
            else if (_satisfaction.Value >= _config.satisfactionUnsatisfiedLevel)
            {
                ChangeState(NeedStates.satisfied);
            }
            else if (_satisfaction.Value >= _config.satisfactionCriticLevel)
			{
                ChangeState(NeedStates.unsatisfied);
			}
            else if(_satisfaction.Value > _config.satisfactionLethalLevel)
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
            if(_itemsList.Value.Count > _config.itemListUnsatisfiedLevel)
			{
                ChangeItemListState(NeedItemStates.satisfied);
			}
            else if(_itemsList.Value.Count > 0)
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
            _stateChanged?.Invoke(this);
		}

        public bool AddItem(Interactable item)
        { 
            if(_itemsList.Value.Count == _config.itemListMax)
			{
                return false;
			}
			
            _itemsList.Value.Add(item);
            item.SubscribeOnDestroy(RemoveItem);
            _listCount.Value = _itemsList.Value.Count;
            CheckItemListState();
            return true;
		
        }

        public void RemoveItem(Interactable item)
		{
            _itemsList.Value.Remove(item);
            _listCount.Value = _itemsList.Value.Count;
            CheckItemListState();
        }

        public void AddSatisfaction(float extraSatisfaction)
		{
            _satisfaction.Value = Mathf.Clamp(_satisfaction.Value + extraSatisfaction, _config.satisfactionMin, _config.satisfactionMax);         
		}

        public List<Interactable> GetItemsList()
		{
            return _itemsList.Value;
		}

        public void Subscribe(Action<Need> action)
		{
            _stateChanged += action;            
		}      
    }
}
