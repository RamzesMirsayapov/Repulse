using UnityEngine;
using Zenject;

public class PlayerRotation : MonoBehaviour
{
    [Header("Rotate Stats")]
    [SerializeField, Min(0f)] private float _sensitivity;
    [SerializeField, Min(0f)] private float _maxYAngle = 90f;

    private float _cameraVerticalRotation;

    private IInput _input;

    [Inject]
    private void Construct(IInput input)
    {
        _input = input;

        _input.OnRotate += Rotate;
    }

    private void OnDisable()
    {
        _input.OnRotate -= Rotate;
    }

    public void Rotate(float inputX, float inputY)
    {
        inputX *= _sensitivity;
        inputY *= _sensitivity;

        transform.parent.Rotate(Vector3.up * inputX);

        _cameraVerticalRotation -= inputY;
        _cameraVerticalRotation = Mathf.Clamp(_cameraVerticalRotation, -_maxYAngle, _maxYAngle);
        transform.localRotation = Quaternion.Euler(_cameraVerticalRotation, 0f, 0f);
    }
}
