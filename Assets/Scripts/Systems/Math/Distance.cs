using UnityEngine;

public static class Distance
{
    public static (Vector2 direction, Vector2 directionNormalized, float distance) GetDistanceInfo(Vector2 pointA, Vector2 pointB)
    {
        float distance = Vector2.Distance(pointA, pointB);
        Vector2 direction = (pointB - pointA);
        Vector2 directionNormalized = direction.normalized;
        return (direction, directionNormalized, distance);
    }
}
