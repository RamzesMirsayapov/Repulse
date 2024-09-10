using System;
using UnityEngine;

public class OverlapTargetFinder
{
    private OverlapSettings _overlapSettings;

    public OverlapTargetFinder(OverlapSettings overlapSettings)
    {
        _overlapSettings = overlapSettings;
    }

    public bool TryFind<TComponent>(out TComponent component)
    {
        return TryFindComponent(out component);
    }

    private bool TryFindComponent<TComponent>(out TComponent component)
    {
        PerformOverlap();

        for (int i = 0; i < _overlapSettings.Size; i++)
        {
            if (_overlapSettings.ConsiderObstacles)
            {
                if (HasObstacleOnTheWay(i))
                {
                    continue;
                }
            }

            if (HasComponent(_overlapSettings.OverlapResults[i], out component))
            {
                return true;
            }
        }

        component = default;
        return false;
    }

    private void PerformOverlap()
    {
        Vector3 position = _overlapSettings.OverlapPoint.TransformPoint(_overlapSettings.Offset);

        switch(_overlapSettings.OverlapType)
        {
            case OverlapType.box: OverlapBox(position); break;
            case OverlapType.sphere: OverlapSphere(position); break;

            default: throw new ArgumentOutOfRangeException(nameof(_overlapSettings.OverlapType));
        }
    }

    private void OverlapBox(Vector3 position)
    {
        _overlapSettings.Size =
            Physics.OverlapBoxNonAlloc(
            position, _overlapSettings.BoxSize / 2, _overlapSettings.OverlapResults,
            _overlapSettings.OverlapPoint.rotation, _overlapSettings.SearchMask.value);
    }

    private void OverlapSphere(Vector3 position)
    {
        _overlapSettings.Size =
            Physics.OverlapSphereNonAlloc(
            position, _overlapSettings.SphereRadius,
            _overlapSettings.OverlapResults, _overlapSettings.SearchMask.value);
    }

    private bool HasObstacleOnTheWay(int id)
    {
        var startPosition = _overlapSettings.OverlapPoint.position;
        var colliderPosition = _overlapSettings.OverlapResults[id].transform.position;

        return Physics.Linecast(startPosition, colliderPosition, _overlapSettings.ObstacleMask);
    }

    private bool HasComponent<TComponent>(Collider collider, out TComponent component)
    {
        return collider.TryGetComponent(out component);
    }
}
