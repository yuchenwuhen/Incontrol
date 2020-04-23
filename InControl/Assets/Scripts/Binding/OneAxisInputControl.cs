using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneAxisInputControl : IInputControl
{
    InputControlState lastState;
    InputControlState nextState;
    InputControlState thisState;

    float stateThreshold = 0.0f;

    public float FirstRepeatDelay = 0.8f;
    public float RepeatDelay = 0.1f;

    public ulong UpdateTick { get; protected set; }

    ulong pendingTick;
    bool pendingCommit;

    float nextRepeatTime;
    float lastPressedTime;

    bool clearInputState;

    public float LastValue
    {
        get
        {
            return lastState.Value;
        }
    }

    public float Value
    {
        get
        {
            return thisState.Value;
        }
    }

    void PrepareForUpdate(ulong updateTick)
    {
        if (updateTick < pendingTick)
        {
            throw new System.Exception("Cannot be updated with an earlier tick.");
        }

        if (pendingCommit && updateTick != pendingTick)
        {
            throw new System.Exception("Cannot be updated for a new tick until pending tick is committed.");
        }

        if (updateTick > pendingTick)
        {
            lastState = thisState;
            nextState.Reset();
            pendingTick = updateTick;
            pendingCommit = true;
        }
    }

    public bool UpdateWithValue(float value, ulong updateTick, float deltaTime)
    {
        PrepareForUpdate(updateTick);

        if (Utility.Abs(value) > Utility.Abs(nextState.RawValue))
        {
            nextState.RawValue = value;

            nextState.Set(value, stateThreshold);

            return true;
        }

        return false;
    }

    public void Commit()
    {
        pendingCommit = false;

        thisState = nextState;

        if (clearInputState)
        {
            lastState = nextState;
            UpdateTick = pendingTick;
            clearInputState = false;
            return;
        }

        var lastPressed = lastState.State;
        var thisPressed = thisState.State;

        if (lastPressed && !thisPressed) // if was released...
        {
            nextRepeatTime = 0.0f;
        }
        else
        if (thisPressed) // if is pressed...
        {
            if (lastPressed != thisPressed) // if has changed...
            {
                nextRepeatTime = Time.realtimeSinceStartup + FirstRepeatDelay;
            }
            else
            if (Time.realtimeSinceStartup >= nextRepeatTime)
            {
                nextRepeatTime = Time.realtimeSinceStartup + RepeatDelay;
            }
        }

        if (thisState != lastState)
        {
            UpdateTick = pendingTick;
        }
    }

    public bool HasChanged
    {
        get
        {
            return thisState != lastState;
        }
    }

    public bool IsPressed
    {
        get
        {
            return thisState.State;
        }
    }

    public bool WasPressed
    {
        get
        {
            return thisState && !lastState;
        }
    }

    public bool WasReleased
    {
        get
        {
            return !thisState && lastState;
        }
    }

    public void ClearInputState()
    {
        lastState.Reset();
        thisState.Reset();
        nextState.Reset();
    }
}
