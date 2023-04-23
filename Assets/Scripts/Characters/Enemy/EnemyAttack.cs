using Opsive.UltimateCharacterController.Character.Abilities.Items;
using Opsive.UltimateCharacterController.Utility;
using UnityEngine;

/// <summary>
/// Enemy attack player when close enough, and stop when player die.
/// </summary>
public class EnemyAttack : Enemy
{
    [Tooltip("Attack if the player is within the close distance.")]
    [SerializeField] private float _attackDistance = 1.5f;
    [Tooltip("The interval between attacks.")]
    [SerializeField] private MinMaxFloat _attackInterval = new MinMaxFloat(2, 4);

    private Use _useAbility;
    private float _nextAttackTime;

    protected override void Start()
    {
        base.Start();
        _useAbility = _characterLocomotion.GetAbility<Use>();
    }

    // Attack the target when within distance.
    private void Update()
    {
        if (!_player.IsAlive)
        {
            return;
        }

        // For better performance used sqrMagnitude instead of Vector3.Distance.
        var distance = (_player.transform.position - transform.position).sqrMagnitude;

        if (distance < _attackDistance * _attackDistance && _nextAttackTime < Time.time)
        {
            Attack();
        }
    }
    // Enemy use weapon from inventory.
    private void Attack()
    {
        _characterLocomotion.TryStartAbility(_useAbility);
        _nextAttackTime = Time.time + _attackInterval.RandomValue;
    }
}
