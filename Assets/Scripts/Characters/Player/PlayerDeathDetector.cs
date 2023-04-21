using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathDetector : DeathDetector
{
    protected override void OnDeath(Vector3 position, Vector3 force, GameObject attacker)
    {
        Player.Instance.IsPlayerAlive = false;
        GameManager.Instance.GameOver();
    }
}
