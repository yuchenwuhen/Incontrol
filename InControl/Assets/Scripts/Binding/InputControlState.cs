using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputControlState {

    public bool State;
    public float Value;
    public float RawValue;


    public void Reset()
    {
        State = false;
        Value = 0.0f;
        RawValue = 0.0f;
    }


    public void Set(float value)
    {
        Value = value;
        State = Utility.IsNotZero(value);
    }


    public void Set(float value, float threshold)
    {
        Value = value;
        State = Utility.AbsoluteIsOverThreshold(value, threshold);
    }


    public void Set(bool state)
    {
        State = state;
        Value = state ? 1.0f : 0.0f;
        RawValue = Value;
    }

    public static implicit operator bool (InputControlState state)
    {
        return state.State;
    }

    public static bool operator ==(InputControlState stateA, InputControlState stateB)
    {
        if (stateA.State != stateB.State) return false;
        return Utility.Approximately(stateA.Value, stateA.Value);
    }

    public static bool operator !=(InputControlState stateA, InputControlState stateB)
    {
        if (stateA.State != stateB.State) return true;
        return !Utility.Approximately(stateA.Value, stateA.Value);
    }
}
