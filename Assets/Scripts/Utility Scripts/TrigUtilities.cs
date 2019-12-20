using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigUtilities
{
    public static float VectorToDegrees(Vector2 vec)
    {
        float degrees = Mathf.Atan2(vec.x, vec.y) * Mathf.Rad2Deg;
        if (degrees < 0) degrees += 360f;
        return degrees;
    }

    public static Vector2 DegreesToVector(float degrees)
    {
        return new Vector2(Mathf.Sin(degrees * Mathf.Deg2Rad), Mathf.Cos(degrees * Mathf.Deg2Rad)).normalized;
    }
}