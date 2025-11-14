using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNickname : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(NicknameChanged))]
    NetworkString<_16> CurrentNickname { get; set; }

    public event Action OnLeft;

    NicknameItem _nicknameItem;

    public override void Spawned()
    {
        _nicknameItem = NicknamesHandler.Instance.CreateNicknameItem(this);

        if (HasInputAuthority)
        {
            NetworkString<_16> _loadedNickname;

            if (PlayerPrefs.HasKey("Nickname"))
            {
                _loadedNickname = PlayerPrefs.GetString("Nickname");
            }
            else
            {
                _loadedNickname = "Unknown Player";
            }

            RPC_LoadNickname(_loadedNickname);
        }
        else
        {
            NicknameChanged();
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    void RPC_LoadNickname(NetworkString<_16> loadedNickname)
    {
        CurrentNickname = loadedNickname;
    }

    void NicknameChanged()
    {
        _nicknameItem.UpdateNickname(CurrentNickname.Value);
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        OnLeft?.Invoke();
    }
}
