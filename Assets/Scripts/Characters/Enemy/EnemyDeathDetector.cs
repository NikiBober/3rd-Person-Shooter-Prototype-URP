using UnityEngine;

public class EnemyDeathDetector : DeathDetector
{
    protected override void OnDeath(Vector3 position, Vector3 force, GameObject attacker)
    {
        gameObject.GetComponent<PooledObject>().Deactivate();
        GameManager.Instance.UpdateScore();
    }
}
