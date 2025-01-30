using System;
using UnityEngine;

public interface IInput
{
    public event Action<float, float> OnRotate;
    public event Action<Vector3> OnDirectionMove;
    public event Action OnLeftMouseClicked;
    public event Action OnGravityChange;
    public event Action OnSpaceClicked;
    public event Action OnPause;
}
