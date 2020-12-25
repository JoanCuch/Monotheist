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
        [SerializeField] private float _searchRadius;
 
        [SerializeField, Range(0, 100)] private float _energy;
        [SerializeField, Range(0, 100)] private float _initialEnergy;
        [SerializeField] private float _energyReductionPerSecond;
        [SerializeField, Range(0, 100)] private float _energyTiredLimit;
        [SerializeField, Range(0, 100)] private float _energyAwakeLimit;

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
            return _energy <= _energyTiredLimit;
		}

        public bool IsAwake()
		{
            return _energy >= _energyAwakeLimit;
		}

        public bool SearchTarget(string tag)
		{
            //RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _searchRadius, Vector3.forward, Mathf.Infinity);
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _searchRadius);

            if(hits != null)
			{
                List<Transform> taggedTargets = new List<Transform>();

                foreach (Collider2D hit in hits)
                {                 
                    if(hit.transform.tag == tag)
					{
                        taggedTargets.Add(hit.transform);
					}
                }

                float nearestDistance = Mathf.Infinity;
                Transform nearestTarget = null;
                
                foreach(Transform target in taggedTargets)
				{
                    Vector2 diff = target.position - transform.position;
                    float targetDistance = diff.sqrMagnitude;
                    if(targetDistance < nearestDistance)
					{
                        nearestTarget = target;
                        nearestDistance = targetDistance;
					}
				}

                if(nearestTarget != null)
				{
                    _target = nearestTarget;
                    Debug.Log("new target: " + _target);
                    return true;
				}
			}
            return false;      
		}
    }
}
