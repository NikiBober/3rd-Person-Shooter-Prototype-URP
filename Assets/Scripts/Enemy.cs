using Opsive.UltimateCharacterController.Character;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected PlayerSingleton _player;
    protected UltimateCharacterLocomotion _characterLocomotion;

    // Initializes the default values.
    protected virtual void Start()
    {
        _characterLocomotion = GetComponent<UltimateCharacterLocomotion>();
        _player = PlayerSingleton.Instance;
    }
}
