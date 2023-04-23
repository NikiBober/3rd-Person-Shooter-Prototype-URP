using Opsive.UltimateCharacterController.Character;
using UnityEngine;

/// <summary>
/// Parent class with references for enemy.
/// </summary>
public class Enemy : MonoBehaviour
{
    protected Player _player;
    protected UltimateCharacterLocomotion _characterLocomotion;

    protected virtual void Start()
    {
        _characterLocomotion = GetComponent<UltimateCharacterLocomotion>();
        _player = Player.Instance;
    }
}
