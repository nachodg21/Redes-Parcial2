using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public float xAxi;

    public NetworkButtons buttons;
}

public enum PlayerButtons
{
    Jump = 0,
    Shot = 1
}
