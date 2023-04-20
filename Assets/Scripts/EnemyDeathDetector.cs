using UnityEngine;

public class EnemyDeathDetector : DeathDetector
{
    protected override void OnDeath(Vector3 position, Vector3 force, GameObject attacker)
    {
        Debug.Log("The object died");
        gameObject.GetComponent<PooledObject>().Deactivate();
    }


}
