using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, IPauseHandler
{
    public event Action<Color> OnEffectStarted;
    public event Action OnEffectEnded;

    [Header("Movement Stats")]
    [SerializeField, Min(0f)] private float _moveSpeed;

    [Header("CharacterComponent")]
    [SerializeField] private CharacterController _characterController;

    public CharacterController CharacterController => _characterController;

    private PauseManager _pauseManager;

    private IInput _input;

    private bool _isPauesed;

    private float _velocityDirectionY;

    private Vector3 _velocityDirection;
    private Vector3 _xzDirection;

    public float VelocityDirectionY
    {
        get { return _velocityDirectionY; }
        set { _velocityDirectionY = value; }
    }

    [Inject]
    private void Construct(IInput input, PauseManager pauseManager)
    {
        _input = input;
        _pauseManager = pauseManager;

        _input.OnDirectionMove += Move;

        _pauseManager.Register(this);
    }

    private void OnDisable()
    {
        _input.OnDirectionMove -= Move;
    }

    private void Move(Vector3 moveDirection)
    {
        if (_isPauesed)
            return;

        _xzDirection = (moveDirection.x * transform.right) + (moveDirection.z * transform.forward);
        _xzDirection = Vector3.ClampMagnitude(_xzDirection, 1f) * _moveSpeed;

        _velocityDirection = new Vector3(_xzDirection.x, VelocityDirectionY, _xzDirection.z);

        _characterController.Move(_velocityDirection * Time.deltaTime);
    }

    public void ApplySpeedEffect(float multiplier, float duration, Color boostEffectColor)
    {
        float originalSpeed = _moveSpeed;

        _moveSpeed = originalSpeed * multiplier;

        OnEffectStarted?.Invoke(boostEffectColor);

        StartCoroutine(ResetSpeedAfterDelay(duration, originalSpeed));
    }

    private IEnumerator ResetSpeedAfterDelay(float duration, float speed)
    {
        yield return new WaitForSeconds(duration);

        OnEffectEnded?.Invoke();

        _moveSpeed = speed;
    }

    public void SetPaused(bool isPaused)
    {
        _isPauesed = isPaused;
    }
}
