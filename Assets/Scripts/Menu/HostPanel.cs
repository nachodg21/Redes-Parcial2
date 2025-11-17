using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HostPanel : MonoBehaviour
{
    NetworkRunnerHandler _handler;

    [SerializeField] TMP_InputField _nameField;
    [SerializeField] Button _hostButton;

    private void Awake()
    {
        gameObject.SetActive(false);

        var provider = GetComponentInParent<IRunnerHandlerProvider>();

        if (provider == null) return;

        provider.OnHandlerUpdated += GetHandler;

        _hostButton.onClick.AddListener(HostGame);
    }

    void HostGame()
    {
        _handler.CreateGame(_nameField.text, "Gameplay");
        _hostButton.interactable = false;
    }

    void GetHandler(NetworkRunnerHandler handler)
    {
        _handler = handler;
    }
}
