using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public abstract class Missile : MonoBehaviour, IReflectable, IPauseHandler
{
    public event Action OnReflected;

    [SerializeField] protected Rigidbody _rigidbody;

    private PauseManager _pauseManager;

    private Vector3 _missileVelocity;

    private bool _isReflected = false;

    protected float _speed;

    protected bool _isPaused = false;

    public bool IsReflected => _isReflected;

    [Inject]
    private void Construct(PauseManager pauseManager)
    {
        _pauseManager = pauseManager;

        _pauseManager.Register(this);

        _isPaused = _pauseManager.IsPaused;
    }


    protected void Initialize(float speed)
    {
        _speed = speed;
    }

    protected virtual void Move()
    {
        _rigidbody.velocity = transform.forward * _speed;
    }

    private void ReflectMissile(Vector3 direction)
    {
        Vector3 newDirection = direction - transform.position;

        newDirection.Normalize();

        _rigidbody.MoveRotation(Quaternion.LookRotation(newDirection));
    }

    public void ReflectionMove(Vector3 direction)
    {
        if (_isReflected == true)
            return;

        _isReflected = true;

        OnReflected?.Invoke();

        ReflectMissile(direction);
    }

    public void SetPaused(bool isPaused)
    {
        _isPaused = isPaused;

        if(gameObject != null)
        {
            _missileVelocity = _rigidbody.velocity;

            if (_isPaused == true)
                _rigidbody.velocity = Vector3.zero;
            else
                _rigidbody.velocity = _missileVelocity;
        }
    }

    public void UnRegisterPauseMissile()
    {
        if (_pauseManager != null)
        {
            _pauseManager.UnRegister(this);
        }
    }

    private void OnDestroy()
    {
        UnRegisterPauseMissile();
    }
}