using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.FSM;

namespace Monotheist
{
    public class Individual: MonoBehaviour
    {
        StateMachineController _stateMachine;

        [SerializeField] private Transform _target;
        [SerializeField] private bool _bored;
        [SerializeField] private float _velocity;
        [SerializeField] private float _targetRange;

        public Transform Target => _target;
        public bool Bored => _bored;
        public float Velocity => _velocity;
        public float TargetRange => _targetRange;


        void Start()
        {
        }

        void Update()
        {
            if(_stateMachine != null)
            {
                _stateMachine.Update();
            }
        }     
        
        public void Configurate(StateMachineController stateMachine)
		{
            _stateMachine = stateMachine;
		}
    }
}
