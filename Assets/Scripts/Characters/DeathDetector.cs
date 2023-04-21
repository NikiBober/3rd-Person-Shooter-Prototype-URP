using Opsive.Shared.Events;
using UnityEngine;

public abstract class DeathDetector : MonoBehaviour
{
    protected void Awake()
    {
        EventHandler.RegisterEvent<Vector3, Vector3, GameObject>(gameObject, "OnDeath", OnDeath);
    }

    protected abstract void OnDeath(Vector3 position, Vector3 force, GameObject attacker);
    
}
