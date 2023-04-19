using UnityEngine;
using UnityEngine.Pool;

public class PooledObject : MonoBehaviour
{
    private IObjectPool<PooledObject> _objectPool;
    // public property to give a reference to its ObjectPool
    public IObjectPool<PooledObject> ObjectPool
    {
        set => _objectPool = value;
    }
}
