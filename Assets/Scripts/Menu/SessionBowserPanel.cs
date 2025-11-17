using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SessionBowserPanel : MonoBehaviour
{
    NetworkRunnerHandler _handler;

    [SerializeField] SessionItem _sessionItemPrefab;
    [SerializeField] VerticalLayoutGroup _contentLayout;
    [SerializeField] TMP_Text _statusText;

    [SerializeField] Button _hostPanelButton;
    [SerializeField] GameObject _hostPanel;

    private void Awake()
    {
        gameObject.SetActive(false);

        var provider = GetComponentInParent<IRunnerHandlerProvider>();

        if (provider == null) return;

        provider.OnHandlerUpdated += GetHandler;

        _hostPanelButton.onClick.AddListener(ShowHostPanel);
    }

    void ShowHostPanel()
    {
        gameObject.SetActive(false);
        _hostPanel.SetActive(true);
    }

    void GetHandler(NetworkRunnerHandler handler)
    {
        _handler = handler;
    }

    private void OnEnable()
    {
        if (!_handler) return;

        _handler.OnSessionListUpdate += GetSessions;
    }

    private void OnDisable()
    {
        if (!_handler) return;

        _handler.OnSessionListUpdate -= GetSessions;
    }

    void GetSessions(List<SessionInfo> allSessions)
    {
        ClearContent();

        if (allSessions.Count == 0)
        {
            _statusText.gameObject.SetActive(true);
        }
        else
        {
            foreach (SessionInfo session in allSessions)
            {
                AddNewSessionItem(session);
            }

            _statusText.gameObject.SetActive(false);
        }
    }

    void ClearContent()
    {
        foreach (Transform contentChild in _contentLayout.transform)
        {
            Destroy(contentChild.gameObject);
        }
    }

    void AddNewSessionItem(SessionInfo session)
    {
        var sessionItem = Instantiate(_sessionItemPrefab, _contentLayout.transform);
        sessionItem.SetInfo(session);
        sessionItem.OnSessionSelected += JointToSession;
    }

    void JointToSession(SessionInfo sessionInfo)
    {
        _handler.JoinGame(sessionInfo);
    }
}
