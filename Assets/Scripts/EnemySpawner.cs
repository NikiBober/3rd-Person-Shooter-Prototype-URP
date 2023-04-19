using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Prefab of the enemy to spawn.")]
    [SerializeField] private PooledObject _enemyPrefab;
    [Tooltip("Radius of the spawn area.")]
    [SerializeField] private float _spawnAreaRadius = 13.0f;
    [Tooltip("Initial delay between spawns.")]
    [SerializeField] private float _initialSpawnDelay = 5.0f;
    [Tooltip("Minimum delay between spawns.")]
    [SerializeField] private float _minSpawnDelay = 1.0f;
    [Tooltip("Rate at which spawn delay decreases.")]
    [SerializeField] private float _spawnDelayDecreaseRate = 0.1f;

    [Header("Object Pool")]
    [Tooltip("Throw an exception if we try to return an existing item, already in the pool.")]
    [SerializeField] private bool _collectionCheck = true;
    [Tooltip("Initial pool capacity.")]
    [SerializeField] private int _defaultCapacity = 20;
    [Tooltip("When pool reaches max size, objects released to pool will be destroyed.")]
    [SerializeField] private int _maxSize = 100;

    private float _currentSpawnDelay;
    private IObjectPool<PooledObject> _objectPool;

    // Create an object pool for enemies.
    private void Awake()
    {
        _objectPool = new ObjectPool<PooledObject>(CreateObject, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, _collectionCheck, _defaultCapacity, _maxSize);
    }

    // Start the coroutine to spawn enemies.
    private void Start()
    {
        _currentSpawnDelay = _initialSpawnDelay;
        StartCoroutine(SpawnEnemiesRoutine());
    }

    private IEnumerator SpawnEnemiesRoutine()
    {
        while (true)
        {
            // Spawn an enemy from the object pool at a random position within the spawn area.
            Vector3 spawnPosition = Random.insideUnitSphere * _spawnAreaRadius;
            spawnPosition.y = 0;
            var enemy = _objectPool.Get();
            enemy.gameObject.transform.position = spawnPosition;

            // Decrease the current spawn delay.
            _currentSpawnDelay -= _spawnDelayDecreaseRate;

            // Limit the current spawn delay to the minimum value.
            _currentSpawnDelay = Mathf.Max(_currentSpawnDelay, _minSpawnDelay);

            // Wait for the current spawn delay before the next spawn.
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
        pooledObject.gameObject.SetActive(false);
    }

    // Invoked when retrieving the next item from the object pool.
    private void OnGetFromPool(PooledObject pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    // Invoked when we exceed the maximum number of pooled items.
    private void OnDestroyPooledObject(PooledObject pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }
    #endregion
}
