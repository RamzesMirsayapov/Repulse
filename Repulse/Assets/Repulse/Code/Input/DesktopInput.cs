using System;
using UnityEngine;
using Zenject;

public class DesktopInput : IInput, ITickable
{
    public event Action<float, float> OnRotate;  // �����
    public event Action<Vector3> OnDirectionMove;
    public event Action OnGravityChange;
    public event Action OnSpaceClicked;
    public event Action OnLeftMouseClicked;

    private float horizontal;
    private float vertical;
    
    private float inputX;
    private float inputY;

    public void Tick()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        inputX = Input.GetAxis("Mouse X");
        inputY = Input.GetAxis("Mouse Y");

        CheckGravitation();

        CheckMove(); // �� ����

        ProcessSpaceClick();
        ProcessMouseClick();
    }

    private void ProcessMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnLeftMouseClicked?.Invoke();
        }
    }

    private void ProcessSpaceClick()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpaceClicked?.Invoke();
        }
    }

    private void CheckMove()
    {
        OnDirectionMove?.Invoke(new Vector3(horizontal, 0f, vertical));
    }

    private void CheckRotate()
    {
        OnRotate?.Invoke(inputX, inputY);
    }

    private void CheckGravitation()
    {
        OnGravityChange?.Invoke();
    }
}
