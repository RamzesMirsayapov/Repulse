using System;
using System.Collections.Generic;
using UnityEngine;

public static class OverlapSettingsExtensions
{
    public static bool TryFind<TComponent>(this OverlapSettings overlapSettings, out TComponent component)
    {
        return TryFindComponent(overlapSettings, out component);
    }

    public static bool TryFind<TComponent>(this OverlapSettings overlapSettings, ICollection<TComponent> results)
    {
        return TryFindManyComponent(overlapSettings, results);
    }

    private static bool TryFindComponent<TComponent>(OverlapSettings overlapSettings, out TComponent component)
    {
        PerformOverlap(overlapSettings);

        for (int i = 0; i < overlapSettings.Size; i++)
        {
            if (overlapSettings.ConsiderObstacles)
            {
                if (HasObstacleOnTheWay(overlapSettings, i))
                {
                    continue;
                }
            }

            if (HasComponent(overlapSettings.OverlapResults[i], out component))
            {
                return true;
            }
        }

        component = default;
        return false;
    }

    private static bool TryFindManyComponent<TComponent>(OverlapSettings overlapSettings, ICollection<TComponent> results)
    {
        results.Clear();
        PerformOverlap(overlapSettings);

        for (int i = 0; i < overlapSettings.Size; i++)
        {
            if (overlapSettings.ConsiderObstacles)
            {
                if (HasObstacleOnTheWay(overlapSettings, i))
                {
                    continue;
                }
            }

            if (HasComponent(overlapSettings.OverlapResults[i], out TComponent target))
            {
                results.Add(target);
            }
        }

        return results.Count > 0;
    }

    private static void PerformOverlap(OverlapSettings overlapSettings)
    {
        Vector3 position = overlapSettings.OverlapPoint.TransformPoint(overlapSettings.Offset);

        switch(overlapSettings.OverlapType)
        {
            case OverlapType.box: OverlapBox(overlapSettings, position); break;
            case OverlapType.sphere: OverlapSphere(overlapSettings, position); break;

            default: throw new ArgumentOutOfRangeException(nameof(overlapSettings.OverlapType));
        }
    }

    private static void OverlapBox(OverlapSettings overlapSettings, Vector3 position)
    {
        overlapSettings.Size =
            Physics.OverlapBoxNonAlloc(
            position, overlapSettings.BoxSize / 2, overlapSettings.OverlapResults,
            overlapSettings.OverlapPoint.rotation, overlapSettings.SearchMask.value);
    }

    private static void OverlapSphere(OverlapSettings overlapSettings, Vector3 position)
    {
        overlapSettings.Size =
            Physics.OverlapSphereNonAlloc(
            position, overlapSettings.SphereRadius,
            overlapSettings.OverlapResults, overlapSettings.SearchMask.value);
    }

    private static bool HasObstacleOnTheWay(OverlapSettings overlapSettings, int id)
    {
        var startPosition = overlapSettings.OverlapPoint.position;
        var colliderPosition = overlapSettings.OverlapResults[id].transform.position;

        return Physics.Linecast(startPosition, colliderPosition, overlapSettings.ObstacleMask);
    }

    private static bool HasComponent<TComponent>(Collider collider, out TComponent component)
    {
        return collider.TryGetComponent(out component);
    }
}