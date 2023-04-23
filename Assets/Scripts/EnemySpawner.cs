using System.Collections;
using Opsive.Shared.Events;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// Spawn enemies using Unity buil in pool with decreasing intervals on random position inside defined circle.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Prefab of the enemy to spawn.")]
    [SerializeField] private PooledObject _enemyPrefab;
    [Tooltip("Radius of the spawn area.")]
    [SerializeField] private float _spawnAreaRadius = 13f;
    [Tooltip("Initial delay between spawns.")]
    [SerializeField] private float _initialSpawnDelay = 5f;
    [Tooltip("Minimum delay between spawns.")]
    [SerializeField] private float _minSpawnDelay = 1f;
    [Tooltip("Rate at which spawn delay decreases.")]
    [SerializeField] private float _spawnDelayDecreaseRate = 0.1f;
    [Tooltip("How many enmies can be actve at the same time.")]
    [SerializeField] private int _maxEnemiesNumber = 20;

    [Header("Object Pool")]
    [Tooltip("Throw an exception if we try to return an existing item, already in the pool.")]
    [SerializeField] private bool _collectionCheck = true;
    [Tooltip("Initial pool capacity.")]
    [SerializeField] private int _defaultCapacity = 10;
    [Tooltip("When pool reaches max size, objects released to pool will be destroyed.")]
    [SerializeField] private int _maxSize = 30;

    private float _currentSpawnDelay;
    private IObjectPool<PooledObject> _objectPool;
    private int _activeObjects;

    private void Awake()
    {
        _objectPool = new ObjectPool<PooledObject>(CreateObject, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, _collectionCheck, _defaultCapacity, _maxSize);
    }

    private void Start()
    {
        _currentSpawnDelay = _initialSpawnDelay;
        StartCoroutine(SpawnEnemiesRoutine());
    }

    // Spawn an enemy from the object pool at a random position within the spawn area.
    private IEnumerator SpawnEnemiesRoutine()
    {
        while (Player.Instance.IsAlive)
        {
            if (_activeObjects < _maxEnemiesNumber)
            {
                _objectPool.Get();

                _currentSpawnDelay -= _spawnDelayDecreaseRate;

                // Limit the current spawn delay to the minimum value.
                _currentSpawnDelay = Mathf.Max(_currentSpawnDelay, _minSpawnDelay);
            }

            yield return new WaitForSeconds(_currentSpawnDelay);
        }
    }

    #region ObjectPool
    // Invoked when creating an item to populate the object pool.
    private PooledObject CreateObject()
    {
        PooledObject objectInstance = Instantiate(_enemyPrefab);
        objectInstance.ObjectPool = _objectPool;
        return objectInstance;
    }

    // Invoked when returning an item to the object pool.
    private void OnReleaseToPool(PooledObject pooledObject)
    {
        _activeObjects--;
        pooledObject.gameObject.SetActive(false);
    }

    // Invoked when retrieving the next item from the object pool.
    private void OnGetFromPool(PooledObject pooledObject)
    {
        _activeObjects++;

        // Set new random position.
        Vector3 spawnPosition = Random.insideUnitSphere * _spawnAreaRadius;
        spawnPosition.y = 0;
        pooledObject.gameObject.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);

        pooledObject.gameObject.SetActive(true);
        // Reset character abilities.
        EventHandler.ExecuteEvent(pooledObject.gameObject, "OnWillRespawn");
        EventHandler.ExecuteEvent(pooledObject.gameObject, "OnRespawn");

    }

    // Invoked when we exceed the maximum number of pooled items.
    private void OnDestroyPooledObject(PooledObject pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }
    #endregion
}
