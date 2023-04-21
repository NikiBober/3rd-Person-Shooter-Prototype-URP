using Opsive.UltimateCharacterController.Character.Abilities.AI;
using UnityEngine;

public class EnemyMovement : Enemy
{
    private Vector3 _destination;
    private NavMeshAgentMovement _navMeshAgentMovement;

    // Initializes the default values.
    protected override void Start()
    {
        base.Start();
        _navMeshAgentMovement = _characterLocomotion.GetAbility<NavMeshAgentMovement>();
    }

    // Enemy walks to the player`s position.
    private void Update()
    {
        _destination = _player.gameObject.transform.position;
        _navMeshAgentMovement.SetDestination(_destination);
    }
}
