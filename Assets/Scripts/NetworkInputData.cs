using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public float xAxi;
    public Vector2 direction;

    public NetworkBool isFirePressed;
    public NetworkButtons buttons;
}

public enum PlayerButtons
{
    Jump = 0,
    Shot = 1,
    ShotUp = 2
}
