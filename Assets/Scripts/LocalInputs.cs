using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalInputs : NetworkBehaviour
{
    private bool _jumpButton;
    private bool _fireButton;

    NetworkInputData _data;

    public static LocalInputs Instance { get; private set; }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Instance = this;
            _data = new NetworkInputData();
        }
        else
        {
            enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _jumpButton = true;
        }

        //_jumpButton = _jumpButton | Input.GetKeyDown(KeyCode.W);
        _fireButton = _fireButton | Input.GetKeyDown(KeyCode.Space);
    }

    public NetworkInputData UpdateInputs()
    {
        _data.xAxi = Input.GetAxis("Horizontal");

        _data.buttons.Set(PlayerButtons.Jump, _jumpButton);
        _jumpButton = false;

        _data.buttons.Set(PlayerButtons.Shot, _fireButton);
        _fireButton = false;

        return _data;
    }
}
