using UnityEngine;
using Zenject;

public class TargetPointer : MonoBehaviour
{
    private PointDisplayer _pointDisplayer;

    private Missile _missile;
    private IExplosion _exploesionAttack;

    private bool _isRemoved = false;

    [Inject]
    private void Construct(PointDisplayer pointDisplayer)
    {
        _pointDisplayer = pointDisplayer;

        InitializeComponents();
    }

    private void InitializeComponents()
    {
        if (TryGetComponent(out Missile missile))
        {
            _missile = missile;
        }
        //_missile = GetComponent<Missile>();

        if (TryGetComponent(out ExplosionAttack explosionAttack))
        {
            _exploesionAttack = explosionAttack;
        }
        else if (TryGetComponent(out DecoyExplosionAttack decoyExplosionAttack))
        {
            _exploesionAttack = decoyExplosionAttack;
        }

        //Debug.Log(_exploesionAttack);
    }

    private void Start()
    {
        AddToList();
    }

    private void OnEnable()
    {
        _missile.OnReflected += RemoveFromList;
        _exploesionAttack.OnExploded += RemoveFromList;
    }

    private void OnDisable()
    {
        _missile.OnReflected -= RemoveFromList;
        _exploesionAttack.OnExploded -= RemoveFromList;
    }

    private void AddToList()
    {
        _pointDisplayer.AddTargetToList(this);
    }

    private void RemoveFromList()
    {
        if(_isRemoved)
            return;

        _isRemoved = true;

        _pointDisplayer.RemoveFromList(this);
    }
}
