using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SessionItem : MonoBehaviour
{
    [SerializeField] TMP_Text _name, _players;
    [SerializeField] Button _joinButton;

    public event Action<SessionInfo> OnSessionSelected;

    public void SetInfo(SessionInfo sessionInfo)
    {
        _name.text = sessionInfo.Name;
        _players.text = $"{sessionInfo.PlayerCount}/{sessionInfo.MaxPlayers}";
        _joinButton.interactable = sessionInfo.PlayerCount < sessionInfo.MaxPlayers;

        _joinButton.onClick.AddListener(() =>
        {
            OnSessionSelected?.Invoke(sessionInfo);
        });
    }
}
