using Opsive.Shared.Events;
using UnityEngine;

public class EnemyDeathDetector : MonoBehaviour
{
    private void Awake()
    {
        EventHandler.RegisterEvent<Vector3, Vector3, GameObject>(gameObject, "OnDeath", OnDeath);
    }

    private void OnDeath(Vector3 position, Vector3 force, GameObject attacker)
    {
        Debug.Log("The object died");
    }
}
