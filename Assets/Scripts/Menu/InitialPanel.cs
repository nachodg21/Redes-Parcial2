using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitialPanel : MonoBehaviour
{

    [SerializeField] Button _joinLobbyButton;
    [SerializeField] TMP_InputField _nicknameField;

    [SerializeField] GameObject _connectingPanel;

    NetworkRunnerHandler _handler;

    private void Awake()
    {
        var provider = GetComponentInParent<IRunnerHandlerProvider>();

        if (provider == null) return;

        provider.OnHandlerUpdated += GetHandler;

        _joinLobbyButton.onClick.AddListener(JoinLobby);
    }

    void GetHandler(NetworkRunnerHandler handler)
    {
        _handler = handler;
    }

    void JoinLobby()
    {
        PlayerPrefs.SetString("Nickname", _nicknameField.text);

        gameObject.SetActive(false);
        _connectingPanel.SetActive(true);
        _handler.JoinLobby();
    }
}
