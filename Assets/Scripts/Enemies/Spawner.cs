using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : ObjectPool
{
    [SerializeField] private GameObject[] _enemyTemplates;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Player _player;
    [SerializeField] private EffectsPool _spawnEffects;
    [SerializeField] private float _secondsToSpawn;
    [SerializeField] private float _spawnRange;

    private float _elapsedTime;
    private int _enemiesCount = 0;
    private int _waveCount = 0;
    private bool _allEnemiesDead = true;
    private List<int> _spawnPointsCounter = new List<int>();

    public event UnityAction<int> EnemiesCountChanged;
    public event UnityAction<int> WaveChanged;
    public event UnityAction EnemyDied;

    private void OnDisable()
    {
        for (int i = 0; i < PrefabsCount; i++)
        {
            GetPrefab(i).GetComponent<Enemy>().Died -= OnEnemyDied;
        }
    }

    private void Start()
    {
        Initialize(_enemyTemplates, _player);
    }

    private void Update()
    {
        if (_allEnemiesDead)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _secondsToSpawn)
            {
                SpawnEnemies();
                _elapsedTime = 0;
            }
        }
    }

    private void SpawnEnemies()
    {
        _enemiesCount = 0;

        for (int i = 0; i < PrefabsCount; i++)
        {
            if (TryGetObject(out GameObject enemy))
            {
                int spawnPoint = Random.Range(0, _spawnPoints.Length);

                SpawnPoint spawnPointObject = _spawnPoints[spawnPoint].GetComponent<SpawnPoint>();

                if (spawnPointObject.HasSpawned == false)
                {
                    spawnPointObject.SpawnedObject();
                }

                SetEnemy(enemy, _spawnPoints[spawnPoint]);

                enemy.GetComponent<Enemy>().Died += OnEnemyDied;

                _enemiesCount++;
            }
        }

        CreateEffects();

        _spawnPointsCounter.Clear();

        _waveCount++;
        WaveChanged?.Invoke(_waveCount);

        EnemiesCountChanged?.Invoke(_enemiesCount);
        _allEnemiesDead = false;
    }

    private void CreateEffects()
    {
        foreach (var spawnpoint in _spawnPoints)
        {
            if (spawnpoint.GetComponent<SpawnPoint>().HasSpawned == true)
            {
                _spawnEffects.SetEffect(spawnpoint);
                spawnpoint.GetComponent<SpawnPoint>().Reset();
            }
        }
    }

    private void SetEnemy(GameObject enemy, Transform spawnPoint)
    {
        float randomX = Random.Range(-_spawnRange, _spawnRange);
        float randomZ = Random.Range(-_spawnRange, _spawnRange);
        
        enemy.transform.position = new Vector3(spawnPoint.position.x + randomX, transform.position.y, spawnPoint.position.z + randomZ);

        enemy.SetActive(true);
    }

    private void OnEnemyDied(int reward, GameObject enemy)
    {
        enemy.GetComponent<Enemy>().Died -= OnEnemyDied;

        _enemiesCount = 0;

        _player.OnEnemyDied(reward);

        for (int i = 0; i < PrefabsCount; i++)
        {
            if (GetPrefab(i).activeSelf == true)
            {
                _enemiesCount++;
            }
        }

        EnemiesCountChanged?.Invoke(_enemiesCount);

        for (int i = 0; i < PrefabsCount; i++)
        {
            if (GetPrefab(i).activeSelf == true)
            {
                return;
            }
        }

        _allEnemiesDead = true;

        AddPrefabs(_enemyTemplates, _player, 5);
    }
}
