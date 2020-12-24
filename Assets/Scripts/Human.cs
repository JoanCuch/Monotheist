using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.FSM;

namespace Monotheist
{
    public class Human: MonoBehaviour
    {
        StateMachineController _stateMachine;

        [SerializeField] private Transform _target;
        [SerializeField] private float _velocity;
        [SerializeField] private float _targetRange;
 
        [SerializeField, Range(0, 100)] private float _energy;
        [SerializeField, Range(0, 100)] private float _initialEnergy;
        [SerializeField] private float _energyReductionPerSecond;
        [SerializeField, Range(0, 100)] private float _energyLimit;

        public Transform Target => _target;
        public float Velocity => _velocity;
        public float TargetRange => _targetRange;

        void Start()
        {
            _energy = _initialEnergy;
        }

        void Update()
        {
            AddEnergy(-1 * _energyReductionPerSecond * Time.deltaTime);
                            
            if(_stateMachine != null)
            {
                _stateMachine.Update();
            }
        }     
        
        public void Configurate(StateMachineController stateMachine)
		{
            _stateMachine = stateMachine;
		}

        public void AddEnergy(float extraEnergy)
        {
            _energy += extraEnergy;
            _energy = Mathf.Clamp(_energy, 0, 100);
        }

        public bool IsTired()
		{
            return _energy <= _energyLimit;
		}

        public bool SearchTarget(string tag)
		{
            return true;
		}
    }
}
