using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ToonyTowers
{
    public class NpcSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _npcPrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _spawnIntervalMin = 1f;
        [SerializeField] private float _spawnIntervalMax = 10f;
        [SerializeField] private float _spawnRadius = 5f;
        [SerializeField] private int _maxNpcs = 10;

        private float _spawnInterval;
        private float _spawnTimer;
        private int _npcCount;

        private void Awake()
        {
            _spawnInterval = _spawnIntervalMin;
        }

        private void Update()
        {
            if (_npcCount >= _maxNpcs) return;
            
            _spawnTimer -= Time.deltaTime;
            if (!(_spawnTimer <= 0f)) return;
            
            SpawnNpc();
            _spawnTimer = _spawnInterval;
        }

        private void SpawnNpc()
        {
            var randomPoint = Random.insideUnitCircle * _spawnRadius;
            var spawnPosition = _spawnPoint.position + new Vector3(randomPoint.x, 0f, randomPoint.y);
            Instantiate(_npcPrefab, spawnPosition, Quaternion.identity);
            _spawnInterval = Random.Range(_spawnIntervalMin, _spawnIntervalMax);
            _npcCount++;
        }
    }
}
