using System;
using UnityEngine;

public interface IReflectable
{
    public event Action OnReflected;

    public void ReflectionMove(Vector3 direction);
}