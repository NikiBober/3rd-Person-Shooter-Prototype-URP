using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class PooledObject : MonoBehaviour
{
    [Tooltip("Deactivate after delay.")]
    [SerializeField] private float timeoutDelay = 5f;

    private IObjectPool<PooledObject> _objectPool;
    // public property to give a reference to its ObjectPool
    public IObjectPool<PooledObject> ObjectPool { set => _objectPool = value; }

    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(timeoutDelay));
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        // release object back to the pool
        _objectPool.Release(this);
    }

}
