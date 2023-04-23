using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// To give a reference to ObjectPool and return to pool after delay.
/// </summary>
public class PooledObject : MonoBehaviour
{
    [Tooltip("Deactivate after delay.")]
    [SerializeField] private float timeoutDelay = 5f;

    private IObjectPool<PooledObject> _objectPool;

    public IObjectPool<PooledObject> ObjectPool { set => _objectPool = value; }

    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(timeoutDelay));
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        _objectPool.Release(this);
    }
}
