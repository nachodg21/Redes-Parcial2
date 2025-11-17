using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerPresenter : MonoBehaviour, IRunnerHandlerProvider
{
    public event Action<NetworkRunnerHandler> OnHandlerUpdated;

    public void SetRunner(NetworkRunnerHandler handler)
    {
        OnHandlerUpdated?.Invoke(handler);
    }
}
