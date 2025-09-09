using UnityEngine;

public class FlightCalculationBezier
{
    public Vector3 GetPoint(Vector3 startPoint, Vector3 middlePoint, Vector3 targetPoint ,float t)
    {
        t = Mathf.Clamp01(t);

        float oneMinusT = 1f - t;

        Vector3 formula = oneMinusT * oneMinusT * startPoint + 2 * oneMinusT * t
            * middlePoint + t * t* targetPoint;

        return formula;
    }

    public Vector3 GetFirstDerivative(Vector3 startPoint, Vector3 middlePoint, Vector3 targetPoint, float t)
    {
        t = Mathf.Clamp01(t);

        float oneMinusT = 1f - t;

        Vector3 formula = 2f * (1f - t) * (middlePoint - startPoint) + 2
            * t * (targetPoint - middlePoint);

        return formula;
    }
}