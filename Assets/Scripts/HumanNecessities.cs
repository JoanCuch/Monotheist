using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monotheist.FSM;

namespace Monotheist
{
    public class HumanNecessities: MonoBehaviour
    {
        private StateMachineController _stateMachine;
        private HumanConfig _config;

        private Transform _target;
		[SerializeField, Range(0, 100)] public float _currentEnergy;
        [SerializeField, Range(0, 100)] public float _currentSatiation;
        private HumanConfig.Necessities _currentNecessity;
        
        public Transform Target => _target;

        void Start()
        {
            _currentEnergy = _config.initialEnergy;
            _currentSatiation = _config.initialSatiation;
            _currentNecessity = HumanConfig.Necessities.fullfilled;
        }

        void Update()
		{
			UpdateNecessities();

			if (_currentEnergy == 0 || _currentSatiation == 0)
			{
				_currentNecessity = HumanConfig.Necessities.dead;			
			}
			else if (_currentNecessity != HumanConfig.Necessities.fullfilled)
			{
				CheckFullfilledNecessity();
			}
			else
			{
				CheckNewNecessity();
			}

			if (_stateMachine != null)
			{
				_stateMachine.Update();
			}
		}

		private void CheckNewNecessity()
		{		
			if (_currentEnergy <= _config.energyTiredLimit)
			{
				_currentNecessity = HumanConfig.Necessities.energy;
			}
			else if (_currentSatiation <= _config.satiationHungryLimit)
			{
				_currentNecessity = HumanConfig.Necessities.satiation;
			}
		}

		private void CheckFullfilledNecessity()
		{
			switch (_currentNecessity)
			{
				case HumanConfig.Necessities.energy:
					if (_currentEnergy >= _config.energyAwakeLimit)
					{
						_currentNecessity = HumanConfig.Necessities.fullfilled;
						_target = null;
					}
					break;

				case HumanConfig.Necessities.satiation:
					if (_currentSatiation >= _config.satiationSatiatedLimit)
					{
						_currentNecessity = HumanConfig.Necessities.fullfilled;
						_target = null;
					}
					break;
			}
		}

		private void UpdateNecessities()
		{
			AddEnergy(-1 * _config.energyReductionPerSecond * Time.deltaTime);
            AddSatiation(-1 * _config.satiationReductionPerSecond * Time.deltaTime);
		}

		public void Configurate(StateMachineController stateMachine, HumanConfig config)
		{
            _stateMachine = stateMachine;
            _config = config;
		}

        public void AddEnergy(float extraEnergy)
        {
            _currentEnergy += extraEnergy;
            _currentEnergy = Mathf.Clamp(_currentEnergy, 0, 100);
        }

        public void AddSatiation(float extraSatiation)
		{
            _currentSatiation += extraSatiation;
            _currentSatiation = Mathf.Clamp(_currentSatiation, 0, 100);
		}

        public HumanConfig.Necessities GetCurrentNecessity()
		{
            return _currentNecessity;
		}

        public bool SearchTarget(string tag)
		{
			_target = null;
			
			Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _config.searchRange);

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
                    Debug.Log("New target adquired: " + _target.name);
                    return true;
				}
			}
            return false;      
		}
    }
}
