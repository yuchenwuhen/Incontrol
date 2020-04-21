using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility {

	public static bool IsNotZero( float value)
    {
        return (value < -Mathf.Epsilon) || (value > Mathf.Epsilon);
    }

    public static bool IsZero(float value)
    {
        return (value >= -Mathf.Epsilon) && (value <= Mathf.Epsilon);
    }

    public static bool AbsoluteIsOverThreshold(float value, float threshold)
    {
        return (value < -threshold) || (value > threshold);
    }

    public static bool Approximately(float v1, float v2)
    {
        var delta = v1 - v2;
        return (delta >= -Mathf.Epsilon) && (delta <= Mathf.Epsilon);
    }

    public static float SymbolAbs(float value)
    {
        return value < 0.0f ? -value : value;
    }
}
