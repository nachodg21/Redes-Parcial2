using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class EnemyController : NetworkBehaviour
{
    EnemyMovement _movement;
    WeaponArm _weapon;

    [SerializeField] List<GameObject> _materials;

    Vector3 initialPost;


    private void Awake()
    {
        _movement = GetComponent<EnemyMovement>();
        _weapon = GetComponentInChildren<WeaponArm>();
        initialPost = transform.position;
    }

    public override void Spawned()
    {

        if (HasStateAuthority)
        {
            var healthSystem = GetComponent<EnemyLife>();

            healthSystem.OnDeadStateUpdate += (isDead) =>
            {
                enabled = !isDead;

                if (!isDead)
                {
                    _movement.Teleport(initialPost);
                    Debug.Log("volvio");
                }
            };
        }

        GameManager.Instance.AddPlayer(this);

    }

    public override void FixedUpdateNetwork()
    {
        if (!GetInput(out NetworkInputData inputData)) return;

        _movement.Move(Vector3.right * inputData.xAxi);

        if (inputData.buttons.IsSet(PlayerButtons.Jump))
        {
            _movement.Jump();
        }

        //if (inputData.buttons.IsSet(PlayerButtons.Shot))
        if (inputData.isFirePressed)
        {
            _weapon.Shoot();
            //_weapon.RaycastShot();
        }

        _weapon.RPC_Rotate(inputData.direction);
    }

    [Rpc]
    public void RPC_SetTeam(Color color)
    {
        foreach (var mat in _materials)
            mat.GetComponent<Renderer>().material.color = color;
        _weapon.SetColor(color);
    }
}
