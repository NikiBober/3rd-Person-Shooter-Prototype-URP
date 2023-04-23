using Opsive.UltimateCharacterController.Character.Abilities.AI;
using UnityEngine;

/// <summary>
/// Enemy constantly moving to the player, and stop when player die.
/// </summary>
public class EnemyMovement : Enemy
{
    private Vector3 _destination;
    private NavMeshAgentMovement _navMeshAgentMovement;

    protected override void Start()
    {
        base.Start();
        _navMeshAgentMovement = _characterLocomotion.GetAbility<NavMeshAgentMovement>();
    }

    // For movement used character's ability.
    private void Update()
    {
        if (!_player.IsAlive)
        {
            _navMeshAgentMovement.Enabled = false;
            return;
        }

        _destination = _player.gameObject.transform.position;
        _navMeshAgentMovement.SetDestination(_destination);
    }
}
