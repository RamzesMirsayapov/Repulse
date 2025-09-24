using UnityEngine;
using Zenject;

public class PlayerRotation : MonoBehaviour, IPauseHandler
{
    [Header("Rotate Stats")]
    [SerializeField, Min(0f)] private float _sensitivity;
    [SerializeField, Min(0f)] private float _maxYAngle = 90f;

    private float _cameraVerticalRotation;

    private PauseManager _pausedManager;

    private IInput _input;

    private bool _isPaused;

    [Inject]
    private void Construct(IInput input, PauseManager pauseManager)
    {
        _input = input;
        _pausedManager = pauseManager;

        _input.OnRotate += Rotate;

        _pausedManager.Register(this);
    }

    private void OnDisable()
    {
        _input.OnRotate -= Rotate;
    }

    public void Rotate(float inputX, float inputY)
    {
        if (_isPaused)
            return;

        inputX *= _sensitivity;
        inputY *= _sensitivity;

        transform.parent.Rotate(Vector3.up * inputX);

        _cameraVerticalRotation -= inputY;
        _cameraVerticalRotation = Mathf.Clamp(_cameraVerticalRotation, -_maxYAngle, _maxYAngle);
        transform.localRotation = Quaternion.Euler(_cameraVerticalRotation, 0f, 0f);
    }

    public void SetPaused(bool isPaused)
    {
        _isPaused = isPaused;
    }
}