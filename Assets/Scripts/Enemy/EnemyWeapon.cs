using System;
using System.Collections;
using Fusion;
using UnityEngine;

public class EnemyWeapon : NetworkBehaviour
{
    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] Transform _spawnPoint;

    public event Action OnShot;

    public void BulletShot()
    {
        if (!HasStateAuthority) return;

        Runner.Spawn(_bulletPrefab, _spawnPoint.position, _spawnPoint.rotation);
        OnShot?.Invoke();
    }

    public void RaycastShot()
    {
        OnShot?.Invoke();

        if (!HasStateAuthority) return;

        var raycastBool = Runner.LagCompensation.Raycast(origin: transform.position,
                                                        direction: transform.right,
                                                        length: 100,
                                                        player: Object.InputAuthority,
                                                        hit: out var rayHitInfo);

        if (raycastBool)
        {
            if (rayHitInfo.Hitbox.Root.TryGetComponent(out EnemyLife enemy))
            {
                enemy.TakeDamage(25);
            }
        }
    }
}
