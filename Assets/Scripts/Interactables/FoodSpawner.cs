using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist
{
    public class FoodSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _foodParent;
        [SerializeField] private GameObject _foodPrefab;
        [SerializeField] private float _spawnsPerSecond;
        [SerializeField] private float _spawnRange;
        [SerializeField] private bool _spawnActivated;


        private float _timer;

        void Start()
        {
            _timer = _spawnsPerSecond;
            _spawnActivated = true;
        }

        void Update()
        {
            if (_spawnActivated)
            {
                _timer -= Time.deltaTime;

                if (_timer <= 0)
                {
                    _timer = _spawnsPerSecond;

                    float randomX = Random.Range(-1 * _spawnRange, _spawnRange);
                    float randomY = Random.Range(-1 * _spawnRange, _spawnRange);

                    Vector3 randomPosition = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z);

                    Instantiate(_foodPrefab, randomPosition, transform.rotation, _foodParent);
                }
            }
        }
    }
}
