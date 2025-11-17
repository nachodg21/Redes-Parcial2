using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoiningPanel : MonoBehaviour
{

    [SerializeField] GameObject _sessionBrowserPanel;

    private void Awake()
    {
        gameObject.SetActive(false);

        var provider = GetComponentInParent<IRunnerHandlerProvider>();

        if (provider == null) return;

        provider.OnHandlerUpdated += GetHandler;
    }

    void GetHandler(NetworkRunnerHandler handler)
    {
        handler.OnJoinedLobby += LobbyJoined;
    }

    void LobbyJoined()
    {
        gameObject.SetActive(false);
        _sessionBrowserPanel.SetActive(true);
    }
}
