using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoAxisInputControl : IInputControl {

    public OneAxisInputControl Left { get; protected set; }
    public OneAxisInputControl Right { get; protected set; }
    public OneAxisInputControl Up { get; protected set; }
    public OneAxisInputControl Down { get; protected set; }

    public float X { get; protected set; }
    public float Y { get; protected set; }

    public ulong UpdateTick { get; protected set; }

    float sensitivity = 1.0f;
    float lowerDeadZone = 0.0f;
    float upperDeadZone = 1.0f;
    float stateThreshold = 0.0f;

    bool thisState;
    bool lastState;
    Vector2 thisValue;
    Vector2 lastValue;

    bool clearInputState;

    public TwoAxisInputControl()
    {
        Left = new OneAxisInputControl();
        Right = new OneAxisInputControl();
        Up = new OneAxisInputControl();
        Down = new OneAxisInputControl();
    }

    public void ClearInputState()
    {
        Left.ClearInputState();
        Right.ClearInputState();
        Up.ClearInputState();
        Down.ClearInputState();

        X = 0.0f;
        Y = 0.0f;

        lastState = false;
        lastValue = Vector2.zero;
        thisState = false;
        thisValue = Vector2.zero;

        clearInputState = true;
    }

    internal void UpdateWithAxes(float x, float y, ulong updateTick, float deltaTime)
    {
        lastState = thisState;
        lastValue = thisValue;

        thisValue = new Vector2(x, y);

        X = thisValue.x;
        Y = thisValue.y;

        Left.CommitWithValue(Mathf.Max(0.0f, -X), updateTick, deltaTime);
        Right.CommitWithValue(Mathf.Max(0.0f, X), updateTick, deltaTime);

        Up.CommitWithValue(Mathf.Max(0.0f, Y), updateTick, deltaTime);
        Down.CommitWithValue(Mathf.Max(0.0f, -Y), updateTick, deltaTime);

        thisState = Up.State || Down.State || Left.State || Right.State;

        if (clearInputState)
        {
            lastState = thisState;
            lastValue = thisValue;
            clearInputState = false;
            HasChanged = false;
            return;
        }

        if (thisValue != lastValue)
        {
            UpdateTick = updateTick;
            HasChanged = true;
        }
        else
        {
            HasChanged = false;
        }
    }

    public bool HasChanged
    {
        get;
        protected set;
    }

    public bool IsPressed
    {
        get
        {
            return thisState;
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

    public static implicit operator bool(TwoAxisInputControl instance)
    {
        return instance.thisState;
    }


    public static implicit operator Vector2(TwoAxisInputControl instance)
    {
        return instance.thisValue;
    }


    public static implicit operator Vector3(TwoAxisInputControl instance)
    {
        return instance.thisValue;
    }
}
