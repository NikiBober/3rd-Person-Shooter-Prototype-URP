using Opsive.Shared.Events;
using UnityEngine;

/// <summary>
/// Used OnImpact instead of OnDamage (that can be assigned in the inspector), cause OnIpmact has force direction, and OnDamage only force amount.
/// </summary>
public class EnemyParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _impactParticle;

    private void Awake()
    {
        EventHandler.RegisterEvent<float, Vector3, Vector3, GameObject, object, Collider>(gameObject, "OnObjectImpact", OnImpact);
    }

    private void OnImpact(float amount, Vector3 position, Vector3 forceDirection, GameObject attacker, object attackerObject, Collider hitCollider)
    {
        _impactParticle.gameObject.transform.SetPositionAndRotation(position, Quaternion.LookRotation(forceDirection, Vector3.up));
        _impactParticle.Play();
    }
}
