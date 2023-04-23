using UnityEngine;

/// <summary>
/// Define behavior on player's death.
/// </summary>
public class PlayerDeathDetector : DeathDetector
{
    protected override void OnDeath(Vector3 position, Vector3 force, GameObject attacker)
    {
        Player.Instance.IsAlive = false;
        GameManager.Instance.GameOver();
    }
}
