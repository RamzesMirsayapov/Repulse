using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using YG;

public class PauseManager : IPauseHandler, IInitializable, IDisposable
{
    private readonly List<IPauseHandler> _handlers = 
        new List<IPauseHandler>();

    public bool IsPaused { get; private set; }

    public void Initialize()
    {
        YG2.onOpenInterAdv += Pause;
        YG2.onCloseInterAdv += UnPause;
    }

    public void Dispose()
    {
        YG2.onOpenInterAdv -= Pause;
        YG2.onCloseInterAdv -= UnPause;
    }

    public void Register(IPauseHandler handler)
    {
        _handlers.Add(handler);
    }

    public void UnRegister(IPauseHandler handler)
    {
        _handlers.Remove(handler);
    }

    public void SetPaused(bool isPaused)
    {
        IsPaused = isPaused;

        Time.timeScale = IsPaused ? 0f : 1f;

        AudioListener.pause = isPaused;

        foreach (var handler in _handlers)
        {
            handler.SetPaused(isPaused);
        }
    }

    private void Pause() =>
        SetPaused(true);

    private void UnPause() =>
        SetPaused(false);
}