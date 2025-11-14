using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NicknameItem : MonoBehaviour
{
    [SerializeField] float _yOffset;
    [SerializeField] TMP_Text _textComponent;

    Transform _target;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void UpdateNickname(string newNick)
    {
        _textComponent.text = newNick;
    }

    public void UpdatePosition()
    {
        transform.position = _target.position + Vector3.up * _yOffset;
    }

}
