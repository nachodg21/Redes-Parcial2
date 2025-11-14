using Fusion;
using Fusion.Addons.Physics;
using System.Collections;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    [SerializeField] NetworkRigidbody3D _netRb;
    [SerializeField] float _initialImpulse;

    [SerializeField] float _lifeTime;
    [SerializeField] byte _damage;

    TickTimer _lifeTimer;

    public override void Spawned()
    {
        _netRb.Rigidbody.AddForce(transform.right * _initialImpulse, ForceMode.VelocityChange);

        _lifeTimer = TickTimer.CreateFromSeconds(Runner, _lifeTime);
    }

    public override void FixedUpdateNetwork()
    {
        if (!_lifeTimer.ExpiredOrNotRunning(Runner)) return;

        DespawnObject();
    }

    void DespawnObject()
    {
        Runner.Despawn(Object);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!HasStateAuthority) return;

        if (other.gameObject.CompareTag("Cube"))
        {
            var cubeNO = other.gameObject.GetComponent<NetworkObject>();

            if (cubeNO != null)
            {
                if (Object.HasStateAuthority)
                {
                    Runner.Despawn(cubeNO);
                }
            }
        }

        if (other.TryGetComponent(out EnemyLife enemyHealth))
        {
            enemyHealth.TakeDamage(_damage);
        }

        DespawnObject();
    }
}
