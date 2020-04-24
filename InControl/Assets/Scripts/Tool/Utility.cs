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

    public static float Abs(float value)
    {
        return value < 0.0f ? -value : value;
    }

    internal static float ValueFromSides(float negativeSide, float positiveSide)
    {
        var nsv = Utility.Abs(negativeSide);
        var psv = Utility.Abs(positiveSide);

        if (Utility.Approximately(nsv, psv))
        {
            return 0.0f;
        }

        return nsv > psv ? -nsv : psv;
    }

    internal static float ValueFromSides(float negativeSide, float positiveSide, bool invertSides)
    {
        if (invertSides)
        {
            return ValueFromSides(positiveSide, negativeSide);
        }
        else
        {
            return ValueFromSides(negativeSide, positiveSide);
        }
    }

    internal static bool TargetIsButton(InputControlType target)
    {
        return (target >= InputControlType.Action1 && target <= InputControlType.Action12) ||
               (target >= InputControlType.Button0 && target <= InputControlType.Button19);
    }

    internal static bool TargetIsStandard(InputControlType target)
    {
        return (target >= InputControlType.LeftStickUp && target <= InputControlType.Action12) ||
               (target >= InputControlType.Command && target <= InputControlType.DPadY);
    }
}
